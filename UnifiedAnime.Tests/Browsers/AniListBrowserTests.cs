using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using UnifiedAnime.Clients.Browsers.AniList;
using UnifiedAnime.Data.AniList;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Tests.Properties;

namespace UnifiedAnime.Tests.Browsers
{
    [TestFixture()]
    public class AniListBrowserTests
    {
        public AniListBrowser Browser { get; } 

        public AniListBrowserTests()
        {
            Browser = new AniListBrowser();
            Browser.Authenticate(Resources.AniListClientId, Resources.AniListClientSecret);
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetUserTest(object displayName)
        {
            var response = Browser.GetUser(displayName.ToString());
            response.Status.ShouldBe(ResponseStatus.Success);
            response.Data.ShouldNotBeNull();

            var user = response.Data;
            user.About.ShouldBe("This is a test user for the library UnifiedAnime:\nhttps://github.com/Hejsil/UnifiedAnime");
            user.AdultContent.ShouldBe(true);
            user.AdvancedRating.ShouldBe(true);
            user.AdvancedRatingNames.ShouldContain("TestRating1");
            user.AdvancedRatingNames.ShouldContain("TestRating2");
            user.AdvancedRatingNames.ShouldContain("TestRating3");
            user.AnimeTime.ShouldBe(1571);
            user.CustomListAnime.ShouldContain("TestCustomAnimeList1");
            user.CustomListAnime.ShouldContain("TestCustomAnimeList2");
            user.CustomListAnime.ShouldContain("TestCustomAnimeList3");
            user.CustomListAnime.ShouldNotContain("TestCustomAnimeList4");
            user.CustomListManga.ShouldContain("TestCustomMangaList1");
            user.CustomListManga.ShouldContain("TestCustomMangaList2");
            user.CustomListManga.ShouldContain("TestCustomMangaList3");
            user.CustomListManga.ShouldNotContain("TestCustomMangaList4");
            user.DisplayName.ShouldBe("UnifiedAnimeTestUser");
            user.Id.ShouldBe(84039);
            user.ImageUrlBanner.ShouldBe("https://cdn.anilist.co/img/dir/userbanner/84039-eOyAb7CU6LoL.png");
            user.ImageUrlLge.ShouldBe("https://cdn.anilist.co/img/dir/user/reg/default.png");
            user.ImageUrlMed.ShouldBe("https://cdn.anilist.co/img/dir/user/sml/default.png");
            user.ListOrder.ShouldBe(ListOrder.Alphabet);
            user.Notifications.ShouldBe(0);
            user.ScoreType.ShouldBe(ScoreSystem.Star5);
            //user.MangaChap.ShouldBe(158); TODO: Don't know why this fails
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetActivityTest(object displayName)
        {
            var response = Browser.GetActivity(displayName.ToString());
            response.Status.ShouldBe(ResponseStatus.Success);
            response.Data.ShouldNotBeNull();

            var activities = response.Data;
            activities.Length.ShouldBe(16);

            // TODO: This will be hard to test, because the activity might change all the time,
            //       and i don't want to fix it every time it happens.
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetFollowersTest(object displayName)
        {
            var response = Browser.GetFollowers(displayName.ToString());
            response.Status.ShouldBe(ResponseStatus.Success);
            response.Data.ShouldNotBeNull();

            var followers = response.Data;
            followers.Length.ShouldBe(1);
            var follower = followers[0];
            follower.DisplayName.ShouldBe("hejsil");
            follower.ImageUrlLge.ShouldBe("https://cdn.anilist.co/img/dir/user/reg/72340-aKJVAlZWuTRE.png");
            follower.ImageUrlMed.ShouldBe("https://cdn.anilist.co/img/dir/user/sml/72340-aKJVAlZWuTRE.png");
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetFollowingTest(object displayName)
        {
            var response = Browser.GetFollowing(displayName.ToString());
            response.Status.ShouldBe(ResponseStatus.Success);
            response.Data.ShouldNotBeNull();

            var followers = response.Data;
            followers.Length.ShouldBe(1);
            var follower = followers[0];
            follower.DisplayName.ShouldBe("hejsil");
            follower.ImageUrlLge.ShouldBe("https://cdn.anilist.co/img/dir/user/reg/72340-aKJVAlZWuTRE.png");
            follower.ImageUrlMed.ShouldBe("https://cdn.anilist.co/img/dir/user/sml/72340-aKJVAlZWuTRE.png");
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetFavouritesTest(object displayName)
        {
            var response = Browser.GetFavourites(displayName.ToString());
            response.Status.ShouldBe(ResponseStatus.Success);
            response.Data.ShouldNotBeNull();

            var favorites = response.Data;
            favorites.Anime.Length.ShouldBe(1);
            favorites.Manga.ShouldBeNull();
            favorites.Character.ShouldBeNull();
            favorites.Staff.ShouldBeNull();

            var favAnimes = favorites.Anime;
            favAnimes[0].TitleEnglish.ShouldBe("Steins;Gate");

            // TODO: Should probably cover more
        }

        [Test()]
        public void SearchUserTest()
        {
            var response = Browser.SearchUser("UnifiedAnimeTestUser");
            response.Status.ShouldBe(ResponseStatus.Success);
            response.Data.ShouldNotBeNull();

            var users = response.Data;
            users.Length.ShouldBe(1);
            var user = users[0];
            user.DisplayName.ShouldBe("UnifiedAnimeTestUser");
            user.ImageUrlLge.ShouldBe("https://cdn.anilist.co/img/dir/user/reg/default.png");
            user.ImageUrlMed.ShouldBe("https://cdn.anilist.co/img/dir/user/sml/default.png");
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetAnimelistTest(object displayName)
        {
            var response = Browser.GetAnimelist(displayName.ToString());
            response.Status.ShouldBe(ResponseStatus.Success);
            response.Data.ShouldNotBeNull();

            var entries = response.Data;
            entries.ShouldNotBeNull();
            
            entries.ShouldContain(entry => entry.Anime.TitleEnglish == "Attack on Titan");
            var entry1 = entries.First(entry => entry.Anime.TitleEnglish == "Attack on Titan");
            entry1.ListStatus.ShouldBe(AnimeEntryStatus.Watching);
            entry1.EpisodesWatched.ShouldBe(15);
            entry1.Rewatched.ShouldBe(0);
            entry1.Score.ShouldBe(3);
            entry1.ScoreRaw.ShouldBe(50);
            // entry1.Priority.ShouldBe(); // TODO: Add this when i actually know what it is used for
            entry1.Private.ShouldBe(false);
            entry1.HiddenDefault.ShouldBe(false);
            entry1.Notes.ShouldBe("This is a test note");
            entry1.AdvancedRatingScores.Length.ShouldBe(3);
            entry1.AdvancedRatingScores[0].ShouldBe(2);
            entry1.AdvancedRatingScores[1].ShouldBe(4);
            entry1.AdvancedRatingScores[2].ShouldBe(0);
            entry1.CustomLists.ShouldContain(1);
            entry1.CustomLists.ShouldNotContain(2);
            entry1.CustomLists.ShouldNotContain(3);
            entry1.StartedOn.ShouldBe(new DateTime(2015, 12, 1)); 
            entry1.FinishedOn.ShouldBe(new DateTime(2015, 12, 5)); 



            entries.ShouldContain(entry => entry.Anime.TitleEnglish == "Death Note");
            entries.ShouldContain(entry => entry.Anime.TitleEnglish == "Steins;Gate");
            entries.ShouldContain(entry => entry.Anime.TitleEnglish == "Angel Beats!");
            entries.ShouldContain(entry => entry.Anime.TitleEnglish == "Sword Art Online");
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetMangalistTest(object displayName)
        {
            Assert.Fail();
        }

        [Test()]
        public void GetBrowseAnimeTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetBrowseMangaTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SearchAnimeTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SearchMangaTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SearchCharacterTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SearchStaffTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SearchStudioTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SearchThreadTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetStaffTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetStaffPageTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetStudioTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetStudioPageTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetReviewTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetAnimeReviewsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetMangaReviewsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetUserReviewsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetUserReviewsTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetRecentThreadsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetNewThreadsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetThreadsByTagTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetThreadTest()
        {
            Assert.Fail();
        }
    }
}