A simple library which automatically intercepts "`?include`" parameters made over an HTTP request and eagerly loads the requested properties from an underlying Entity Framework model.

### Overview
This library exists to solve a simple problem when implementing a REST API which supports eager-loading, using Entity Framework as a back-end storage. Specifically, the client can specify an `?include` parameter in the request to have these related entities returned in a single HTTP call. 

In my particular case, this was a JsonApi endpoint, whose [specification](http://jsonapi.org/format/#fetching-includes) optionally allows this behaviour, but you do not need to be using JsonApi yourself to consume this library. 

It's not a lot, but doing this automatically saves you from a handful of niggles.

_Targets .NET Standard 1.3 (i.e. .NET 4.6 and up), ASP.NET Core 1.1, and Entity Framework Core 1.1._


### Usage
This library includes a type, `MercuryDbContext`, which is derived from EF's `DbContext`. You should make your context subclass `MercuryDbContext`. 

Constructing the `MercuryDbContext` requires an instance of `IHttpContextAccessor`, in addition to the `DbContextOptions`. I suggest that you register this with ASP.NET's `IServiceCollection` during application startup. (Or, use the `.AddMercuryApi()` extension method). 

Once you have your instance of `MercuryDbContext`, use it exactly as you would a regular `DbContext`. When you wish to automatically load related entities based on the user's query, instead of `Set<TEntity>()`, call the `IncludedSet<TEntity>()` method. 

You can force certain entities to always be eagerly loaded in addition to whatever is requested by the client by simply chaining a call to Entity Framework's standard `.Include()` /  `.ThenInclude()` methods after the call to `IncludedSet<TEntity>()`. 


Example:

    myContext.IncludedSet<Customer>()
        .Include(c => c.Reviews)
            .ThenInclude(r => r.Product)
                .ThenInclude(p => p.Manufacturer)
        .Include(c => c.Address)

There is also a fully-functional sample project included in the source code for you to examine, in both .NET 4.6 and .NET Core flavours. 

### Known issues and caveats
_These limitations may not matter to you, and could potentially be addressed by future developments, but it's worth pointing them out._

* The properties specified in the `?include` string refer to those that you return to the client, whereas MercuryApi interprets them as the properties on the associated database entity. If you perform any sort of projection or mapping of the DB entities before returning them to the client, they must match up closely or MercuryApi may not work. 
* Entity Framework Core does not ([yet](https://github.com/aspnet/EntityFramework/issues/1368)) support many-to-many relationships natively. You must manually model the link table entities. This implementation detail therefore spills onto the client who must know to traverse the link in order to reach the intended entity. Thus, something like navigating intuitively from a basket to its collection of products via `?include=Basket.Products` would not work, it would need to be something more along the lines of `?include=Basket.BasketProducts.Product`.
* Reflects over the types being navigated every time and does not cache any member accesses.
* Makes the assumption that every property that you `?include` comes from a database - does not consider that they may come from anywhere else.




