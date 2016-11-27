using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Data
{
    public interface IAnimeEntry
    {
        #region Properties

        int Id { get; }

        EntryStatus Status { get; set; }

        int EpisodesWatched { get; set; }

        double Score { get; set; }

        bool Rewatching { get; set; }

        int RewatchTimes { get; set; }

        bool Private { get; set; }

        string Notes { get; set; }

        IAnimeInfo Info { get; }

        #endregion
    }
}