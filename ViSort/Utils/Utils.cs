using System;

namespace ViSort
{
    internal static class Utils
    {
        internal static readonly int MAX_SCORE = 999;
        internal static readonly double SCORE_SENSITIVITY = 0.01;

        internal static int CalcScore(int steps, int questions, int answered)
        {
            return Convert.ToInt32(MAX_SCORE / (1 + (steps * SCORE_SENSITIVITY * (questions / answered))));
        }
    }
}