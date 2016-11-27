using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifiedAnime.Other
{
    public class ScoreConverter
    {
        private const int HummingBirdScores = 11;
        private const int HummingBirdSplits = HummingBirdScores + 1;
        private const double HummingBirdSplitSize = 100.0 / HummingBirdSplits;

        public static double UnifiedToHummingBird(double value)
        {
            for (var i = 1; i < HummingBirdSplits; i++)
                if (((i - 1) * HummingBirdSplitSize <= value) && (value <= i * HummingBirdSplitSize))
                    return (i - 1) * 0.5;

            return 0.0;
        }

        public static double HummingBirdToUnified(double value)
        {
            return value * 20;
        }
    }
}
