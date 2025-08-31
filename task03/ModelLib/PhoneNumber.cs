using System.Text.RegularExpressions;

namespace ModelLib;
public class PhoneNumber
{
    private string _number;
    private string _extension;

    public PhoneNumber(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException("Null or empty string is not allowed");
        }

        text = text.Replace(" ", "");
        text = text.Replace("-", "");
        text = text.Replace("(", "");
        text = text.Replace(")", "");

        Regex phoneNumberRegex = new Regex(@"^\+?\d{2,15}(?:x\d{1,4})?$", RegexOptions.Compiled);
        Regex numberRegex = new Regex(@"^\+?\d{2,15}", RegexOptions.Compiled);
        Regex extensionRegex = new Regex(@"x\d{1,4}?$", RegexOptions.Compiled);

        if (phoneNumberRegex.Matches(text).Count > 0)
        {
            // нужно сделать сохранение номера без плюса в начале и доп номера без х в начале
            _number = numberRegex.Match(text).ToString();
            if (_number[0] == '+')
            {
                _number = _number.Remove(0, 1);
            }

            if (extensionRegex.Matches(text).Count > 0)
            {
                _extension = extensionRegex.Match(text).ToString();
                _extension = _extension.Remove(0, 1);
            }
            else
            {
                _extension = "";
            }
        }
        else
        {
            throw new ArgumentException("Wrong phone number format");
        }
    }

    public override string ToString()
    {
        return (_extension != "") ? "+" + _number + "x" + _extension : "+" + _number;
    }

    public string Number()
    {
        return "+" + _number;
    }

    public string Extension()
    {
        return _extension;
    }
}