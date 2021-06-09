using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Plugins.GameBoost.Core
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(Dictionary<string, object> json, bool doSort)
        {
            var builder = new JsonStringBuilder();
            builder.WriteDictionary(json, doSort);
            return builder.ToString();
        }


        public class JsonStringBuilder
        {
            private readonly StringBuilder stringBuilder = new StringBuilder();

            public void WriteObject(object obj, bool doSort)
            {
                if (obj == null)
                {
                    stringBuilder.Append("null");
                }
                else if (obj is IDictionary)
                {
                    var dictObject = (IDictionary) obj;
                    WriteDictionary(dictObject, doSort);
                }
                else if (obj is IList)
                {
                    var listObject = (IList) obj;
                    WriteList(listObject, doSort);
                }
                else if (obj is string)
                {
                    var stringObject = (string) obj;
                    WriteString(stringObject);
                }
                else
                {
                    WriteAny(obj);
                }
            }

            public void WriteList(IList list, bool doSort)
            {
                stringBuilder.Append('[');

                bool isFirst = true;
                foreach (var element in list)
                {
                    if (!isFirst)
                    {
                        stringBuilder.Append(',');
                    }

                    WriteObject(element, doSort);
                    isFirst = false;
                }

                stringBuilder.Append(']');
            }

            public void WriteDictionary(IDictionary dictionary, bool doSort)
            {
                stringBuilder.Append('{');

                var dict = doSort
                    ? new SortedList(dictionary)
                    : dictionary;

                bool isFirst = true;
                foreach (DictionaryEntry entry in dict)
                {
                    if (!isFirst)
                    {
                        stringBuilder.Append(',');
                    }

                    WriteString(entry.Key.ToString());
                    stringBuilder.Append(':');
                    WriteObject(entry.Value, doSort);
                    isFirst = false;
                }

                stringBuilder.Append('}');
            }

            public void WriteString(string stringToWrite)
            {
                JsonStringWriter.WriteString(stringBuilder, stringToWrite);
            }

            public void WriteAny(object obj)
            {
                if (obj is int
                    || obj is uint
                    || obj is sbyte
                    || obj is byte
                    || obj is short
                    || obj is ushort
                    || obj is long
                    || obj is ulong)
                {
                    stringBuilder.Append(obj);
                }
                // We want to round-trip floating point numbers
                // See https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#RFormatString
                else if (obj is float)
                {
                    var flt = (float) obj;
                    stringBuilder.Append(flt.ToString("R", CultureInfo.InvariantCulture));
                }
                else if (obj is double)
                {
                    var dbl = (double) obj;
                    stringBuilder.Append(dbl.ToString("R", CultureInfo.InvariantCulture));
                }
                else if (obj is decimal)
                {
                    var dml = (decimal) obj;
                    stringBuilder.Append(dml.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    WriteString(obj.ToString());
                }
            }

            public override string ToString()
            {
                return stringBuilder.ToString();
            }
        }

        public static class JsonStringWriter
        {
            public static void WriteString(StringBuilder builder, string stringToWrite)
            {
                builder.Append('"');

                foreach (var ch in stringToWrite)
                {
                    // Symbols that should be escaped (as per https://www.json.org)
                    switch (ch)
                    {
                        case '"':
                            builder.Append("\\\"");
                            break;
                        case '\\':
                            builder.Append("\\\\");
                            break;
                        case '/':
                            builder.Append("\\/");
                            break;
                        case '\b':
                            builder.Append("\\b");
                            break;
                        case '\f':
                            builder.Append("\\f");
                            break;
                        case '\n':
                            builder.Append("\\n");
                            break;
                        case '\r':
                            builder.Append("\\r");
                            break;
                        case '\t':
                            builder.Append("\\t");
                            break;
                        default:
                            builder.Append(ch);
                            break;
                    }
                }

                builder.Append('"');
            }
        }
    }
}
