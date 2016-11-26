UnifiedAnime
==================
A projekt trying to make one common interface for most anime database sites. The goal is to be able to write this in your project:

```csharp
IAnimeClient[] clients = new IAnimeClient[]
{
    new AniDBClient(),
    new AniListClient(),
    new AnimePlanetClient(),
    new HummingBirdV1Client(),
    new HummingBirdV2Client(),
    new MyAnimeListClient()
};

foreach (var client in clients)
{
    var response = client.SearchAnime("Flip Flappers");

    if (response.Status == ResponseStatus.Success)
    {
        foreach (var anime in response.Data)
        {
            Console.WriteLine(anime.Title);
        }
    }
}
```

For now, only a small supset of what is shown in the code example works. 


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
- [ ] Implement AnimePlanet client (AnimePlanet have no api, so it's gonna be one giant hack)
