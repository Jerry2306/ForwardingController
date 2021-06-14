using System;
using System.Text;

namespace SharedItems.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetFormattedMessage(this Exception source)
        {
            if (source == null)
                return string.Empty;

            var result = new StringBuilder();
            result.AppendLine($"Message: {source.Message}");

            var temp = source.InnerException;

            for (var i = 1; temp != null; i++)
            {
                result.AppendLine($"Detail {i}: {temp.Message}");
                temp = temp.InnerException;
            }

            return result.ToString();
        }
    }
}