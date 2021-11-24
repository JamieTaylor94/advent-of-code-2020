using System;
using System.IO;

namespace AdventOfCode
{
    public class ResourceRetriever
    {
        public static string[] GetTextForDay(int day)
        {
            var path = $"{Directory.GetCurrentDirectory()}/Resources/day{day}.txt";
            return File.ReadAllLines(path);
        }

        public static string[] SplitTextByEmptyLine(int day)
        {
            var path = $"{Directory.GetCurrentDirectory()}/Resources/day{day}.txt";
            var text = File.ReadAllText(path);
            return text.Split(new[] {"\r\n\r\n"}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}