using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedAnime.Data
{
    public interface IAnimeInfo
    {
        string Url { get; set; }
        string Title { get; set; }
        int EpisodeCount { get; set; }
        string ImageUrl { get; set; }
        string Synopsis { get; set; }
        double Rating { get; set; }
    }
}
