using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode
{
    internal class DayThree
    {
        private readonly string[] _data;

        public DayThree()
        {
            _data = ResourceRetriever.GetTextForDay(3).ToArray();
        }

        [Test]
        public void PartOne()
        {
            var result = GetTreesForPathway(3, 1);
            Assert.AreEqual(173, result);
        }

        [Test]
        public void PartTwo()
        {
            var slopes = new List<int[]>()
            {
                new[] {1, 1},
                new[] {3, 1},
                new[] {5, 1},
                new[] {7, 1},
                new[] {1, 2},
            };

            var results = slopes.Select(slope => GetTreesForPathway(slope[0], slope[1])).ToList();

            var total = results.Aggregate<int, long>(1, (current, result) => current * result);

            Assert.AreEqual(total, 4385176320);
        }

        private int GetTreesForPathway(int right, int down)
        {
            var totalColumns = _data.First().Length;

            var column = right;
            var trees = 0;

            for (var row = down; row < _data.Length; row += down)
            {
                if (_data[row][column] == '#')
                {
                    trees++;
                }

                column += right;

                if (column >= totalColumns)
                {
                    column = column - totalColumns;
                }
            }

            return trees;
        }
    }
}