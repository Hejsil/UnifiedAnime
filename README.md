UnifiedAnime
==================
A library trying to make one common interface for most anime database sites.

# Examples
Let's imagine a program that prints the users anime library for them. This program is easily implemented using UnifiedAnime.

```csharp
static void Main(string[] args)
{
    IAnimeClient client;
    var username = args[0];
    var site = args[1];

    switch (site)
    {
        case "HummingBird":
            client = new UnifiedHummingBirdV1Client();
            break;
        case "MyAnimeList":
            client = new UnifiedMyAnimeListClient();
            break;
        case "AniList":
            client = new UnifiedAniListClient();
            break;
        default:
            Console.WriteLine($"Site not supported: {site}");
            return;
    }
    
    var response = client.BrowseUserLibrary(username);

    if (response.Item1.Status == ResponseStatus.Success)
    {
        var animeEntries = response.Item2;

        foreach (var entry in animeEntries)
        {
            Console.WriteLine(entry.Info.Title);
        }
    }

    Console.ReadKey();
}
```

As seen from the example, swapping out clients is easy. With a system like this, having mutible client connected at the same time can also be beneficial, as users might have an account on more than one site at the time.

```csharp
var client = new IAnimeClient[2]
{
    new UnifiedHummingBirdV1Client();
    new UnifiedAniListClient();
};

client[0].AuthenticateUsername("HummingBirdUsername", "HummingBirdPassWord");
client[0].AuthenticateUsername("AniListUsername", "AniListPassWord");

// We search for an anime on one client
var response = client[0].BrowseAnime("Flip Flappers");

if (response.Item1.Success)
{
    // We can now add the result to both clients
    client[0].LibraryAdd(response.Item2[0]);
    client[1].LibraryAdd(response.Item2[0]);
}

```

A whole system, for keeping multible sites up to date, could be implemented like this.

So inferface are great, but a generic intefaces can cover everything. Some sites might have something specific that the interface can't incorparate. None inferface versions of each client can therefor also be instantiated.

```csharp
var client = new HummingBirdV1Client();
var unifiedClient = new UnifiedHummingBirdV1Client();
IAnimeClient interfacedClient = new UnifiedHummingBirdV1Client();

// Returns Tuple<Response, Anime[]>
var data1 = client.SearchAnime("Flip Flappers");

// Returns Tuple<Response, IAnimeInfo[]>
var unifiedData1 = unifiedClient.BrowseAnime("Flip Flappers");

// Interfaced clients inherit from uninterfaced clients, 
// so uninterfaced functionallity is available.
var data2 = unifiedClient.SearchAnime("Flip Flappers");

// This gives compile error, as the interface doesn't have 
// a SearchAnime method.
// var data3 = interfacedClient.SearchAnime("Flip Flappers");
```



# Status
## High priority
- [ ] Finalize the IAnimeClient interface
- [ ] Finalize the interfaces for data
    * [ ] IAnimeEntry
    * [ ] IAnimeInfo
    * [ ] IFeedEntry
    * [ ] IUserInfo

## Medium priority
- [ ] Implement AniList client
- [x] Implement HummingBird client
- [ ] Implement MyAnimeList client

## Low priority 
- [ ] Implement AniDB client
- [ ] Implement AnimePlanet client (AnimePlanet have no api, so it's gonna be one giant hack(Is it even possible?))
