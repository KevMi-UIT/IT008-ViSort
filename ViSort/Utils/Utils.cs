using System;

namespace ViSort
{
    internal static class Utils
    {
        internal static readonly int MaxScore = 999;
        internal static readonly double ScoreSensitive = 0.01;
        internal static int CalcScore(int steps)
        {
            return Convert.ToInt32(MaxScore / (1 + steps * ScoreSensitive));
        }
    }
}
