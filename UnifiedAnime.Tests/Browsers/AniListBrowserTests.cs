using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using UnifiedAnime.Clients.Browsers.AniList;
using UnifiedAnime.Data.AniList;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Tests.Properties;
using System.Net;

namespace UnifiedAnime.Tests.Browsers
{
    [TestFixture()]
    public class AniListBrowserTests
    {
        public AniListBrowser Browser { get; } 

        public AniListBrowserTests()
        {
            Browser = new AniListBrowser(Resources.AniListClientId, Resources.AniListClientSecret);
            Browser.RefreshCredentials();
        }

        [Test()]
        public void AniListBrowserTest()
        {
            var browser = new AniListBrowser(Resources.AniListClientId, Resources.AniListClientSecret);
        }

        [Test()]
        public void AuthenticateTest()
        {
            var browser = new AniListBrowser(Resources.AniListClientId, Resources.AniListClientSecret);
            var response = browser.RefreshCredentials();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetUserTest(object displayName)
        {
            var response = Browser.GetUser(displayName.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.User);

            var user = response.User;
            Assert.AreEqual("This is a test user for the library UnifiedAnime:\nhttps://github.com/Hejsil/UnifiedAnime", user.About);
            Assert.AreEqual(true, user.AdultContent);
            Assert.AreEqual(true, user.AdvancedRating);
            Assert.Contains("TestRating1", user.AdvancedRatingNames);
            Assert.Contains("TestRating2", user.AdvancedRatingNames);
            Assert.Contains("TestRating3", user.AdvancedRatingNames);
            Assert.AreEqual(1571, user.AnimeTime);
            Assert.Contains("TestCustomAnimeList1", user.CustomListAnime);
            Assert.Contains("TestCustomAnimeList2", user.CustomListAnime);
            Assert.Contains("TestCustomAnimeList3", user.CustomListAnime);
            Assert.IsFalse(user.CustomListAnime.Contains("TestCustomAnimeList4"));
            Assert.Contains("TestCustomMangaList1", user.CustomListManga);
            Assert.Contains("TestCustomMangaList2", user.CustomListManga);
            Assert.Contains("TestCustomMangaList3", user.CustomListManga);
            Assert.IsFalse(user.CustomListManga.Contains("TestCustomMangaList4"));
            Assert.AreEqual("UnifiedAnimeTestUser", user.DisplayName);
            Assert.AreEqual(84039, user.Id);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/userbanner/84039-eOyAb7CU6LoL.png", user.ImageUrlBanner);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/user/reg/default.png", user.ImageUrlLge);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/user/sml/default.png", user.ImageUrlMed);
            Assert.AreEqual(ListOrder.Alphabet, user.ListOrder);
            Assert.AreEqual(0, user.Notifications);
            Assert.AreEqual(ScoreSystem.Star5, user.ScoreType);
            Assert.AreEqual(158, user.MangaChap);
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetActivityTest(object displayName)
        {
            var response = Browser.GetActivity(displayName.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Activities);

            var activities = response.Activities;
            Assert.AreEqual(16, activities.Length);

            // TODO: This will be hard to test, because the activity might change all the time,
            //       and i don't want to fix it every time it happens.
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetFollowersTest(object displayName)
        {
            var response = Browser.GetFollowers(displayName.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Users);

            var followers = response.Users;
            Assert.AreEqual(1, followers.Length);
            var follower = followers[0];
            Assert.AreEqual("hejsil", follower.DisplayName);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/user/reg/72340-aKJVAlZWuTRE.png", follower.ImageUrlLge);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/user/sml/72340-aKJVAlZWuTRE.png", follower.ImageUrlMed);
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetFollowingTest(object displayName)
        {
            var response = Browser.GetFollowing(displayName.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Users);

            var followers = response.Users;
            Assert.AreEqual(1, followers.Length);
            var follower = followers[0];
            Assert.AreEqual("hejsil", follower.DisplayName);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/user/reg/72340-aKJVAlZWuTRE.png", follower.ImageUrlLge);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/user/sml/72340-aKJVAlZWuTRE.png", follower.ImageUrlMed);
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetFavouritesTest(object displayName)
        {
            var response = Browser.GetFavourites(displayName.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Favorites);

            var favorites = response.Favorites;
            Assert.AreEqual(1, favorites.Anime.Length);
            Assert.Null(favorites.Manga);
            Assert.Null(favorites.Character);
            Assert.Null(favorites.Staff);

            var favAnimes = favorites.Anime;
            Assert.AreEqual("Steins;Gate", favAnimes[0].TitleEnglish);

            // TODO: Should probably cover more
        }

        [Test()]
        public void SearchUserTest()
        {
            var response = Browser.SearchUser("UnifiedAnimeTestUser");
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Users);

            var users = response.Users;
            Assert.AreEqual(1, users.Length);
            var user = users[0];
            Assert.AreEqual("UnifiedAnimeTestUser", user.DisplayName);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/user/reg/default.png", user.ImageUrlLge);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/user/sml/default.png", user.ImageUrlMed);
        }

        private void CheckAnimeEntry(AnimeEntry entry, string title, AnimeEntryStatus status, int episodesWatched, int rewatched, int score,
            int priority, bool isPrivate, bool hiddenDefault, string notes, int[] advancedRatingScores, int[] customeLists,
            DateTime? startedOn, DateTime? endedOn)
        {
            Assert.NotNull(entry);
            Assert.AreEqual(title, entry.Anime.TitleEnglish);
            Assert.AreEqual(status, entry.ListStatus);
            Assert.AreEqual(episodesWatched, entry.EpisodesWatched);
            Assert.AreEqual(rewatched, entry.Rewatched);
            Assert.AreEqual(score, entry.Score);
            Assert.AreEqual(priority, entry.Priority); // NOTE: Don't know what this is actually used for, but it seems to always be 0
            Assert.AreEqual(isPrivate, entry.Private);
            Assert.AreEqual(hiddenDefault, entry.HiddenDefault);
            Assert.AreEqual(notes, entry.Notes);

            Assert.AreEqual(advancedRatingScores.Length, entry.AdvancedRatingScores.Length);
            for (var i = 0; i < advancedRatingScores.Length; i++)
                Assert.AreEqual(advancedRatingScores[i], entry.AdvancedRatingScores[i]);

            // TODO: Gotta figure out how the custom list system actually works
            //Assert.AreEqual(customeLists.Length, entry.CustomLists.Length);
            //foreach (var list in customeLists)
            //    Assert.True(entry.CustomLists.Contains(list));
            
            Assert.AreEqual(startedOn, entry.StartedOn);
            Assert.AreEqual(endedOn, entry.FinishedOn);
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetAnimelistTest(object displayName)
        {
            var response = Browser.GetAnimelist(displayName.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.UserWithAnimeList);
            Assert.NotNull(response.UserWithAnimeList.AnimeList);
            Assert.NotNull(response.UserWithAnimeList.AnimeList.Completed);
            Assert.NotNull(response.UserWithAnimeList.AnimeList.Dropped);
            Assert.NotNull(response.UserWithAnimeList.AnimeList.OnHold);
            Assert.NotNull(response.UserWithAnimeList.AnimeList.PlanToWatch);
            Assert.NotNull(response.UserWithAnimeList.AnimeList.Watching);

            var animelist = response.UserWithAnimeList.AnimeList;
            
            Assert.AreEqual(1, animelist.Completed.Length);
            Assert.AreEqual(1, animelist.Dropped.Length);
            Assert.AreEqual(1, animelist.OnHold.Length);
            Assert.AreEqual(1, animelist.PlanToWatch.Length);
            Assert.AreEqual(1, animelist.Watching.Length);
            
            CheckAnimeEntry(animelist.Completed[0], 
                title: "Death Note",
                status: AnimeEntryStatus.Completed,
                episodesWatched: 37,
                rewatched: 0,
                score: 2,
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: "Desu Noto",
                advancedRatingScores: new[] { 1, 2, 3 },
                customeLists: new[] { 3 },
                startedOn: new DateTime(2012, 01, 06), 
                endedOn: new DateTime(2013, 11, 22));
            
            CheckAnimeEntry(animelist.Dropped[0],
                title: "Angel Beats!",
                status: AnimeEntryStatus.Dropped,
                episodesWatched: 8,
                rewatched: 0,
                score: 3, // TODO: Double on site, help!
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: ":)",
                advancedRatingScores: new[] { 5, 4, 2 },
                customeLists: new[] { 1 },
                startedOn: new DateTime(2001, 04, 06),
                endedOn: null);
            
            CheckAnimeEntry(animelist.OnHold[0],
                title: "Steins;Gate",
                status: AnimeEntryStatus.OnHold,
                episodesWatched: 7,
                rewatched: 0,
                score: 5,
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: "No advanced score",
                advancedRatingScores: new int[0], 
                customeLists: new int[0],
                startedOn: null,
                endedOn: null);
            
            CheckAnimeEntry(animelist.PlanToWatch[0],
                title: "Sword Art Online",
                status: AnimeEntryStatus.PlanToWatch,
                episodesWatched: 0,
                rewatched: 0,
                score: 1,
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: null,
                advancedRatingScores: new int[0],
                customeLists: new int[0],
                startedOn: null,
                endedOn: null);
            
            CheckAnimeEntry(animelist.Watching[0],
                title: "Attack on Titan",
                status: AnimeEntryStatus.Watching,
                episodesWatched: 15,
                rewatched: 0,
                score: 3,
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: "This is a test note",
                advancedRatingScores: new[] { 2, 4, 0 },
                customeLists: new[] { 1 },
                startedOn: new DateTime(2015, 12, 1),
                endedOn: new DateTime(2015, 12, 5));
        }

        private void CheckMangaEntry(MangaEntry entry, string title, MangaEntryStatus status, int chaptersRead, int volumesRead,
            int score, int priority, bool isPrivate, bool hiddenDefault, string notes,
            int[] advancedRatingScores, int[] customLists, DateTime? startedOn, DateTime? endedOn)
        {
            Assert.NotNull(entry);
            Assert.AreEqual(title, entry.Manga.TitleEnglish);
            Assert.AreEqual(status, entry.ListStatus);
            Assert.AreEqual(chaptersRead, entry.ChaptersRead);
            Assert.AreEqual(volumesRead, entry.VolumesRead);
            Assert.AreEqual(score, entry.Score);
            Assert.AreEqual(priority, entry.Priority); // NOTE: Don't know what this is actually used for, but it seems to always be 0
            Assert.AreEqual(isPrivate, entry.Private);
            Assert.AreEqual(hiddenDefault, entry.HiddenDefault);
            Assert.AreEqual(notes, entry.Notes);

            Assert.AreEqual(advancedRatingScores.Length, entry.AdvancedRatingScores.Length);
            for (var i = 0; i < advancedRatingScores.Length; i++)
                Assert.AreEqual(advancedRatingScores[i], entry.AdvancedRatingScores[i]);

            // TODO: Gotta figure out how the custom list system actually works
            //Assert.AreEqual(customLists.Length, entry.CustomLists.Length);
            //foreach (var list in customLists)
            //    Assert.True(entry.CustomLists.Contains(list));

            Assert.AreEqual(startedOn, entry.StartedOn);
            Assert.AreEqual(endedOn, entry.FinishedOn);
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetMangalistTest(object displayName)
        {
            var response = Browser.GetMangalist(displayName.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.UserWithMangaList);
            Assert.NotNull(response.UserWithMangaList.MangaList);
            Assert.NotNull(response.UserWithMangaList.MangaList.Completed);
            Assert.NotNull(response.UserWithMangaList.MangaList.Dropped);
            Assert.NotNull(response.UserWithMangaList.MangaList.OnHold);
            Assert.NotNull(response.UserWithMangaList.MangaList.PlanToRead);
            Assert.NotNull(response.UserWithMangaList.MangaList.Reading);

            var mangalist = response.UserWithMangaList.MangaList;

            Assert.AreEqual(1, mangalist.Completed.Length);
            Assert.AreEqual(1, mangalist.Dropped.Length);
            Assert.AreEqual(1, mangalist.OnHold.Length);
            Assert.AreEqual(1, mangalist.PlanToRead.Length);
            Assert.AreEqual(1, mangalist.Reading.Length);

            CheckMangaEntry(mangalist.Completed[0], 
                title: "Goodnight Punpun",
                status: MangaEntryStatus.Completed, 
                chaptersRead: 147,
                volumesRead: 0,
                score: 2,
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: null,
                advancedRatingScores: new[] { 1, 2, 3 },
                customLists: new int[0],
                startedOn: null, 
                endedOn: null);

            CheckMangaEntry(mangalist.Dropped[0],
                title: "Yotsuba&!",
                status: MangaEntryStatus.Dropped,
                chaptersRead: 2,
                volumesRead: 0,
                score: 1,
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: null,
                advancedRatingScores: new int[0],
                customLists: new int[0],
                startedOn: null,
                endedOn: null);

            CheckMangaEntry(mangalist.OnHold[0],
                title: "One Piece",
                status: MangaEntryStatus.OnHold,
                chaptersRead: 3,
                volumesRead: 0,
                score: 3,
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: null,
                advancedRatingScores: new int[0],
                customLists: new int[0],
                startedOn: null,
                endedOn: null);

            CheckMangaEntry(mangalist.PlanToRead[0],
                title: "Fullmetal Alchemist",
                status: MangaEntryStatus.PlanToRead,
                chaptersRead: 0,
                volumesRead: 0,
                score: 4,
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: null,
                advancedRatingScores: new int[0],
                customLists: new int[0],
                startedOn: null,
                endedOn: null);

            CheckMangaEntry(mangalist.Reading[0],
                title: "Berserk",
                status: MangaEntryStatus.Reading,
                chaptersRead: 6,
                volumesRead: 1,
                score: 3,
                priority: 0,
                isPrivate: false,
                hiddenDefault: false,
                notes: "This is a test note",
                advancedRatingScores: new[] { 3, 2, 4 },
                customLists: new[] { 1 },
                startedOn: new DateTime(2015, 12, 01), 
                endedOn: new DateTime(2015, 12, 05));
        }

        [TestCase(2013, Season.Spring, MediaType.TV, SortingMethod.StartDateDescending,
            "The Eccentric Family",
            "Yuuto-kun ga Iku",
            "Kingdom 2",
            "Odoriko Clinoppe",
            "Inazuma Eleven Go: Galaxy",
            "Pokemon Best Wishes! Season 2: Dekorora Adventure")]
        public void BrowseAnimeTest(int year, Season season, MediaType type, SortingMethod sortingMethod, params string[] firstAnimes)
        {
            var response = Browser.BrowseAnime(year: year, season: season, type: type, sortingMethod: sortingMethod);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Animes);

            var animes = response.Animes;
            Assert.NotNull(animes);

            for (var i = 0; i < firstAnimes.Length; i++)
            {
                Assert.AreEqual(firstAnimes[i], animes[i].TitleEnglish);
            }
        }

        [TestCase(2013, MediaType.Manga, SortingMethod.StartDateDescending,
            "The Sea, You, and the Sun",
            "Gendai Majo Zukan",
            "Gabriel DropOut",
            "A Certain Scientific Accelerator",
            "Kantai Collection -Kan Colle- Shimakaze Whirlwind Girl",
            "Bocchiman")]
        public void BrowseMangaTest(int year, MediaType type, SortingMethod sortingMethod, params string[] firstMangas)
        {
            var response = Browser.BrowseManga(year: year, type: type, sortingMethod: sortingMethod);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Mangas);

            var mangas = response.Mangas;
            Assert.NotNull(mangas);

            for (var i = 0; i < firstMangas.Length; i++)
            {
                Assert.AreEqual(firstMangas[i], mangas[i].TitleEnglish);
            }
        }

        [TestCase("clannad",
            "Clannad The Motion Picture",
            "Clannad",
            "Clannad: Another World, Tomoyo Chapter",
            "Clannad After Story",
            "Clannad: Another World, Kyou Chapter")]
        public void SearchAnimeTest(string animeName, params string[] resultNames)
        {
            var response = Browser.SearchAnime(animeName);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Animes);

            var animes = response.Animes;
            Assert.NotNull(animes);

            foreach (var result in resultNames)
            {
                Assert.IsTrue(animes.Any(anime => anime.TitleEnglish == result));
            }
        }

        [TestCase("clannad",
            "Clannad",
            "Tomoyo After ~Dear Shining Memories~",
            "Clannad 4-koma Manga Gekijyou",
            "CLANNAD Official Another Story",
            "Clannad: Tomoyo Dearest")]
        public void SearchMangaTest(string mangaName, params string[] resultNames)
        {
            var response = Browser.SearchManga(mangaName);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Mangas);

            var mangas = response.Mangas;
            Assert.NotNull(mangas);

            foreach (var result in resultNames)
            {
                Assert.IsTrue(mangas.Any(manga => manga.TitleEnglish == result));
            }
        }

        [TestCase("fuko",
            "Fuko Omuro",
            "Fuko Ibuki",
            "Fuko Kuzuha",
            "Fuko Laramie",
            "Fuko Izumi")]
        public void SearchCharacterTest(string characterName, params string[] resultNames)
        {
            var response = Browser.SearchCharacter(characterName);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Characters);

            var characters = response.Characters;
            Assert.NotNull(characters);

            foreach (var result in resultNames)
            {
                Assert.IsTrue(characters.Any(character => character.NameFirst + " " + character.NameLast == result));
            }
        }

        [TestCase("hanasaki",
            "Akira Hanasaki",
            "Kiyomi Hanasaki",
            "Iori Hanasaki")]
        public void SearchStaffTest(string staffName, params string[] resultNames)
        {
            var response = Browser.SearchStaff(staffName);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Staff);

            var staff = response.Staff;
            Assert.NotNull(staff);

            foreach (var result in resultNames)
            {
                Assert.IsTrue(staff.Any(s => s.NameFirst + " " + s.NameLast == result));
            }
        }

        [TestCase("kyoto",
            "Kyoto Animation",
            "Kyotoma")]
        public void SearchStudioTest(string studioName, params string[] resultNames)
        {
            var response = Browser.SearchStudio(studioName);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Studios);

            var studios = response.Studios;
            Assert.NotNull(studios);

            foreach (var result in resultNames)
            {
                Assert.IsTrue(studios.Any(studio => studio.StudioName == result));
            }
        }

        [Test()]
        public void SearchThreadTest()
        {
            // HACK: Don't really know how to test this, as thread are ever changing
            var response = Browser.SearchThread("Attack On Titan");
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Threads);

            var threads = response.Threads;
            Assert.NotNull(threads);
            // HACK: We will just assume, for now, that there is always at least one thread about Attack On Titan
            Assert.Greater(threads.Length, 0);
        }

        [Test()]
        public void GetStaffTest()
        {
            var response = Browser.GetStaff(95185);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Staff);

            var staff = response.Staff;
            Assert.NotNull(staff);

            //Assert.AreEqual(, staff.Dob); TODO: Figure these out
            //Assert.AreEqual(, staff.Info); This is just a bother to check
            //Assert.AreEqual("Kana", staff.NameFirstJapanese); TODO: Figure these out
            //Assert.AreEqual("Kana", staff.NameLastJapanese); TODO: Figure these out
            //Assert.AreEqual(, staff.Website); TODO: Figure these out
            Assert.AreEqual(95185, staff.Id);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/person/reg/185.jpg", staff.ImageUrlLge);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/person/med/185.jpg", staff.ImageUrlMed);
            Assert.AreEqual("Japanese", staff.Language);
            Assert.AreEqual("Kana", staff.NameFirst);
            Assert.AreEqual("Hanazawa", staff.NameLast);
            //Assert.AreEqual(, staff.Role); TODO: Figure these out
        }

        [Test()]
        public void GetStaffPageTest()
        {
            // HACK: What difference is this from GetStaff?
            var response = Browser.GetStaffPage(95185);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Staff);

            var staff = response.Staff;
            Assert.NotNull(staff);

            //Assert.AreEqual(, staff.Dob); TODO: Figure these out
            //Assert.AreEqual(, staff.Info); This is just a bother to check
            //Assert.AreEqual("Kana", staff.NameFirstJapanese); TODO: Figure these out
            //Assert.AreEqual("Kana", staff.NameLastJapanese); TODO: Figure these out
            //Assert.AreEqual(, staff.Website); TODO: Figure these out
            Assert.AreEqual(95185, staff.Id);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/person/reg/185.jpg", staff.ImageUrlLge);
            Assert.AreEqual("https://cdn.anilist.co/img/dir/person/med/185.jpg", staff.ImageUrlMed);
            Assert.AreEqual("Japanese", staff.Language);
            Assert.AreEqual("Kana", staff.NameFirst);
            Assert.AreEqual("Hanazawa", staff.NameLast);
            //Assert.AreEqual(, staff.Role); TODO: Figure these out
        }

        [Test()]
        public void GetStudioTest()
        {
            var response = Browser.GetStudio(2);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Studio);

            var studio = response.Studio;
            Assert.NotNull(studio);

            //Assert.AreEqual(, studio.MainStudio); TODO: Figure these out
            Assert.AreEqual("Kyoto Animation", studio.StudioName);
            //Assert.AreEqual(, //studio.StudioWiki); TODO: Figure these out
        }

        [Test()]
        public void GetStudioPageTest()
        {
            // HACK: What difference is this from GetStudio?
            var response = Browser.GetStudioPage(2);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Studio);

            var studio = response.Studio;
            Assert.NotNull(studio);

            //Assert.AreEqual(, //studio.MainStudio); TODO: Figure these out
            Assert.AreEqual("Kyoto Animation", studio.StudioName);
            //Assert.AreEqual(, //studio.StudioWiki); TODO: Figure these out
        }

        [Test()]
        public void GetAnimeReviewTest()
        {
            var response = Browser.GetAnimeReview(2131);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Review);

            var review = response.Review;

            Assert.AreEqual("Sound! Euphonium 2", review.Anime.TitleEnglish);
            //Assert.AreEqual(2016", review.Date, "Dec 29); // TODO: Fix date datatype
            Assert.AreEqual(0, review.Private); // TODO: Use bool datatype
            //Assert.AreEqual(80, review.Rating); // TODO: Rating vs Score?
            Assert.AreEqual(80, review.Score);
            //Assert.AreEqual(, review.RatingAmount); TODO: What is this?
            Assert.AreEqual("Sound! S2 takes the stage set-up by S1 and tells a compelling story about hardships of performing and growing up", review.Summary);
            Assert.AreEqual("WillQ", review.User.DisplayName);
            //Assert.AreEqual(, review.UserRating); TODO: What is this ?
            Assert.AreEqual(2131, review.Id);
            
        }

        [Test()]
        public void GetMangaReviewTest()
        {
            var response = Browser.GetMangaReview(2116);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Review);

            var review = response.Review;

            Assert.AreEqual("Evil Blade", review.Manga.TitleEnglish);
            //Assert.AreEqual(2016", review.Date, "Dec 29); // TODO: Fix date datatype
            Assert.AreEqual(0, review.Private); // TODO: Use bool datatype
            //Assert.AreEqual(80, review.Rating); // TODO: Rating vs Score?
            Assert.AreEqual(78, review.Score);
            //Assert.AreEqual(, review.RatingAmount); TODO: What is this?
            Assert.AreEqual("So much potential, but the author doesn't seem to be inrested in't", review.Summary);
            Assert.AreEqual("Nipoking", review.User.DisplayName);
            //Assert.AreEqual(, review.UserRating); TODO: What is this ?
            Assert.AreEqual(2116, review.Id);
        }

        [Test()]
        public void GetAnimeReviewsTest()
        {
            // 2167 is Clannad
            var response = Browser.GetAnimeReviews(2167);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Reviews);
            Assert.Greater(response.Reviews.Length, 0);
            // TODO: Maybe more
        }

        [Test()]
        public void GetMangaReviewsTest()
        {
            // 30002 is Evil Blade
            var response = Browser.GetMangaReviews(30070);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Reviews);
            Assert.Greater(response.Reviews.Length, 0);
            // TODO: Maybe more
        }

        [TestCase("WillQ")]
        [TestCase("WillQ")] // TODO: Use id
        public void GetUserReviewsTest(object displayName)
        {
            var response = Browser.GetUserReviews(displayName.ToString());
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Review);
            Assert.NotNull(response.Review.AnimeReviews);
            Assert.NotNull(response.Review.MangaReviews);
            // TODO: Maybe more
        }

        [Test()]
        public void GetRecentThreadsTest()
        {
            var response = Browser.GetRecentThreads(0);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Feed);
            // TODO: Maybe more
        }

        [Test()]
        public void GetNewThreadsTest()
        {
            var response = Browser.GetNewThreads(0);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Feed);
            // TODO: Maybe more
        }

        [Test()]
        public void GetThreadsByTagTest()
        {
            var response = Browser.GetThreadsByTag(0);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Feed);
            // TODO: Maybe more
        }

        [Test()]
        public void GetThreadTest()
        {
            var response = Browser.GetThread(1802);
            Assert.AreEqual(HttpStatusCode.OK, response.RestResponse.StatusCode);
            Assert.NotNull(response.Feed);
            // TODO: Maybe more
        }
    }
}