using ApiResultLibrary;
using AuthLibrary;
using CacheLibrary;
using JwtLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthPortalApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuth _authService;
        private readonly ICache<string> _cacheService;
        private readonly IJwt _jwtService;

        public AuthController(IAuth authService, ICache<string> cacheService, IJwt jwtService)
        {
            _authService = authService;
            _cacheService = cacheService;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<ActionResult<ApiResult<ChangePasswordResponseAto>>> ChangePassword(ChangePasswordAto input)
        {
            try
            {

                await _authService.ChangePassword(input.Username, input.OldPassword, input.NewPassword);

                return new OkObjectResult(
                    ApiResult<ChangePasswordResponseAto>.Ok(
                        new ChangePasswordResponseAto(username: input.Username)
                    )

                ); ;

            }
            catch (PasswordValidationException ex) {

                return new OkObjectResult(ApiResult<ChangePasswordResponseAto>.Error(
                        new NiaveWhoops { 
                            Context = "During change password.",
                            Reason = ex.Message,
                            ErrType = "new-password-invalid",
                            Suggestion = "Try a new password which meets the contraints",
                        }
                    ));
            }

            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("confirm-forgot-password")]
        public async Task<ActionResult<ApiResult<ConfirmForgotPasswordResponseAto>>> ConfirmForgotPassword(ConfirmForgotPasswordAto input) {
            try
            {

                await _authService.ConfirmForgotPassword(input.Username, input.NewPassword, input.ConfirmationCode);

                return new OkObjectResult(ApiResult<object>.Ok(
                    new ConfirmForgotPasswordResponseAto(username: input.Username)
                )); 


            }
            catch (PasswordValidationException ex) { 
                
                return new OkObjectResult(ApiResult<object>.Error(
                        new NiaveWhoops { 
                            Context = "During change password.",
                            Reason = ex.Message,
                            ErrType = "new-password-invalid",
                            Suggestion = "Try a new password which meets the contraints",
                        }
                    ));
            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex.Message);
            }

        }


        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<ApiResult<SignupResponseAto>>> Signup(SignupAto input)
        {

            try
            {

                var result = await _authService.CreateUser(input.Email, input.Username);

                if (result is null)
                {
                    throw new NullReferenceException();
                }

                return new OkObjectResult(ApiResult<SignupResponseAto>.Ok(
                        new SignupResponseAto(
                            username: result.Username ?? "",
                            userStatus: result.UserStatus ??"",
                            email: result.Email ??"",
                            userCreateDate: result.UserCreateDate ?? 0
                        )
                     )
                  );
            }
            catch (UserAlreadyExistsException)
            {
                return new OkObjectResult(
                    ApiResult<SignupResponseAto>.Error(
                            new NiaveWhoops
                            {
                                Context = "During Signup.",
                                Reason = "The username is already taken",
                                ErrType = "username-exists",
                                Suggestion = "Try signing up with a different username",
                            }
                        )
                    );
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<ActionResult<ApiResult<ForgotPasswordResponseAto>>> ForgotPassword(ForgotPasswordAto input)
        {
            try
            {
                await _authService.ForgotPassword(input.Username);
                return new OkObjectResult(
                    ApiResult<ForgotPasswordResponseAto>.Ok(
                        new ForgotPasswordResponseAto(username: input.Username)
                    )
                );
            }
            catch (IncorrectUsernameException)
            {
                return new OkObjectResult(
                    ApiResult<string>.Error(
                            new NiaveWhoops
                            {
                                Context = "During forgot password.",
                                Reason = "Provided username was not found",
                                ErrType = "username-not-found",
                                Suggestion = "Try putting in a valid username",
                            }
                        )
                    );
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [Route("token")]
        [Authorize]
        public async Task<ActionResult<ApiResult<TokenResponseAto>>> Token()
        {

            var key = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(key))
            {
                return new OkObjectResult(
                    ApiResult<TokenResponseAto>.Error(
                            new NiaveWhoops
                            {
                                Context = "Retrieving cognito token",
                                Reason = "Could not find name attribute in JWT",
                                ErrType = "invalid-token",
                                Suggestion = "Add name attribute to JWT",
                            }
                        )
                    );
            }

            var token = await _cacheService.GetValue(key);

            if (string.IsNullOrEmpty(token))
            {
                return new OkObjectResult(
                    ApiResult<TokenResponseAto>.Error(
                            new NiaveWhoops
                            {
                                Context = "Retrieving cognito token",
                                Reason = "Could not find matching value in cache",
                                ErrType = "token-not-found",
                                Suggestion = "Try logging in again to retrieve a new token to try",
                            }
                        )
                    );
            }

            await _cacheService.ClearValue(key);

            return new OkObjectResult(
                    ApiResult<TokenResponseAto>.Ok(
                        new TokenResponseAto(Token: token)
                    )
                );
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ApiResult<LoginResponseAto>>> Login(LoginAto creds)
        {

            try
            {
                var result = await _authService.Login(creds.Username, creds.Password);
                var token = result?.Token ?? throw new NullReferenceException();

                var id = Guid.NewGuid().ToString();

                var claim = new Claim(ClaimTypes.Name, id);

                var jwt = await _jwtService.GenerateToken(new List<Claim> { claim });

                await _cacheService.SetValue(id, token);

                return new OkObjectResult(
                    ApiResult<LoginResponseAto>.Ok(
                        new LoginResponseAto(Token: jwt)
                    )
                );

            }
            catch (NewPasswordNeededException)
            {
                return new OkObjectResult(ApiResult<LoginResponseAto>.Error(new NiaveWhoops
                {
                    Context = "Tried to login.",
                    ErrType = "new-password-needed",
                    Reason = "Password needs to be updated.",
                    Suggestion = "Use the appropiate api route to change the user's password."
                }));
            }
            catch (IncorrectUsernameException)
            {
                return new OkObjectResult(ApiResult<LoginResponseAto>.Error(new NiaveWhoops
                {
                    Context = "Tried to login.",
                    ErrType = "incorrect-username",
                    Reason = "User could not be found.",
                    Suggestion = "Input a valid username."
                }));
            }
            catch (IncorrectCredentialsException)
            {
                return new OkObjectResult(ApiResult<LoginResponseAto>.Error(new NiaveWhoops
                {
                    Context = "Tried to login.",
                    ErrType = "incorrect-password",
                    Reason = "The password entered is incorrect",
                    Suggestion = "Input the correct password, or use the forgot password route to reset the user's password."
                }));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }
    }
}
