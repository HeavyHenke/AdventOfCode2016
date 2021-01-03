using System.Linq;
using System.Text;

namespace AdventOfCode
{
    internal class Day16
    {
        public void DoIt()
        {
            string a;
            string b;

            const int diskSize = 35_651_584;

            a = "10001001100000001";


            while (a.Length < diskSize)
            {
                b = MakeB(a);

                a = a + '0' + b;
            }

            a = a.Substring(0, diskSize);

            string checksum = a;
            while ((checksum.Length&1) == 0)
            {
                var chk2 = new StringBuilder(checksum.Length/2);
                for (int i = 0; i < checksum.Length; i += 2)
                {
                    if (checksum[i] == checksum[i + 1])
                        chk2.Append('1');
                    else
                        chk2.Append('0');
                }

                checksum = chk2.ToString();
            }

            Tools.PostResult(checksum);
        }

        private string MakeB(string a)
        {
            var sb = new StringBuilder(a.Length);
            foreach (var chr in a.Reverse())
            {
                if (chr == '0')
                    sb.Append('1');
                else
                    sb.Append('0');
            }

            return sb.ToString();
        }
    }
}