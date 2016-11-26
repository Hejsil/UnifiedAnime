using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifiedAnime.Data.Common;

namespace UnifiedAnime.Data
{
    public interface IAnimeEntry
    {
        int Id { get; set; }
        int EpisodesWatched { get; set; }
        int RewatchTimes { get; set; }
        bool Rewatching { get; set; }
        bool Private { get; set; }
        AnimeStatus Status { get; set; }
        double Score { get; set; }
        string Notes { get; set; }
    }
}
