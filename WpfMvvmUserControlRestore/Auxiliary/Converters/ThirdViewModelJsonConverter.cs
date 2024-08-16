//1.0.8994.*//
using System.Text.Json;
using System.Text.Json.Serialization;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;
using WpfMvvmUserControlRestore.ViewModels;

namespace WpfMvvmUserControlRestore.Auxiliary.Converters
{
   /// <summary>
   /// Считывает и преобразует JSON в тип <see cref="ThirdViewModel"/>.
   /// </summary>
   internal class ThirdViewModelJsonConverter : JsonConverter<ThirdViewModel>
   {
      #region Debug
#if DEBUG
#pragma warning disable 0649
      private static readonly int? debugBranch;
#pragma warning restore 0649
#endif
      #endregion Debug

      #region Overridden methods

      public override ThirdViewModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      {
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
#endif
         #endregion Debug

         if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException("Expected StartObject token.");

         SelectorEnum selectedEnumItem = SelectorEnum.None;
         object? childViewModel = null;
         while (reader.Read())
         {
            if (reader.TokenType == JsonTokenType.EndObject)
               return new ThirdViewModel(childViewModel, selectedEnumItem);

            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException("Expected PropertyName token.");

            string? propertyName = reader.GetString();
            reader.Read();
            switch (propertyName)
            {
               case nameof(ThirdViewModel.SelectedEnumItem):
                  selectedEnumItem = JsonSerializer.Deserialize<SelectorEnum>(ref reader);
                  #region Remarks (same result)
                  //selectedEnumItem = (SelectorEnum)reader.GetInt32();
                  #endregion Remarks
                  break;
               case nameof(ThirdViewModel.ChildViewModel):
                  switch (selectedEnumItem)
                  {
                     case SelectorEnum.None:
                        break;
                     case SelectorEnum.First:
                        childViewModel = JsonSerializer.Deserialize<FirstViewModel>(ref reader) ?? new FirstViewModel();
                        break;
                     case SelectorEnum.Second:
                        childViewModel = JsonSerializer.Deserialize<SecondViewModel>(ref reader) ?? new SecondViewModel();
                        break;
                     case SelectorEnum.Third:
                        break;
                     default:
                        break;
                  }
                  break;
               default:
                  break;
            }
         }
         throw new JsonException("Expected EndObject token.");
      }

      public override void Write(Utf8JsonWriter writer, ThirdViewModel value, JsonSerializerOptions options)
      {
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
#endif
         #endregion Debug

         writer.WriteStartObject();

         writer.WritePropertyName(nameof(ThirdViewModel.SelectedEnumItem));
         JsonSerializer.Serialize(writer, value.SelectedEnumItem, options);
         #region Remarks (same result)
         // Запись значения перечисления в виде целого.
         //writer.WriteNumber(nameof(ThirdViewModel.SelectedEnumItem), (int)value.SelectedEnumItem);
         #endregion Remarks

         writer.WritePropertyName(nameof(ThirdViewModel.ChildViewModel));
         JsonSerializer.Serialize(writer, value.ChildViewModel, options);

         writer.WriteEndObject();
      }

      #endregion Overridden methods
   }
}
