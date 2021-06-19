using System.Linq;

namespace MarsRoverChallenge.ConsoleApp.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveWhitespace(this string input)
        {
            if (input == null) 
            {
                return null;
            }

            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
