using Ganss.XSS;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace CouplesJournal.Helper
{
    public static class HelperExtensions
    {
        // Taken from: https://stackoverflow.com/a/24087164/170217
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => (Index: i, Value: x))
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }

    public static class MarkupStringExtensions
    {
        public static MarkupString Sanitize(this MarkupString markupString)
        {
            return new MarkupString(SanitizeInput(markupString.Value));
        }

        private static string SanitizeInput(string value)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(value);
        }
    }
}
