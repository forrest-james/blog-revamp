using System.Text;

namespace Domain.Services
{
    public static class PostTitleEncodingService
    {
        public static string Encode(this string title) => title.RemoveSpecialCharacters().ReduceWhiteSpace().ConvertWhiteSpace().ToLower();

        private static string RemoveSpecialCharacters(this string text)
        {
            var tempString = new StringBuilder();
            foreach(char c in text)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == ' '))
                    tempString.Append(c);
            }
            return tempString.ToString();
        }

        private static string ReduceWhiteSpace(this string text)
        {
            var tempString = new StringBuilder();
            bool consecutiveWhiteSpace = false;
            foreach(char c in text)
            {
                if (char.IsWhiteSpace(c) && consecutiveWhiteSpace)
                    continue;
                else if (char.IsWhiteSpace(c))
                    consecutiveWhiteSpace = true;
                else
                    consecutiveWhiteSpace = false;

                tempString.Append(c);
            }
            return tempString.ToString();
        }

        private static string ConvertWhiteSpace(this string text) => text.Replace(" ", "-");
    }
}