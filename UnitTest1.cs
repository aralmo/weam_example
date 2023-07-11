using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace example;

public class UnitTest1
{
    IServiceProvider services;
    public UnitTest1() 
    {
        //arrangement, remember this runs per test in xunit
        services = new ServiceCollection()
            .AddSingleton<ServiceUnderTest>()
            .AddSingleton(Substitute.For<ISomeDependencyToMock>())
            .BuildServiceProvider();
    } 

    [Fact]
    public void Test1()
    {
        //act
        services
            .GetRequiredService<ServiceUnderTest>()
            .Foo();

        //assert against the mock
        services.GetRequiredService<ISomeDependencyToMock>().Received().Bar();
    }
}


public class ServiceUnderTest
{
    private readonly ISomeDependencyToMock dependency;

    public ServiceUnderTest(ISomeDependencyToMock dependency)
    {
        this.dependency = dependency;
    }

    public void Foo()
    {
        dependency.Bar();
    }
}

public interface ISomeDependencyToMock
{
    void Bar();
}