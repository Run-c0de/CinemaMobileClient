using System.Globalization;

namespace CinemaMobileClient.Converters
{
  public class CardNumberToColorConverter : CardValidator, IValueConverter
  {
    public Color Amex { get; set; }
    public Color Diners { get; set; }
    public Color Discover { get; set; }
    public Color Jcb { get; set; }
    public Color MasterCard { get; set; }
    public Color Unknown { get; set; }
    public Color Visa { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is null)
        return Unknown;

      var number = value.ToString();
      var normalized = number.Replace("-", string.Empty);

      if (AmexRegex.IsMatch(normalized))
        return Amex;
      else if (DinersRegex.IsMatch(normalized))
        return Diners;
      else if (DiscoverRegex.IsMatch(normalized))
        return Discover;
      else if (JcbRegex.IsMatch(normalized))
        return Jcb;
      else if (MasterRegex.IsMatch(normalized))
        return MasterCard;
      else if (VisaRegex.IsMatch(normalized))
        return Visa;
      else
        return Unknown;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
