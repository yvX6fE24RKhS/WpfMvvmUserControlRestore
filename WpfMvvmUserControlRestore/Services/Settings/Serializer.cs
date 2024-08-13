//1.0.8991.*//
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;

namespace WpfMvvmUserControlRestore.Services.Settings
{
   /// <summary>
   /// Предоставляет функциональность для сериализации настроек приложения в файл и десериализации их из файла.
   /// </summary>
   internal static class Serializer
   {
      #region Fields

      #region Debug
#if DEBUG
#pragma warning disable 0649
      private static readonly int? debugBranch = 1;
#pragma warning restore 0649
#endif
      #endregion Debug

      static readonly string className = typeof(Serializer).FullName!;

      /// <summary> Настройки преобразователя сериализатора. </summary>
      static readonly JsonSerializerOptions jsonSerializerOptions = new()
      {
         DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
         IgnoreReadOnlyProperties = true,
         UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode,
         WriteIndented = true
      };

      #endregion Fields

      #region Methods

      /// <summary> Преобразует предоставленное значение хранилища настроек в текст JSON в кодировке UTF-8 и записывает его в файл. </summary>
      /// <param name="settingsStorage"> Хранилища настроек. </param>
      /// <param name="file"> Строка содержащая имя файла. </param>
      /// <returns> Задача, представляющая асинхронную операцию записи. </returns>
      /// <exception cref="Exception"> Объект не может быть сериализован или записан в файл. </exception>
      internal static async Task SerializeSettingsAsync(SettingsStorage settingsStorage, string file)
      {
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{nameof(Serializer)}.{AppHelper.GetCallerName()}");
#endif
         #endregion Debug
         try
         {
            await using FileStream fileStream = File.Create(file);
            await JsonSerializer.SerializeAsync(fileStream, settingsStorage, jsonSerializerOptions);
         }
         catch (Exception ex)
         {
            #region Debug
#if DEBUG
            AppHelper.DebugOut(debugBranch ?? 0,
                               $"{nameof(Serializer)}.{AppHelper.GetCallerName()}",
                               $"Error: {ex.Message}.");
#endif
            #endregion Debug
         }
      }

      /// <summary> Преобразует текст JSON в кодировке UTF-8 из файла в экземпляр хранилища настроек. /// </summary>
      /// <param name="file"> Строка содержащая имя файла. </param>
      /// <returns> Хранилище настроек. </returns>
      /// <exception cref="Exception"> Текст не может быть получен из файла или преобразован экземпляр хранилища настроек. </exception>
      /// <exception cref="FileNotFoundException"> Файл настроек не найден. </exception>
      /// <exception cref="JsonException"> Ошибка преобразования текста json из файла. </exception>
      internal static SettingsStorage? DeSerializeSettings(string file)
      {
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{nameof(Serializer)}.{AppHelper.GetCallerName()}");
#endif
         #endregion Debug
         try
         {
            using FileStream fileStream = File.OpenRead(file);
            return JsonSerializer.Deserialize<SettingsStorage>(fileStream, jsonSerializerOptions);
         }
         catch (FileNotFoundException)
         {
            throw new FileNotFoundException($"{className}.{AppHelper.GetCallerName()}: Файл настроек {file} не найден.");
         }
         catch (JsonException ex)
         {
            throw new JsonException($"{className}.{AppHelper.GetCallerName()}: Ошибка преобразования текста json из файла {file}.{Environment.NewLine}{ex.Message}");
         }
         catch (Exception ex)
         {
            #region Debug
#if DEBUG
            AppHelper.DebugOut(debugBranch ?? 0,
                               $"{nameof(Serializer)}.{AppHelper.GetCallerName()}",
                               $"Error: {ex.Message}.");
            return default;
#endif
            #endregion Debug
         }
      }

      #endregion Methods
   }
}
