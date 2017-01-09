using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
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
            Browser = new AniListBrowser(Resources.AniListClientId, Resources.AniListClientSecret);
            Browser.Authenticate();
        }

        [Test()]
        public void AniListBrowserTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void AuthenticateTest()
        {
            Assert.Fail();
        }
        
        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetUserTest(object displayName)
        {
            var response = Browser.GetUser(displayName.ToString());
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var user = response.Data;
            Assert.AreEqual(user.About, "This is a test user for the library UnifiedAnime:\nhttps://github.com/Hejsil/UnifiedAnime");
            Assert.AreEqual(user.AdultContent, true);
            Assert.AreEqual(user.AdvancedRating, true);
            Assert.Contains("TestRating1", user.AdvancedRatingNames);
            Assert.Contains("TestRating2", user.AdvancedRatingNames);
            Assert.Contains("TestRating3", user.AdvancedRatingNames);
            Assert.AreEqual(user.AnimeTime, 1571);
            Assert.Contains("TestCustomAnimeList1", user.CustomListAnime);
            Assert.Contains("TestCustomAnimeList2", user.CustomListAnime);
            Assert.Contains("TestCustomAnimeList3", user.CustomListAnime);
            Assert.IsFalse(user.CustomListAnime.Contains("TestCustomAnimeList4"));
            Assert.Contains("TestCustomMangaList1", user.CustomListManga);
            Assert.Contains("TestCustomMangaList2", user.CustomListManga);
            Assert.Contains("TestCustomMangaList3", user.CustomListManga);
            Assert.IsFalse(user.CustomListManga.Contains("TestCustomMangaList4"));
            Assert.AreEqual(user.DisplayName, "UnifiedAnimeTestUser");
            Assert.AreEqual(user.Id, 84039);
            Assert.AreEqual(user.ImageUrlBanner, "https://cdn.anilist.co/img/dir/userbanner/84039-eOyAb7CU6LoL.png");
            Assert.AreEqual(user.ImageUrlLge, "https://cdn.anilist.co/img/dir/user/reg/default.png");
            Assert.AreEqual(user.ImageUrlMed, "https://cdn.anilist.co/img/dir/user/sml/default.png");
            Assert.AreEqual(user.ListOrder, ListOrder.Alphabet);
            Assert.AreEqual(user.Notifications, 0);
            Assert.AreEqual(user.ScoreType, ScoreSystem.Star5);
            //Assert.AreEqual(user.MangaChap, 158); TODO: Don't know why this fails
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetActivityTest(object displayName)
        {
            var response = Browser.GetActivity(displayName.ToString());
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var activities = response.Data;
            Assert.AreEqual(activities.Length, 16);

            // TODO: This will be hard to test, because the activity might change all the time,
            //       and i don't want to fix it every time it happens.
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetFollowersTest(object displayName)
        {
            var response = Browser.GetFollowers(displayName.ToString());
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var followers = response.Data;
            Assert.AreEqual(followers.Length, 1);
            var follower = followers[0];
            Assert.AreEqual(follower.DisplayName, "hejsil");
            Assert.AreEqual(follower.ImageUrlLge, "https://cdn.anilist.co/img/dir/user/reg/72340-aKJVAlZWuTRE.png");
            Assert.AreEqual(follower.ImageUrlMed, "https://cdn.anilist.co/img/dir/user/sml/72340-aKJVAlZWuTRE.png");
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetFollowingTest(object displayName)
        {
            var response = Browser.GetFollowing(displayName.ToString());
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var followers = response.Data;
            Assert.AreEqual(followers.Length, 1);
            var follower = followers[0];
            Assert.AreEqual(follower.DisplayName, "hejsil");
            Assert.AreEqual(follower.ImageUrlLge, "https://cdn.anilist.co/img/dir/user/reg/72340-aKJVAlZWuTRE.png");
            Assert.AreEqual(follower.ImageUrlMed, "https://cdn.anilist.co/img/dir/user/sml/72340-aKJVAlZWuTRE.png");
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetFavouritesTest(object displayName)
        {
            var response = Browser.GetFavourites(displayName.ToString());
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var favorites = response.Data;
            Assert.AreEqual(favorites.Anime.Length, 1);
            Assert.Null(favorites.Manga);
            Assert.Null(favorites.Character);
            Assert.Null(favorites.Staff);

            var favAnimes = favorites.Anime;
            Assert.AreEqual(favAnimes[0].TitleEnglish, "Steins;Gate");

            // TODO: Should probably cover more
        }

        [Test()]
        public void SearchUserTest()
        {
            var response = Browser.SearchUser("UnifiedAnimeTestUser");
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var users = response.Data;
            Assert.AreEqual(users.Length, 1);
            var user = users[0];
            Assert.AreEqual(user.DisplayName, "UnifiedAnimeTestUser");
            Assert.AreEqual(user.ImageUrlLge, "https://cdn.anilist.co/img/dir/user/reg/default.png");
            Assert.AreEqual(user.ImageUrlMed, "https://cdn.anilist.co/img/dir/user/sml/default.png");
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetAnimelistTest(object displayName)
        {
            var response = Browser.GetAnimelist(displayName.ToString());
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var entries = response.Data;
            Assert.NotNull(entries);

            Assert.IsTrue(entries.Any(entry => entry.Anime.TitleEnglish == "Attack on Titan"));
            var entry1 = entries.First(entry => entry.Anime.TitleEnglish == "Attack on Titan");
            Assert.AreEqual(entry1.ListStatus, AnimeEntryStatus.Watching);
            Assert.AreEqual(entry1.EpisodesWatched, 15);
            Assert.AreEqual(entry1.Rewatched, 0);
            Assert.AreEqual(entry1.Score, 3);
            Assert.AreEqual(entry1.ScoreRaw, 50);
            // Assert.AreEqual(entry1.Priority, ); // TODO: Add this when i actually know what it is used for
            Assert.AreEqual(entry1.Private, false);
            Assert.AreEqual(entry1.HiddenDefault, false);
            Assert.AreEqual(entry1.Notes, "This is a test note");
            Assert.AreEqual(entry1.AdvancedRatingScores.Length, 3);
            Assert.AreEqual(entry1.AdvancedRatingScores[0], 2);
            Assert.AreEqual(entry1.AdvancedRatingScores[1], 4);
            Assert.AreEqual(entry1.AdvancedRatingScores[2], 0);
            Assert.Contains(1, entry1.CustomLists);
            Assert.IsFalse(entry1.CustomLists.Contains(2));
            Assert.IsFalse(entry1.CustomLists.Contains(3));
            Assert.AreEqual(entry1.StartedOn, new DateTime(2015, 12, 1));
            Assert.AreEqual(entry1.FinishedOn, new DateTime(2015, 12, 5));



            Assert.IsTrue(entries.Any(entry => entry.Anime.TitleEnglish == "Death Note"));
            Assert.IsTrue(entries.Any(entry => entry.Anime.TitleEnglish == "Steins;Gate"));
            Assert.IsTrue(entries.Any(entry => entry.Anime.TitleEnglish == "Angel Beats!"));
            Assert.IsTrue(entries.Any(entry => entry.Anime.TitleEnglish == "Sword Art Online"));
        }

        [TestCase("UnifiedAnimeTestUser")]
        [TestCase(84039)]
        public void GetMangalistTest(object displayName)
        {
            var response = Browser.GetMangalist(displayName.ToString());
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var entries = response.Data;
            Assert.NotNull(entries);

            Assert.IsTrue(entries.Any(entry => entry.Manga.TitleEnglish == "Berserk"));
            var entry1 = entries.First(entry => entry.Manga.TitleEnglish == "Berserk");
            Assert.AreEqual(entry1.ListStatus, MangaEntryStatus.Completed);
            Assert.AreEqual(entry1.ChaptersRead, 6);
            Assert.AreEqual(entry1.VolumesRead, 1);
            Assert.AreEqual(entry1.Score, 3);
            Assert.AreEqual(entry1.ScoreRaw, 50);
            // Assert.AreEqual(entry1.Priority, ); // TODO: Add this when i actually know what it is used for
            Assert.AreEqual(entry1.Private, false);
            Assert.AreEqual(entry1.HiddenDefault, false);
            Assert.AreEqual(entry1.Notes, "This is a test note");
            Assert.AreEqual(entry1.AdvancedRatingScores.Length, 3);
            Assert.AreEqual(entry1.AdvancedRatingScores[0], 2);
            Assert.AreEqual(entry1.AdvancedRatingScores[1], 4);
            Assert.AreEqual(entry1.AdvancedRatingScores[2], 0);
            Assert.Contains(1, entry1.CustomLists);
            Assert.IsFalse(entry1.CustomLists.Contains(2));
            Assert.IsFalse(entry1.CustomLists.Contains(3));
            Assert.AreEqual(entry1.StartedOn, new DateTime(2015, 12, 1));
            Assert.AreEqual(entry1.FinishedOn, new DateTime(2015, 12, 5));



            Assert.IsTrue(entries.Any(entry => entry.Manga.TitleEnglish == "Goodnight Punpun"));
            Assert.IsTrue(entries.Any(entry => entry.Manga.TitleEnglish == "One Piece"));
            Assert.IsTrue(entries.Any(entry => entry.Manga.TitleEnglish == "Yotsuba&!"));
            Assert.IsTrue(entries.Any(entry => entry.Manga.TitleEnglish == "Fullmetal Alchemist"));
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
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var animes = response.Data;
            Assert.NotNull(animes);

            for (var i = 0; i < firstAnimes.Length; i++)
            {
                Assert.AreEqual(animes[i].TitleEnglish, firstAnimes[i]);
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
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var mangas = response.Data;
            Assert.NotNull(mangas);

            for (var i = 0; i < firstMangas.Length; i++)
            {
                Assert.AreEqual(mangas[i].TitleEnglish, firstMangas[i]);
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
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var animes = response.Data;
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
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var mangas = response.Data;
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
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var characters = response.Data;
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
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var staff = response.Data;
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
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var studios = response.Data;
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
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var threads = response.Data;
            Assert.NotNull(threads);
            // HACK: We will just assume, for now, that there is always at least one thread about Attack On Titan
            Assert.Greater(threads.Length, 0);
        }

        [Test()]
        public void GetStaffTest()
        {
            var response = Browser.GetStaff(95185);
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var staff = response.Data;
            Assert.NotNull(staff);

            //Assert.AreEqual(staff.Dob, ); TODO: Figure these out
            Assert.AreEqual(staff.Info, 
@"<b>Height</b>: 156 cm<br>
<b>Blood type</b>: AB<br>
<b>Radio Page</b>: http://www.joqr.co.jp/blog/hanazawa/<br>
The Office Osawa talent agency represents her.<br>
<b>Interesting facts</b>:<br>
- She was invited to AFA 2010 on stage.<br>
- She used to be a junior idol in Akiba where hundreds of people came to watch her, which is how she got her breakthrough for her acting career in commercials, before becoming a seiyuu.");
            //Assert.AreEqual(staff.NameFirstJapanese, "Kana"); TODO: Figure these out
            //Assert.AreEqual(staff.NameLastJapanese, "Kana"); TODO: Figure these out
            //Assert.AreEqual(staff.Website, ); TODO: Figure these out
            Assert.AreEqual(staff.Id, 95185);
            Assert.AreEqual(staff.ImageUrlLge, "https://cdn.anilist.co/img/dir/person/reg/185.jpg");
            Assert.AreEqual(staff.ImageUrlMed, "https://cdn.anilist.co/img/dir/person/med/185.jpg");
            Assert.AreEqual(staff.Language, "Japanese");
            Assert.AreEqual(staff.NameFirst, "Kana");
            Assert.AreEqual(staff.NameFirst, "Hanazawa");
            //Assert.AreEqual(staff.Role, ); TODO: Figure these out
        }

        [Test()]
        public void GetStaffPageTest()
        {
            // HACK: What difference is this from GetStaff?
            var response = Browser.GetStaffPage(95185);
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var staff = response.Data;
            Assert.NotNull(staff);

            //Assert.AreEqual(staff.Dob, ); TODO: Figure these out
            Assert.AreEqual(staff.Info,
@"<b>Height</b>: 156 cm<br>
<b>Blood type</b>: AB<br>
<b>Radio Page</b>: http://www.joqr.co.jp/blog/hanazawa/<br>
The Office Osawa talent agency represents her.<br>
<b>Interesting facts</b>:<br>
- She was invited to AFA 2010 on stage.<br>
- She used to be a junior idol in Akiba where hundreds of people came to watch her, which is how she got her breakthrough for her acting career in commercials, before becoming a seiyuu.");
            //Assert.AreEqual(staff.NameFirstJapanese, "Kana"); TODO: Figure these out
            //Assert.AreEqual(staff.NameLastJapanese, "Kana"); TODO: Figure these out
            //Assert.AreEqual(staff.Website, ); TODO: Figure these out
            Assert.AreEqual(staff.Id, 95185);
            Assert.AreEqual(staff.ImageUrlLge, "https://cdn.anilist.co/img/dir/person/reg/185.jpg");
            Assert.AreEqual(staff.ImageUrlMed, "https://cdn.anilist.co/img/dir/person/med/185.jpg");
            Assert.AreEqual(staff.Language, "Japanese");
            Assert.AreEqual(staff.NameFirst, "Kana");
            Assert.AreEqual(staff.NameFirst, "Hanazawa");
            //Assert.AreEqual(staff.Role, ); TODO: Figure these out
        }

        [Test()]
        public void GetStudioTest()
        {
            var response = Browser.GetStudio(2);
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var studio = response.Data;
            Assert.NotNull(studio);

            //Assert.AreEqual(studio.MainStudio, ); TODO: Figure these out
            Assert.AreEqual(studio.StudioName, "Kyoto Animation");
            //Assert.AreEqual(//studio.StudioWiki, ); TODO: Figure these out
        }

        [Test()]
        public void GetStudioPageTest()
        {
            // HACK: What difference is this from GetStudio?
            var response = Browser.GetStudioPage(2);
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var studio = response.Data;
            Assert.NotNull(studio);

            //Assert.AreEqual(//studio.MainStudio, ); TODO: Figure these out
            Assert.AreEqual(studio.StudioName, "Kyoto Animation");
            //Assert.AreEqual(//studio.StudioWiki, ); TODO: Figure these out
        }

        [Test()]
        public void GetAnimeReviewTest()
        {
            var response = Browser.GetAnimeReview(2131);
            Assert.AreEqual(response.Status, UnifiedStatus.Success);
            Assert.NotNull(response.Data);

            var review = response.Data;
            Assert.NotNull(review);

            Assert.AreEqual(review.Anime.TitleEnglish, "Sound! Euphonium 2");
            Assert.AreEqual(review.Date, "Dec 29, 2016"); // TODO: Fix date datatype
            Assert.AreEqual(review.Private, 0); // TODO: Use bool datatype
            Assert.AreEqual(review.Rating, 80); // TODO: Rating vs Score?
            Assert.AreEqual(review.Score, 80); // TODO: Rating vs Score?
            //Assert.AreEqual(review.RatingAmount, ); TODO: What is this?
            Assert.AreEqual(review.Summary, "Sound! S2 takes the stage set-up by S1 and tells a compelling story about hardships of performing and growing up");
            Assert.AreEqual(review.User.DisplayName, "WillQ");
            //Assert.AreEqual(review.UserRating, ); TODO: What is this ?
            Assert.AreEqual(review.Id, 2131);
            
        }

        [Test()]
        public void GetMangaReviewTest()
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