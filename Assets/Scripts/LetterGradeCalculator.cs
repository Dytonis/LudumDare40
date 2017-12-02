using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD40
{
    public class LetterGradeCalculator
    {
        public static LetterGrade GetGradeFromScore(float time, float ATime, int shots, int AShots)
        {
            float timescore = (ATime / time) * (float)LetterGrade.A;

            float shotscore = (AShots / shots) * (float)LetterGrade.A;

            float score = (timescore + shotscore) / 2;

            if (score >= 130)
                return LetterGrade.SS;
            else if (score >= 120)
                return LetterGrade.S;
            else if (score >= 110)
                return LetterGrade.AA;
            else if (score >= 100)
                return LetterGrade.A;
            else if (score >= 80)
                return LetterGrade.B;
            else if (score >= 60)
                return LetterGrade.C;
            else if (score >= 40)
                return LetterGrade.D;
            else return LetterGrade.F;
        }
    }

    public enum LetterGrade
    {
        SS = 130,
        S = 120,
        AA = 110,
        A = 100,
        B = 80,
        C = 60,
        D = 40,
        F = 0,
    }
}
