using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sodv2202_assign2
{
    public class PlayerStats
    {
        public string Name;
        public string Team;
        public string Pos;
        public int GP;
        public int G;
        public int A;
        public int P;
        public int PlusMinus;
        public int PIM;
        public double PGP;
        public int PPG;
        public int PPP;
        public int SHG;
        public int SHP;
        public int GWG;
        public int OTG;
        public int S;
        public double Spercent;
        public string TOIperGP;
        public double ShiftsPerGP;
        public double FOWpercent;

        public PlayerStats(string csv)
        {
            int index = 0;
            Name = NextValue(csv, ref index);
            Team = NextValue(csv, ref index);
            Pos = NextValue(csv, ref index);
            int.TryParse(NextValue(csv, ref index), out GP);
            int.TryParse(NextValue(csv, ref index), out G);
            int.TryParse(NextValue(csv, ref index), out A);
            int.TryParse(NextValue(csv, ref index), out P);
            int.TryParse(NextValue(csv, ref index), out PlusMinus);
            int.TryParse(NextValue(csv, ref index), out PIM);
            double.TryParse(NextValue(csv, ref index), out PGP);
            int.TryParse(NextValue(csv, ref index), out PPG);
            int.TryParse(NextValue(csv, ref index), out PPP);
            int.TryParse(NextValue(csv, ref index), out SHG);
            int.TryParse(NextValue(csv, ref index), out SHP);
            int.TryParse(NextValue(csv, ref index), out GWG);
            int.TryParse(NextValue(csv, ref index), out OTG);
            int.TryParse(NextValue(csv, ref index), out S);
            double.TryParse(NextValue(csv, ref index), out Spercent);
            TOIperGP = NextValue(csv, ref index);
            double.TryParse(NextValue(csv, ref index), out ShiftsPerGP);
            double.TryParse(NextValue(csv, ref index), out FOWpercent);

        }

        private string NextValue(string csv, ref int index)
        {
            string result = "";
            if (index < csv.Length)
            {
                if (csv[index] == ',')
                {
                    index++;
                }
                else if (csv[index] == '"')
                {
                    int endIndex = csv.IndexOf('"', index + 1);
                    result = csv.Substring(index + 1, endIndex - index + 1);
                    index = endIndex + 2;
                }
                else
                {
                    int endIndex = csv.IndexOf(',', index);
                    if (endIndex == -1) result = csv.Substring(index);
                    else result = csv.Substring(index, endIndex - index);
                    index = endIndex + 1;
                }
            }
            return result;
        }
    }
}
