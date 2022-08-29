using AwsAuthLibrary;

namespace AwsAuthTest;

public class UnitTest1
{
    [Fact]
    public void Add_ShouldAdd1()
    {
        Assert.Equal(4, Demo.Add(2, 2));
    }

    [Fact]
    public void Add_ShouldAdd()
    {
        Assert.Equal(5, Demo.Add(3, 2));
    }
}