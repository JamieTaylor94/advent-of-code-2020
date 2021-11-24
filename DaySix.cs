using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace AdventOfCode
{
    internal class DaySix
    {
        private readonly IEnumerable<string> _data;
        public DaySix()
        {
            _data = ResourceRetriever.SplitTextByEmptyLine(6)
                .Select(x => Regex.Replace(x, @"\r\n|\n", " ")); 
        }

        [Test]
        public void PartOne()
        {
            var result = _data.Sum(group => group.Replace(" ", "")
                .Distinct()
                .Count()
            );
            
            Assert.AreEqual(6686, result);
        }

        [Test]
        public void PartTwo()
        {
            var num = _data.Select(WhereAllMembersOfGroupAnswerYes).Sum(x => x.Count());

            Assert.AreEqual(3476, num);
        }

        private static IEnumerable<char> WhereAllMembersOfGroupAnswerYes(string group)
        {
            var people = group.Split(' ');
            return group.Distinct()
                .Where(c => people.All(x => x.Contains(c)));
        }
    }
}