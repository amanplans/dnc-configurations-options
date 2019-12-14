# .NET Core 3.1 - Configurations

In this solution we explore how we can configure settings. The examples make use of .NET Core 3.1.
In all examples we make use of the configuration rpvoider from `Microsoft.Extensions.Configuration`.

## Example 1
In the `application.settings.json` file, place the following section:

```json
"Example1": {
    "SiteConfiguration": {
      "BaseUrl": "https://www.example1.com",
      "Key": "ExAmPlE1KeY"
    }
  }
```

So the file looks like:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Example1": {
    "SiteConfiguration": {
      "BaseUrl": "https://www.example1.com",
      "Key": "ExAmPlE1KeY"
    }
  },
  "AllowedHosts": "*"
}
```

In this first example we make use of dependency injection to inject the configuration in the HomeController. 
During Startup the default implementation of `IConfiguration` will be retrieved and assigned to `_configuration`.

Add

```csharp
IConfiguration configuration
```

as a parameter in the constructor. Add the field above the contructor.

```csharp
private readonly IConfiguration _configuration;
```

Initialize the readonly field `_configuration`

```csharp
_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
```

The result should look like this:

```csharp
private readonly IConfiguration _configuration;
private readonly ILogger<HomeController> _logger;

public HomeController(
    IConfiguration configuration,
    ILogger<HomeController> logger)
{
    _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
}
```

Now we can read the BaseUrl from the `application.json` file within the Index method.

First we create the class `Configurations.cs` in the `Models` folder.

```csharp
public class Configurations
{
    public string BaseUrl { get; set; }

    public string Key { get; set; }
}
```

The `BaseUrl` is within the section `SiteConfiguration` which is within the section `Example1`.
To traverse the sections we make use of the colon `:`. We have two sections so we use two `:`, as in

```csharp
var configurations = new Configurations
{
    BaseUrl = _configuration["Example1:SiteConfiguration:BaseUrl"]
};
```

We assign `configurations` as the model in the line

```csharp
    return View(configurations);
```

The `Index` method should look like

```csharp
public IActionResult Index()
{
    var configurations = new Configurations
    {
        BaseUrl = _configuration["Example1:SiteConfiguration:BaseUrl"]
    };

    return View(configurations);
}
```

Now we can show the `BaseUrl` in the view. Open `Views\Index.html`.

We add at the first line

```html
@model Configurations
```

We add
```html
<p>BaseUrl: @Model.BaseUrl</p>
```

before the closing `</div>`

The `Index.html` file will look like

```html
@model Configurations

@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome in Example 1</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
    <p>BaseUrl: @Model.BaseUrl</p>
</div>
```

Now you can start the project with 'F5' and see the result

```
BaseUrl: https://www.example1.com
```

## Example 2
In this example we will make use of Options pattern. We change the Configurations class in the `Models` folder to

```csharp
public class Configurations
{
    public Example2 Example2 { get; set; }
}

public class Example2
{
    public SiteConfiguration SiteConfiguration { get; set; }
}

public class SiteConfiguration
{
    public string BaseUrl { get; set; }

    public string Key { get; set; }
}
```

This mirrors the hierarchy of the sections in `appsettings.json`.

```json
"Example2": {
    "SiteConfiguration": {
      "BaseUrl": "https://www.example2.com",
      "Key": "ExAmPlE1KeY"
    }
  }
```

In `Startup.cs` in the `ConfigureServices` method we add

```csharp
services.Configure<Models.Example2>(Configuration.GetSection(nameof(Example2)));
```

With this line of code, we retrieve the section with the name `Example` from `appsettings.json`. 
Since the hierarchy and the naming is the same this is mapped correctly.
We will see this later when we use it in the view.
  
In the `HomeController` class, we add the parameter

```csharp
IOptions<Models.Example2> settings,
```

The dependency container will give us an instance of the `Example2` class, since we configured this 
in the `Startup` class.
  
We add the following field above the constructor

```csharp
private readonly Models.Example2 _example2;
```

and initialize it in the constructor. The result should look like

```csharp
private readonly Models.Example2 _example2;
private readonly ILogger<HomeController> _logger;

public HomeController(
    IOptions<Models.Example2> settings,
    ILogger<HomeController> logger)
{
    _example2 = settings.Value;
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
}
```

In the `Index` method we create a `Configurations` variable and assign `_example2` to its Exampl2 property.

```chsarp
public IActionResult Index()
{
    var configurations = new Configurations
    {
        Example2 = _example2
    };

    return View(configurations);
}
```

Now we can show the `BaseUrl` in the view. Open `Views\Index.html`.

We add at the first line

```html
@model Configurations
```

We add
```html
<p>BaseUrl: @Model.BaseUrl</p>
```

before the closing `</div>`

The `Index.html` file will look like

```html
@model Configurations

@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome in Example 2</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
    <p>BaseUrl: @Model.Example2.SiteConfiguration.BaseUrl</p>
</div>
```

## Example 3 
Validation configuration using data annotations
  
UserSecrets  
KeyVault  
Configurations in Progam.cs  
IOptionsFactory for unit test  

You can read more at:
- [Configurations in ASP.NET Core - Microsoft docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1)
- [Options pattern in ASP.NET Core - Microsoft docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1)
- [Order of Precedence when Configuring ASP.NET Core](https://devblogs.microsoft.com/premier-developer/order-of-precedence-when-configuring-asp-net-core/)
- [Using Configuration and Options in .NET Core and ASP.NET Core Apps - PluralSight](https://app.pluralsight.com/library/courses/dotnet-core-aspnet-core-configuration-options/table-of-contents)