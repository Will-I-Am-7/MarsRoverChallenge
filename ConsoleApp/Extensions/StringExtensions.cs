using System.Linq;

namespace MarsRoverChallenge.ConsoleApp.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes all white space from a string
        /// </summary>
        /// <param name="input">string from which to remove white space</param>
        /// <returns></returns>
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
