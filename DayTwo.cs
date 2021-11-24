using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode
{
    internal class DayTwo
    {
        private readonly IEnumerable<PasswordPolicy> _data;

        public DayTwo()
        {
            _data = ResourceRetriever.GetTextForDay(2)
                .Select(x => new PasswordPolicy(x));
        }

        [Test]
        public void GetValidLogins_PartOne()
        {
            var validLogins = _data.Count(x =>
            {
                return x.PasswordEntered.Contains(x.RequiredCharacter)
                    && x.PasswordEntered.Count(c => c == x.RequiredCharacter) >= x.LengthRules.Min
                    && x.PasswordEntered.Count(c => c == x.RequiredCharacter) <= x.LengthRules.Max;
            });

            Assert.AreEqual(416, validLogins);
        }

        [Test]
        public void GetValidLogins_PartTwo()
        {
            var validLogins = _data.Count(SingleOccurrenceOfRequiredCharacter);
            Assert.AreEqual(688, validLogins);
        }

        private bool SingleOccurrenceOfRequiredCharacter(PasswordPolicy login)
        {
            return login.PasswordEntered.Contains(login.RequiredCharacter)
                   && login.PasswordEntered[login.LengthRules.Min - 1] == login.RequiredCharacter
                   ^ login.PasswordEntered[login.LengthRules.Max - 1] == login.RequiredCharacter;
        }
    }

    internal class PasswordPolicy
    {
        private readonly string[] _parts;

        public PasswordPolicy(string policy)
        {
            _parts = policy.Split(' ');
        }

        public char RequiredCharacter => _parts[1].ToCharArray().First();

        public string PasswordEntered => _parts[2];

        public LengthRules LengthRules => GetLengthRules();

        private LengthRules GetLengthRules()
        {
            var lengthPart = _parts[0];
            var array = Array.ConvertAll(lengthPart.Split('-'), int.Parse);

            return new LengthRules
            {
                Min = array[0],
                Max = array[1]
            };
        }
    }

    internal class LengthRules
    {
        public int Min { get; set; }
        public int Max { get; set; }

    }
}
