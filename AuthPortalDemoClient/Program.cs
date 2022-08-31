// See https://aka.ms/new-console-template for more information
using Amazon.CognitoIdentityProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AuthLibrary;

using IHost host = Host.CreateDefaultBuilder(args).UseEnvironment("Development").Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

var provider = new AmazonCognitoIdentityProviderClient();


var repo = new CognitoUserRepository(new CognitoUserRepositoryConfig()
{
    Provider = provider,
    UserpoolId = config.GetValue<string>("USERPOOL_ID"),
    ClientId = config.GetValue<string>("CLIENT_ID"),
    ClientSecret = config.GetValue<string>("CLIENT_SECRET"),
});


//var result = await repo.CreateUser("20544dk@gmail.com", "20544");
//var result = await repo.Login("20544", "Password@1234");
//await repo.ChangePassword("20544", ";d3JaDNC", "");

//await repo.ForgotPassword("20544");

await repo.ConfirmForgotPassword("20544", "Password@12345", "128611");

//Console.WriteLine(result.Token.ToString());
Console.ReadLine();



