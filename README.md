UnifiedAnime
==================
A library trying to make one common interface for most anime database sites.

# Examples
Let's imagine a program that prints the users anime library for them. This program is easily implemented using UnifiedAnime.

```csharp
static void Main(string[] args)
{
    IAnimeProfile profile;
    var username = args[0];
    var password = args[1];
    var site = args[2];

    switch (site)
    {
        case "HummingBird":
            profile = new UnifiedHummingBirdV1Profile();
            break;
        case "AniList":
            profile = new UnifiedAniListProfile("clientId", "clientSecret");
            break;
        default:
            Console.WriteLine($"Site not supported: {site}");
            return;
    }

    profile.Authenticate(username, password);
    var response = profile.Get();

    if (response.Status == ResponseStatus.Success)
    {
        var animeEntries = response.Data;

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
var profiles = new IAnimeProfile[2]
{
    new UnifiedHummingBirdV1Profile();
    new UnifiedAniListProfile("clientId", "clientSecret");
};

profiles[0].Authenticate("HummingBirdUsername", "HummingBirdPassWord");
profiles[1].Authenticate("AniListUsername", "AniListPassWord");

var hbBrowser = new UnifiedHummingBirdV1Browser();

// We search for an anime on one client
var response = hbBrowser.Search("Flip Flappers");

if (response.Status == ResponseStatus.Success)
{
    // We can now add the result to both clients
    profiles[0].Add(response.Data[0]);
    profiles[1].Add(response.Data[0]);
}

```

A whole system, for keeping multible sites up to date, could be implemented like this.

So inferface are great, but a generic intefaces can cover everything. Some sites might have something specific that the interface can't incorparate. None inferface versions of each client can therefor also be instantiated.

```csharp
var browser1 = new HummingBirdV1Browser();
var browser2 = new UnifiedHummingBirdV1Client();
IAnimeClient browse3 = new UnifiedHummingBirdV1Client();

Response<Anime[]> data1 = browser1.GetSearchAnime("Flip Flappers");
Response<IAnimeInfo[]> data2 = browser2.Search("Flip Flappers");

// Interfaced clients inherit from uninterfaced clients, 
// so uninterfaced functionallity is also available.
Response<Anime[]> data3 = browser2.GetSearchAnime("Flip Flappers");

// This gives compile error, as the interface doesn't have 
// a GetSearchAnime method.
Response<Anime[]> data4 = browse3.GetSearchAnime("Flip Flappers");
```



# Status
## High priority
- [ ] Finalize the IAnimeProfile interface
- [ ] Finalize the IAnimeBrowser interface
- [ ] Finalize the interfaces for data
    * [ ] IAnimeEntry
    * [ ] IAnimeInfo

## Medium priority
- [ ] Implement AniList
    * [ ] Browser
    * [ ] Profile
- [x] Implement HummingBird
    * [x] Browser
    * [x] Profile
- [ ] Implement MyAnimeList
    * [ ] Browser
    * [ ] Profile

## Low priority 
- [ ] Implement AniDB
    * [ ] Browser
    * [ ] Profile
- [ ] Implement AnimePlanet (AnimePlanet have no api. Is it even possible?)
    * [ ] Browser
    * [ ] Profile
