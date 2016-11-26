namespace UnifiedAnime.Data
{
    public interface IAnimeInfo
    {
        #region Properties

        string ShowType { get; set; }
        int Id { get; set; }
        int EpisodeCount { get; set; }
        string ImageUrl { get; set; }
        double Score { get; set; }
        string Synopsis { get; set; }
        string Title { get; set; }
        string Url { get; set; }

        #endregion
    }
}