using System.Text;
using System.Text.RegularExpressions;

namespace StringLib;

public static class TextUtil
{
    public static List<string> SplitIntoWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return [];
        }

        // Регулярное выражение для поиска слов:
        // - Слово начинается и заканчивается на букву.
        // - Может содержать апострофы и дефисы внутри.
        // - Не содержит чисел или знаков препинания.
        const string pattern = @"\p{L}+(?:[\-\']\p{L}+)*";
        Regex regex = new(pattern, RegexOptions.Compiled);

        return regex.Matches(text)
            .Select(match => match.Value)
            .ToList();
    }

    public static string CapitalizeWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        List<string> words = SplitIntoWords(text);
        foreach(string word in words)
        {
            StringBuilder sb = new StringBuilder(word);
            sb[0] = char.ToUpper(sb[0]);
            text.Replace(word, sb.ToString());
        }

        return text;
    }
}