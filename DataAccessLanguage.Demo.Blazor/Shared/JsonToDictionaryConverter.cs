using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataAccessLanguage.Demo.Blazor
{
    public class JsonToDictionaryConverter : JsonConverterFactory
	{
		private static JsonConverter<Dictionary<string, object>> _valueConverter = null;

		public override bool CanConvert(Type typeToConvert)
		{
			return typeToConvert == typeof(Dictionary<string, object>);
		}

		public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
		{
			return _valueConverter ?? (_valueConverter = new DictionaryConverterInner(options));
		}

		private class DictionaryConverterInner : JsonConverter<Dictionary<string, object>>
		{
			private JsonSerializerOptions _options;
			private JsonConverter<Dictionary<string, object>> _valueConverter = null;

			JsonConverter<Dictionary<string, object>> converter
			{
				get
				{
					return _valueConverter ?? (_valueConverter = (JsonConverter<Dictionary<string, object>>)_options.GetConverter(typeof(Dictionary<string, object>)));
				}
			}

			public DictionaryConverterInner(JsonSerializerOptions options)
			{
				_options = options;
			}

			public override Dictionary<string, object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				if (reader.TokenType != JsonTokenType.StartObject)
				{
					throw new JsonException();
				}

				Dictionary<string, object> dictionary = new Dictionary<string, object>();

				while (reader.Read())
				{
					if (reader.TokenType == JsonTokenType.EndObject)
					{
						return dictionary;
					}

					// Get the key.
					if (reader.TokenType != JsonTokenType.PropertyName)
					{
						throw new JsonException();
					}

					string propertyName = reader.GetString();
					reader.Read();

					dictionary[propertyName] = getValue(ref reader, options);
				}
				return dictionary;
			}

			private object getValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
			{
				switch (reader.TokenType)
				{
					case JsonTokenType.String:
						string s = reader.GetString();
						if (s.Contains(':') && TimeSpan.TryParse(s, out TimeSpan time))
							return time;
						else if (s.Contains(':') && DateTime.TryParse(s, out DateTime res))
							return res;
						return reader.GetString();
					case JsonTokenType.False:
						return false;
					case JsonTokenType.True:
						return true;
					case JsonTokenType.Null:
						return null;
					case JsonTokenType.Number:
						if (reader.TryGetInt64(out long _long))
							return _long;
						else if (reader.TryGetDecimal(out decimal _dec))
							return _dec;
						throw new JsonException($"Unhandled Number value");
					case JsonTokenType.StartObject:
						return JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
					case JsonTokenType.StartArray:
						List<object> array = new List<object>();
						while (reader.Read() &&
							reader.TokenType != JsonTokenType.EndArray)
						{
							array.Add(getValue(ref reader, options));
						}
						return array;
				}
				throw new JsonException($"Unhandled TokenType {reader.TokenType}");
			}

			public override void Write(Utf8JsonWriter writer, Dictionary<string, object> hashtable, JsonSerializerOptions options)
			{
				writer.WriteStartObject();

				foreach (KeyValuePair<string, object> kvp in hashtable)
				{
					writer.WritePropertyName(kvp.Key);

					if (converter != null &&
						kvp.Value is Dictionary<string, object>)
					{
						converter.Write(writer, (Dictionary<string, object>)kvp.Value, options);
					}
					else if (kvp.Value is TimeSpan || kvp.Value is TimeSpan?)
					{
						Console.WriteLine(kvp.Value);
						writer.WriteStringValue(kvp.Value?.ToString());
					}
					else
					{
						JsonSerializer.Serialize(writer, kvp.Value, options);
					}
				}

				writer.WriteEndObject();
			}
		}
	}
}
