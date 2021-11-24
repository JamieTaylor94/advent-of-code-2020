using System;
using NUnit.Framework;

namespace AdventOfCode
{
    internal class DayOne
    {
        private readonly int[] _data;

        public DayOne()
        {
            _data = Array.ConvertAll(
                ResourceRetriever.GetTextForDay(1),
                int.Parse);
        }

        [Test]
        public void AddTwoNumbersTogetherToFind2020()
        {
            var numbers = new int[] { };

            for (var i = 0; i < _data.Length - 1; i++)
            {
                for (var j = 0; j < _data.Length; j++)
                {
                    if (_data[i] + j == 2020)
                    {
                        numbers = new[] { _data[i], j };
                    }
                }
            }

            var calculateTotal = numbers[0] * numbers[1];

            Assert.AreEqual(1020099, calculateTotal);
        }

        [Test]
        public void AddThreeNumbersTogetherToFind2020()
        {
            var numbers = new int[] { };

            for (var i = 0; i < _data.Length - 1; i++)
            {
                for (var j = 0; j < _data.Length; j++)
                {
                    for (var k = 0; k < _data.Length; k++)
                    {
                        if (_data[i] + _data[j] + _data[k] == 2020)
                        {
                            numbers = new[] { _data[i], _data[j], _data[k] };
                        }
                    }
                }
            }

            var calculateTotal = numbers[0] * numbers[1] * numbers[2];


            Assert.AreEqual(49214880, calculateTotal);
        }
        
    }
}