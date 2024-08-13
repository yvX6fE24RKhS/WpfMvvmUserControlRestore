//1.0.8991.*//
using System.Text.Json;
using System.Text.Json.Serialization;
using WpfMvvmUserControlRestore.Services.Settings;
using WpfMvvmUserControlRestore.ViewModels;

namespace WpfMvvmUserControlRestore.Auxiliary.Converters
{
   /// <summary>
   /// Считывает и преобразует JSON в тип TypeValuePair
   /// </summary>
   internal class TypeValuePairJsonConverter : JsonConverter<TypeValuePair>
   {
      #region Fields

      /// <summary> Список имен типов, допустимых для преобразования. </summary>
      private readonly List<string> KnownTypes = ["Int32", "FirstViewModel", "String"];

      #endregion Fields

      #region Overridden methods

      /// <inheritdoc/>
      /// <exception cref="JsonException"> Ошибка преобразования. </exception>
      public override TypeValuePair? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

         reader.Read();
         if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();

         string? propertyName = reader.GetString();
         if (propertyName != "Type") throw new JsonException();

         reader.Read();
         if (reader.TokenType != JsonTokenType.String) throw new JsonException();

         string? type = reader.GetString();
         if (type == null || !KnownTypes.Contains(type)) throw new JsonException();

         TypeValuePair? tvp = null;
         object value = new();
         while (reader.Read())
         {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
               return tvp;
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
               propertyName = reader.GetString();
               if (propertyName != "Value") throw new JsonException();

               reader.Read();
               switch (type)
               {
                  case "Int32":
                     value = reader.GetInt32();
                     break;
                  case "FirstViewModel":
                     value = JsonSerializer.Deserialize<FirstViewModel>(ref reader)
                       ?? throw new NullReferenceException("Ошибка десериализации. FirstViewModel не может быть null.");
                     break;
                  case "String":
                     value = reader.GetString() ?? "";
                     break;
               }
               tvp = new(value);
            }
         }

         throw new JsonException();
      }

      /// <inheritdoc/>
      public override void Write(Utf8JsonWriter writer, TypeValuePair value, JsonSerializerOptions options)
      {
         writer.WriteStartObject();

         writer.WriteString("Type", value.Type);
         switch (value.Type)
         {
            case "Int32":
               writer.WriteNumber("Value", (int)value.Value);
               break;
            case "FirstViewModel":
               writer.WritePropertyName("Value");
               JsonSerializer.Serialize(writer, value.Value, options);
               break;
            case "String":
               writer.WriteString("Value", (string)value.Value);
               break;
            default:
               writer.WriteNull("Value");
               break;
         }

         writer.WriteEndObject();
      }

      #endregion Overridden methods
   }
}
