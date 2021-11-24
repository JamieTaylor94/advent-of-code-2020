using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode
{
    internal class DayFive
    {
        private readonly string[] _data;

        public DayFive()
        {
            _data = ResourceRetriever.GetTextForDay(5);
        }

        [Test]
        public void PartOne()
        {
            var seats = _data.Select(SeatIdentifier.GetSeatDetails);
            var largestSeatId = seats.Select(seat => seat.Id).Max();
            Assert.AreEqual(largestSeatId, 926);
        }

        [Test]
        public void PartTwo()
        {
            var seats = _data.Select(SeatIdentifier.GetSeatDetails).ToList();
            var missingSeat = SeatIdentifier.FindMissingSeat(seats);
            Assert.AreEqual(657, missingSeat.Id);
        }
    }

    public class SeatIdentifier
    {
        public static Seat GetSeatDetails(string seatCode)
        {
            var columnStart = 0;
            var columnEnd = 7;
            var rowStart = 0;
            var rowEnd = 127;

            foreach (var character in seatCode)
            {
                if (character == 'F')
                {
                    rowEnd = TakeTopHalf(rowStart, rowEnd);
                }
                else if (character == 'B')
                {
                    rowStart = TakeLowerHalf(rowStart, rowEnd);
                }
                else if (character == 'L')
                {
                    columnEnd = TakeTopHalf(columnStart, columnEnd);
                }
                else if (character == 'R')
                {
                    columnStart = TakeLowerHalf(columnStart, columnEnd);
                }
            }

            return new Seat
            {
                Column = columnStart,
                Row = rowStart,
            };
        }

        public static Seat FindMissingSeat(List<Seat> seats)
        {
            var columnRange = Enumerable.Range(0, 7).ToList();
            var emptySeat = new Seat();

            var rows = seats.GroupBy(x => x.Row);

            foreach (var row in rows)
            {
                var columns = row.Select(x => x.Column);
                var missingFromRange = columnRange.Except(columns).ToList();
               
                if (missingFromRange.Any())
                {
                    emptySeat.Column = missingFromRange.First();
                    emptySeat.Row = row.Select(x => x.Row).First();
                }
            }

            return emptySeat;
        }

        private static int TakeLowerHalf(int start, int end)
        {
            var difference = end - start;
            return (int)Math.Ceiling((double)difference / 2) + start;
        }

        private static int TakeTopHalf(int end, int start)
        {
            var difference = end - start;
            return (int)Math.Floor((double)difference / 2) + start;
        }
    }

    public class Seat
    {
        private const int SeatsPerColumn = 8;

        public int Row { get; set; }
        public int Column { get; set; }
        public int Id => Row * SeatsPerColumn + Column;
    }
}
