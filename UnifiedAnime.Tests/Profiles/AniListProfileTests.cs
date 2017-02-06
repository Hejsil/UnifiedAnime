using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using NUnit.Framework;
using UnifiedAnime.Clients.Browsers.AniList;
using UnifiedAnime.Clients.Profiles.AniList;
using UnifiedAnime.Data.AniList;
using UnifiedAnime.Data.Common;
using UnifiedAnime.Other;
using UnifiedAnime.Tests.Properties;

namespace UnifiedAnime.Tests.Profiles
{
    [TestFixture()]
    public class AniListProfileTests
    {
        public AniListBrowser Browser { get; }
        public AniListProfile Profile { get; }

        public AniListProfileTests()
        {
            Browser = new AniListBrowser(Resources.AniListClientId, Resources.AniListClientSecret);
            var response = Browser.Authorize();
            Assert.AreEqual(UnifiedStatus.Success, response.Status);

            Profile = new AniListProfile(Resources.AniListClientId, Resources.AniListClientSecret, "");
            response = Profile.AuthenticateWithPin(Resources.AniListProfilePin);
            Assert.AreEqual(UnifiedStatus.Success, response.Status);
        }

        [Test()]
        public void AniListProfileTest()
        {
        }

        [Test()]
        public void AuthenticateTest()
        {
        }

        [Test()]
        public void CreateActivityStatusTest()
        {
            const string testMessage =
                "This status was added during a test. It should have been removed once the test has been completed.";

            {
                var response = Profile.CreateActivityStatus(testMessage);
                Assert.AreEqual(UnifiedStatus.Success, response.Status);
            }
            {
                var response = Browser.GetActivity("UnifiedAnimeTestUser");
                Assert.AreEqual(UnifiedStatus.Success, response.Status);
                Assert.NotNull(response.Data);

                var activities = response.Data.Where(act => act.Value == testMessage).ToArray();
                Assert.AreEqual(activities.Length, 1);

                foreach (var act in activities)
                {
                    Profile.RemoveActivity(act.Id);
                }
            }
        }

        [Test()]
        public void CreateActivityMessageTest()
        {
            const string testMessage =
                "This status was added during a test. It should have been removed once the test has been completed.";

            {
                var response = Profile.CreateActivityMessage(testMessage, Browser.GetUser("hejsil").Data.Id);
                Assert.AreEqual(UnifiedStatus.Success, response.Status);
            }
            {
                var response = Browser.GetActivity("hejsil");
                Assert.AreEqual(UnifiedStatus.Success, response.Status);
                Assert.NotNull(response.Data);

                var activities = response.Data.Where(act => act.Value == testMessage).ToArray();
                Assert.AreEqual(activities.Length, 1);

                foreach (var act in activities)
                {
                    Profile.RemoveActivity(act.Id);
                }
            }
        }

        [Test()]
        public void CreateActivityReplyTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveActivityTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveActivityReplyTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ToggleFollowTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetAiringAnimesTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateAnimeEntryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditAnimeEntryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateMangaEntryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditAnimeEntryTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveAnimeEntryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveMangaEntryTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ToggleFavouriteAnimeTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ToggleFavouriteMangaTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ToggleFavouriteCharacterTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ToggleFavouriteStaffTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RateAnimeReviewTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RateMangaReviewTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateAnimeReviewTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditAnimeReviewTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateMangaReviewTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditMangaReviewTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveAnimeReviewTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void RemoveMangaReviewTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateThreadTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditThreadTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteThreadTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ToggleThreadSubscriptionTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CreateThreadCommentTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EditThreadCommentTest()
        {
            Assert.Fail();
        }
    }
}