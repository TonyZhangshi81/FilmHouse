using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace FilmHouse.App.Presentation.Web.UI.Configuration;

public static class Encoder
{
    public static HtmlEncoder FilmHouseHtmlEncoder => HtmlEncoder.Create(
        UnicodeRanges.BasicLatin,
        UnicodeRanges.CjkCompatibility,
        UnicodeRanges.CjkCompatibilityForms,
        UnicodeRanges.CjkCompatibilityIdeographs,
        UnicodeRanges.CjkRadicalsSupplement,
        UnicodeRanges.CjkStrokes,
        UnicodeRanges.CjkUnifiedIdeographs,
        UnicodeRanges.CjkUnifiedIdeographsExtensionA,
        UnicodeRanges.CjkSymbolsandPunctuation,
        UnicodeRanges.EnclosedCjkLettersandMonths,
        UnicodeRanges.MiscellaneousSymbols,
        UnicodeRanges.HalfwidthandFullwidthForms
    );
}