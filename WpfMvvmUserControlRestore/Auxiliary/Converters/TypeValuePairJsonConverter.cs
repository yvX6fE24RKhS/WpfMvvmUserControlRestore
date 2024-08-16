//1.0.8994.*:1.0.8992.*//
using System.Text.Json;
using System.Text.Json.Serialization;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;
using WpfMvvmUserControlRestore.Services.Settings;
using WpfMvvmUserControlRestore.ViewModels;

namespace WpfMvvmUserControlRestore.Auxiliary.Converters
{
   /// <summary>
   /// Считывает и преобразует JSON в тип TypeValuePair
   /// </summary>
   internal class TypeValuePairJsonConverter : JsonConverter<TypeValuePair>
   {
      #region Debug
#if DEBUG
#pragma warning disable 0649
      private static readonly int? debugBranch = 1;
#pragma warning restore 0649
#endif
      #endregion Debug

      #region Fields

      /// <summary> Список имен типов, допустимых для преобразования. </summary>
      private readonly List<string> _knownTypes = [
         "FirstViewModel",
         "Int32",
         "SecondViewModel",
         "SelectorEnum",
         "String",
         "ThirdViewModel"
      ];

      #endregion Fields

      #region Overridden methods

      /// <inheritdoc/>
      /// <exception cref="JsonException"> Ошибка преобразования. </exception>
      public override TypeValuePair? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
#endif
         #endregion Debug

         if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

         reader.Read();
         if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();

         string? propertyName = reader.GetString();
         if (propertyName != "Type") throw new JsonException();

         reader.Read();
         if (reader.TokenType != JsonTokenType.String) throw new JsonException();

         string? type = reader.GetString();
         if (type == null || !_knownTypes.Contains(type)) throw new JsonException();

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
                       ?? throw new NullReferenceException($"Ошибка десериализации. {type} не может быть null.");
                     break;
                  case "SecondViewModel":
                     value = JsonSerializer.Deserialize<SecondViewModel>(ref reader)
                       ?? throw new NullReferenceException($"Ошибка десериализации. {type} не может быть null.");
                     break;
                  case "SelectorEnum":
                     value = JsonSerializer.Deserialize<SelectorEnum>(ref reader);
                     break;
                  case "String":
                     value = reader.GetString() ?? "";
                     break;
                  case "ThirdViewModel":
                     value = JsonSerializer.Deserialize<ThirdViewModel>(ref reader)
                       ?? throw new NullReferenceException($"Ошибка десериализации. {type} не может быть null.");
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
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
#endif
         #endregion Debug

         writer.WriteStartObject();

         writer.WriteString("Type", value.Type);
         switch (value.Type)
         {
            case "Int32":
               writer.WriteNumber("Value", (int)value.Value);
               break;
            case "FirstViewModel":
            case "SecondViewModel":
            case "SelectorEnum":
            case "ThirdViewModel":
               #region Debug
#if DEBUG
               if (debugBranch == 1) AppHelper.DebugOut(debugBranch,
                                                        $"{AppHelper.GetCallerMemberName(this)}",
                                                        $"value.Type is {value.Type}.");
#endif
               #endregion Debug
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
