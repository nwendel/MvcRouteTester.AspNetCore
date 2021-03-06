# MvcRouteTester.AspNetCore [![Build status](https://ci.appveyor.com/api/projects/status/sot37agt946gbm93?svg=true)](https://ci.appveyor.com/project/nwendel/mvcroutetester-aspnetcore)

### NuGet Package

```
Install-Package MvcRouteTester.AspNetCore
```

### Example
```csharp
public class Example : IClassFixture<TestServerFixture>
{

    private readonly TestServer _server;

    public Example(TestServerFixture testServerFixture)
    {
        _server = testServerFixture.Server;
    }

    [Fact]
    public async Task CanRoute()
    {
        await RouteAssert.ForAsync(
            _server,
            request => request.WithPathAndQuery("/some-route"),
            routeAssert => routeAssert.MapsTo<HomeController>(a => a.SomeRoute()));
    }

    [Fact]
    public async Task CanRouteWithArguments()
    {
        await RouteAssert.ForAsync(
            _server,
            request => request.WithPathAndQuery("/some-other-route?parameter=value"),
            routeAssert => routeAssert.MapsTo<HomeController>(a => a.SomeOtherRoute("value")));
    }

}

public class TestServerFixture
{

    public TestServerFixture()
    {
        Server = new TestServer(new WebHostBuilder().UseTestStartup<TestStartup, Startup>());
    }

    public TestServer Server { get; }

    public class TestStartup
    {

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvc();
            serviceCollection.AddMvcRouteTester();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }

    }

}
```
