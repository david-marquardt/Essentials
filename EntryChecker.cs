using System.Linq;
using System.Text.RegularExpressions;

namespace Essentials
{
    public static class EntryChecker
    {
        public static bool IsName(string name, string type = "Name")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es wurde kein " + type + " eingetragen", "OK");

                return false;
            }

            if (name.Any(char.IsDigit))
            {
                // contains digit
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Der " + type + " kann keine Zahl enthalten", "OK");
                return false;
            }

            Regex rx = new Regex(@"^([A-Za-zöüäÖÜÄ]+([ ]?[a-zöüäß]?['-]?[A-Za-zöüäÖÜÄ]+)*)$",
              RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (!rx.IsMatch(name))
            {
                // contains digit
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es wurde kein " + type + " eingetragen", "OK");
                return false;
            }

            return true;
        }
        public static bool IsNameOrEmpty(string name, string type = "Name")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return true;
            }

            if (name.Any(char.IsDigit))
            {
                // contains digit
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Der " + type + " kann keine Zahl enthalten", "OK");
                return false;
            }

            Regex rx = new Regex(@"^([A-Za-zöüäÖÜÄ]+([ ]?[a-zöüäß]?['-]?[A-Za-zöüäÖÜÄ]+)*)$",
              RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (!rx.IsMatch(name))
            {
                // contains digit
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es wurde kein " + type + " eingetragen", "OK");
                return false;
            }

            return true;
        }

        public static bool IsPostCode(string postCode, int length = 5, bool CanBeEmpty = false)
        {
            if (string.IsNullOrWhiteSpace(postCode))
            {
                if (!CanBeEmpty)
                {
                    Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es wurde keine Postleitzahl eingetragen", "OK");
                }

                return CanBeEmpty;
            }

            if (postCode.Any(char.IsLetter))
            {
                // contains digit
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Die Postleitzahl kann keinen Buchstaben enthalten", "OK");
                return false;
            }

            if (postCode.Length != length)
            {
                // contains digit
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Die Postleitzahl muss " + length.ToString() + " Stellen enthalten", "OK");
                return false;
            }


            return true;
        }

        public static bool IsHouseNo(string houseNo, bool CanBeEmpty = false)
        {
            // Housenumbers can contain numbers and letters

            if (string.IsNullOrWhiteSpace(houseNo))
            {
                if (!CanBeEmpty)
                {
                    Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es wurde keine Hausnummer eingetragen", "OK");
                }

                return CanBeEmpty;
            }

            Regex rx = new Regex(@"^([0-9]*[-]?[a-zA-Z]*?)*$",
              RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (!rx.IsMatch(houseNo))
            {
                // contains digit
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es ist keine Hausnummer", "OK");
                return false;
            }


            return true;
        }

        public static bool IsPosIntOrZero(string number, bool CanBeEmpty = false)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                if (!CanBeEmpty)
                {
                    Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es wurde keine Zahl eingetragen", "OK");
                }

                return CanBeEmpty;
            }

            if (!int.TryParse(number, out int no))
            {
                // contains digit
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es wurde keine ganze Zahl eingetragen", "OK");
                return false;
            }

            if (no < 0)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Die Zahl muss positiv oder 0 sein", "OK");
                return false;
            }

            return true;
        }

        public static bool IsPosDoubleOrZero(string number, bool CanBeEmpty = false)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                if (!CanBeEmpty)
                {
                    Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es wurde keine Zahl eingetragen", "OK");
                }

                return CanBeEmpty;
            }

            if (!double.TryParse(number, out double no))
            {
                // contains digit
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Es wurde keine ganze Zahl eingetragen", "OK");
                return false;
            }

            if (no < 0)
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Fehler", "Die Zahl muss positiv oder 0 sein", "OK");
                return false;
            }

            return true;
        }

        public static string CleanString(string text)
        {
            return text.Trim();
        }
    }
}
