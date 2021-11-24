using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace AdventOfCode
{
    [TestFixture]
    internal class DaySeven
    {
        private readonly string[] _data;
        private static IEnumerable<Bag> _bags;

        public DaySeven()
        {
            _data = ResourceRetriever.GetTextForDay(7);
        }
        
        [Test]
        public void PartOne()
        {
            _bags = _data.Select(x => new Bag(x));

            var list = new List<BagColour>();

            foreach (var bag in _bags)
            {
                if (bag.BagColour.Name.ToLower() == "shiny gold")
                {
                    list.Add(bag.BagColour);
                }
                else
                {
                    list.Add(CheckContainedBags(bag.BagColour));
                }
            }

            Assert.AreEqual(0, list.Count(x => x.Name != null));

        }

        private static BagColour CheckContainedBags(BagColour bagColour)
        {
            var foundBag = new BagColour();
            
            var matchingBag = _bags.Select(r => r).FirstOrDefault(c => c.BagColour.Name == bagColour.Name);

            if (matchingBag == null) return null;

            if (matchingBag.BagColour.Name.ToLower() == "shiny gold")
            {
                return bagColour;
            }

            if (matchingBag.ContainedBags.Any())
            {
                foreach (var containedBag in matchingBag.ContainedBags)
                {
                    if (containedBag.Name.ToLower() == "shiny gold")
                    {
                        foundBag = matchingBag.BagColour;
                        break;
                    }
                    else 
                    {
                        CheckContainedBags(containedBag);
                    }
                }
            }
            
            return foundBag;
        }
    }

    internal class Bag
    {
        private readonly string[] _info;

        public Bag(string info)
        {
            _info = info.Split(new[] {"bags contain"}, StringSplitOptions.None);
        }

        public BagColour BagColour => new BagColour {Count = 1, Name = _info[0].Trim()};

        public List<BagColour> ContainedBags => GetContainedBagColours();

        public List<BagColour> GetContainedBagColours()
        {
            var list = new List<BagColour>();
            
            var colourStr = _info[1]
                .Trim()
                .Split(','); 
            
            foreach (var colour in colourStr)
            {
                var count = (int) char.GetNumericValue(colour[0]);
                
                var name = colour.Substring(count.ToString().Length)
                    .Split(new [] {"bag"}, StringSplitOptions.None)[0]
                    .Trim();
                
                list.Add(new BagColour
                {
                    Count = count,
                    Name = name
                });
            }

            return list;
        }
    }

    public class BagColour
    {
        public int Count { get; set; }
        public string Name { get; set; }
    }
}
