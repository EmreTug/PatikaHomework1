namespace PatikaHomework1.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsWord(this string s, string word)
        {
            return s.IndexOf(" " + word + " ") >= 0 ||
                   s.StartsWith(word + " ") ||
                   s.EndsWith(" " + word) ||
                   s.Equals(word);
        }
    }

}
