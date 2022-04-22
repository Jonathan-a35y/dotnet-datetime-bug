using System.Globalization;

var cultureNames = new[] { "en-GB", "en-US", "fr-FR", "es-ES", "de-DE" };

foreach (var cultureName in cultureNames)
{
    var culture = CultureInfo.GetCultureInfo(cultureName);

    Thread.CurrentThread.CurrentCulture = culture;

    var parseFormat = "d.MMM.yyyy";

    var abbMonths = culture.DateTimeFormat.AbbreviatedMonthNames.Where(abb => !string.IsNullOrWhiteSpace(abb)).ToArray();

    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.WriteLine($"Using culture {culture.Name} with format '{parseFormat}'.");
    Console.WriteLine($"DateTimeFormat.AbbreviatedMonthNames: [{string.Join(", ", abbMonths)}]");

    Console.WriteLine();

    foreach (var (abbrMonthName, index) in abbMonths.Select((e, i) => (e, i)))
    {
        var date = new DateTime(2020, index + 1, 1, culture.Calendar);
        var dateString = date.ToString(parseFormat, culture);
        var expectedDateString = $"1.{abbrMonthName}.2020";

        Console.WriteLine($"  Parsing {expectedDateString}");


        if (DateTime.TryParseExact(expectedDateString, parseFormat, culture, DateTimeStyles.AllowWhiteSpaces, out var dt))
        {
            Console.WriteLine($"    OK: {dt.ToShortDateString()}");
        }
        else
        {
            Console.WriteLine($"    FAILED!");
        }

        if (!string.Equals(dateString, expectedDateString))
        {
            Console.WriteLine($"    -> Formatted string differs: expected '{expectedDateString}', got '{dateString}'");
        }
    }

    Console.WriteLine();
}
