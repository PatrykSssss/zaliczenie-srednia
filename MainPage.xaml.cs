using System.Globalization;

namespace sredniaocen
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnObliczClicked(object sender, EventArgs e)
        {
            try
            {
                // Tablica kontrolek do pobrania tekstu
                string[] wejscia = {
                    Ocena1.Text,
                    Ocena2.Text,
                    Ocena3.Text,
                    Ocena4.Text,
                    Ocena5.Text
                };

                double suma = 0;
                int iloscOcen = 0;

                foreach (var tekst in wejscia)
                {
                    if (string.IsNullOrWhiteSpace(tekst)) continue;

                    // Obsługa kropki i przecinka
                    string formatowanyTekst = tekst.Replace(',', '.');

                    if (double.TryParse(formatowanyTekst, NumberStyles.Any, CultureInfo.InvariantCulture, out double ocena))
                    {
                        suma += ocena;
                        iloscOcen++;
                    }
                    else
                    {
                        throw new Exception("Błędny format liczby");
                    }
                }

                if (iloscOcen == 0)
                {
                    await DisplayAlert("Uwaga", "Wpisz co najmniej jedną ocenę!", "OK");
                    return;
                }

                double srednia = suma / iloscOcen;
                WynikLabel.Text = $"Średnia: {srednia:F2}";

                // Logika statusu i kolorów
                if (srednia >= 4.75)
                {
                    StatusLabel.Text = "🎉 Pasek / Wyróżnienie";
                    StatusLabel.TextColor = Color.FromArgb("#FFD700");
                }
                else if (srednia >= 2.0)
                {
                    StatusLabel.Text = "✅ Zdane";
                    StatusLabel.TextColor = Colors.LightGreen;
                }
                else
                {
                    StatusLabel.Text = "❌ Nie zdane";
                    StatusLabel.TextColor = Colors.Red;
                }
            }
            catch
            {
                await DisplayAlert("Błąd", "Wprowadź poprawne wartości (liczby)!", "OK");
            }
        }
    }
}