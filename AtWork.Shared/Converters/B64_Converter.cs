using System.Text.RegularExpressions;

namespace AtWork.Shared.Converters
{
    public static class B64_Converter
    {
        public static byte[]? GetBytesFromBase64String(string? b64)
        {
            try
            {
                if (b64 is null || b64.Length == 0)
                    return null;

                byte[] fileBytes = Convert.FromBase64String(b64);

                if (fileBytes.Length == 0)
                {
                    return null;
                }

                return fileBytes;
            }
            catch
            {
                return null;
            }
        }

        public static string? GetMimeTypeFromBase64(string? base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
                return null;

            var match = Regex.Match(base64String, @"^data:(?<type>[\w/+.-]+);base64,", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups["type"].Value : null;
        }
    }
}
