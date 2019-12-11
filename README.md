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

