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
        public AniListBrowser Browser { get; set; } 
            = new AniListBrowser(Resources.AniListClientId, Resources.AniListClientSecret);

        [Test()]
        public void GetUserTest()
        {
            var response = Browser.GetUser("UnifiedAnimeTestUser");
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
            user.ImageUrlLge.ShouldBe("https://cdn.anilist.co/img/dir/user/reg/default.png");
            user.ImageUrlMed.ShouldBe("https://cdn.anilist.co/img/dir/user/sml/default.png");
            user.ImageUrlBanner.ShouldBe("https://cdn.anilist.co/img/dir/userbanner/84039-eOyAb7CU6LoL.png");
            user.ListOrder.ShouldBe(ListOrder.Alphabet);
            user.MangaChap.ShouldBe(158);
        }

        [Test()]
        public void GetUserTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetActivityTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetActivityTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetFollowersTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetFollowersTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetFollowingTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetFollowingTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetFavouritesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetFavouritesTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void SearchUserTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetAnimelistTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetAnimelistTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetMangalistTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetMangalistTest1()
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