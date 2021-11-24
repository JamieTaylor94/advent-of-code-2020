using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace AdventOfCode
{
    internal class DayFour
    {
        private readonly string[] _data;

        public DayFour()
        {
            _data = ResourceRetriever.SplitTextByEmptyLine(4).ToArray()
                .Select(x => Regex.Replace(x, @"\r\n|\n", " "))
                .ToArray();
        }

        [Test]
        public void PartOne()
        {
            var validPassports = _data.Select(x => new Passport(x))
                .Count(password => new PassportValidator(password).IsValid);
            Assert.AreEqual(260, validPassports);
        }


        [Test]
        public void PartTwo()
        {
            var validPassports = _data.Select(x => new Passport(x))
                .Count(password => new PassportValidator(password).IsValidForPartTwo);
            Assert.AreEqual(153, validPassports);
        }
    }

    internal class PassportValidator
    {
        private readonly Passport _passport;

        public PassportValidator(Passport passport)
        {
            _passport = passport;
        }

        public bool IsValid => _passport.BirthYear > 0
                               && _passport.IssueYear > 0
                               && _passport.ExpirationYear > 0
                               && string.IsNullOrEmpty(_passport.PassportId) == false
                               && string.IsNullOrEmpty(_passport.Height) == false
                               && string.IsNullOrEmpty(_passport.HairColour) == false
                               && string.IsNullOrEmpty(_passport.EyeColour) == false;

        public bool IsValidForPartTwo => (_passport.BirthYear >= 1920 && _passport.BirthYear <= 2002)
                                         && (_passport.IssueYear >= 2010 && _passport.IssueYear <= 2020)
                                         && (_passport.ExpirationYear >= 2020 && _passport.ExpirationYear <= 2030)
                                         && ValidateHeight()
                                         && ValidateHairColour()
                                         && ValidateEyeColour()
                                         && ValidatePassportId();

        private bool ValidateHeight()
        {
            if (string.IsNullOrEmpty(_passport.Height)) return false;

            var hasValidMeasurement = false;

            int.TryParse(_passport.Height.Substring(0, _passport.Height.Length - 2), out var number);

            if (_passport.Height.Contains("cm"))
            {
                hasValidMeasurement = number >= 150 && number <= 193;
            }
            else if (_passport.Height.Contains("in"))
            {
                hasValidMeasurement = number >= 59 && number <= 76;
            }

            return hasValidMeasurement;
        }

        private bool ValidateHairColour()
        {
            if (string.IsNullOrEmpty(_passport.HairColour)) return false;

            var startsWithHash = _passport.HairColour[0] == '#';

            var regex = new Regex("^[a-zA-Z0-9]*$");
            var alphanumerics = _passport.HairColour.Substring(1);

            return startsWithHash
                   && alphanumerics.Length == 6
                   && regex.IsMatch(alphanumerics);
        }

        private bool ValidateEyeColour()
        {
            if (string.IsNullOrEmpty(_passport.EyeColour)) return false;

            var colours = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            return colours.Contains(_passport.EyeColour.ToLower());
        }

        private bool ValidatePassportId()
        {
            if (string.IsNullOrEmpty(_passport.PassportId)) return false;

            var isNumber = int.TryParse(_passport.PassportId, out _);

            return isNumber
                   && _passport.PassportId.Length == 9;
        }
    }

    internal class Passport
    {
        private readonly Dictionary<string, string> _keyValuePairs;

        public Passport(string input)
        {
            _keyValuePairs = ParseInput(input);
        }

        public int BirthYear => GetNumberValueFor("byr");

        public int IssueYear => GetNumberValueFor("iyr");

        public int ExpirationYear => GetNumberValueFor("eyr");

        public int CountryId => GetNumberValueFor("cid");

        public string PassportId => TryGetValueFor("pid");

        public string Height => TryGetValueFor("hgt");

        public string HairColour => TryGetValueFor("hcl");

        public string EyeColour => TryGetValueFor("ecl");

        private Dictionary<string, string> ParseInput(string input)
        {
            var dictionary = new Dictionary<string, string>();

            var kvps = input.Split(' ');

            foreach (var kvp in kvps)
            {
                var parts = kvp.Split(':');

                if (parts.All(pt => pt.Length > 0))
                {
                    dictionary.Add(parts[0], parts[1]);
                }
            }

            return dictionary;
        }

        private int GetNumberValueFor(string key)
        {
            var value = TryGetValueFor(key);
            int.TryParse(value, out var result);
            return string.IsNullOrEmpty(value) ? 0 : result;
        }

        private string TryGetValueFor(string key)
        {
            return _keyValuePairs.TryGetValue(key, out var value)
                ? value
                : string.Empty;
        }
    }
}