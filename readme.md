# throttle

# tis it?

Throttle is a package I wrote to do basic request throttling and ip banning on whatever endpoints you want to decorate with the `[Throttle]` decorator.

# setup

- Install the nuget package
- Add IMemoryCache to whatever IOC you use.

```c#
    serviceCollection.AddSingleton<IMemoryCache, MemoryCache>();
```

## that's it folks!

### Want to pay me to build your software?

email: caybo@mergedigital.io

Thanks!

