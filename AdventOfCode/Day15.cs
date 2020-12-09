using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day15
    {
        public void DoIt()
        {
            //var discs = new[]
            //{
            //    new Disc(5, 4), new Disc(2, 1),
            //};

            var discs = _input.SplitLines().Select(l => new Disc(l)).Concat(new [] {new Disc(11,0), }).ToArray();


            for (int time = 0;; time++)
            {
                bool fail = false;
                for (int disc = 0; disc < discs.Length; disc++)
                {
                    if (discs[disc].IsAt0(time + disc + 1) == false)
                    {
                        fail = true;
                        break;
                    }
                }
                if (!fail)
                {
                    Tools.PostResult(time);
                    return;
                }
            }
        }

        class Disc
        {
            private int _posAtTime0;
            private int _numPoses;

            public Disc(string text)
            {
                var regEx = new Regex(@"Disc #\d has (\d+) positions; at time=0, it is at position (\d+).");
                var m = regEx.Match(text);
                if(!m.Success)
                    throw  new Exception("Knas");
                _numPoses = m.Groups[1].Value.ParseInt();
                _posAtTime0 = m.Groups[2].Value.ParseInt();
            }

            public Disc(int numPoses, int posAt0)
            {
                _numPoses = numPoses;
                _posAtTime0 = posAt0;
            }

            public bool IsAt0(int time)
            {
                return (_posAtTime0 + time)%_numPoses == 0;
            }
        }

        private const string _input = @"Disc #1 has 17 positions; at time=0, it is at position 5.
Disc #2 has 19 positions; at time=0, it is at position 8.
Disc #3 has 7 positions; at time=0, it is at position 1.
Disc #4 has 13 positions; at time=0, it is at position 7.
Disc #5 has 5 positions; at time=0, it is at position 1.
Disc #6 has 3 positions; at time=0, it is at position 0.";
    }
}