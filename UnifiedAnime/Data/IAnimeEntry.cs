using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Data
{
    public interface IAnimeEntry
    {
        #region Properties

        int EpisodesWatched { get; set; }
        int Id { get; set; }
        string Notes { get; set; }
        bool Private { get; set; }
        bool Rewatching { get; set; }
        int RewatchTimes { get; set; }
        double Score { get; set; }
        EntryStatus Status { get; set; }

        #endregion
    }
}