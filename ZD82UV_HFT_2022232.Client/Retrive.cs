using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZD82UV_HFT_2022232.Client
{
    public class Retrive
    {
        public class BestSo
        {
            public BestSo()
            {
            }

            public string SongName { get; set; }
            public int SongCount { get; set; }
            public int Rate { get; set; }
        }

        public class Topla
        {
            public Topla()
            {
            }

            public string LabelName { get; set; }
            public int SongCount { get; set; }
            public double Revenu { get; set; }
        }

        public class YearInfo
        {
            public YearInfo()
            {
            }

            public int Year { get; set; }
            public double AvgRating { get; set; }
            public int SongNumber { get; set; }

            public override bool Equals(object obj)
            {
                YearInfo b = obj as YearInfo;
                if (b == null)
                {
                    return false;
                }
                else
                {
                    return this.Year == b.Year
                        && this.AvgRating == b.AvgRating
                        && this.SongNumber == b.SongNumber;
                }
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(this.Year, this.AvgRating, this.SongNumber);
            }
        }

        public class LabelReve
        {
            public LabelReve()
            {
            }

            public string LabelName { get; set; }
            public int SongCount { get; set; }

            public double Revenu { get; set; }
        }
        public class MostSo
        {
            public MostSo()
            {
            }

            public string SongName { get; set; }
            public int SongNumber { get; set; }
        }
    }

}
