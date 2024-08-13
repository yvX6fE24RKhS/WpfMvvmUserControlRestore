//1.0.8991.*//
using WpfMvvmUserControlRestore.Auxiliary.Helpers;
using WpfMvvmUserControlRestore.Services.Settings.Abstractions;

namespace WpfMvvmUserControlRestore.Services.Settings.Implementations
{
   /// <summary>
   /// Представляет реализацию интерфейса службы параметров.
   /// </summary>
   internal sealed class SettingsService : ISettingsService
   {
      #region Fields

      #region Debug
#if DEBUG
#pragma warning disable 0649
      private static readonly int? debugBranch;
#pragma warning restore 0649
#endif
      #endregion Debug

      /// <summary> Имя файла по умолчанию. </summary>
      private const string DefaltFileName = "settings.json";

      /// <summary> Хранилище настроек. </summary>
      private static SettingsStorage SettingsStorage = new();

      #endregion Fields

      #region Constructors

      /// <summary> Инициализирует экземпляр класса <see cref="SettingsService"/>. </summary>
      /// <param name="file"> Строка содержащая имя файла. Если значение не задано, то "settings.json" из папки приложения. </param>
      public SettingsService(string? file = null)
      {
         if (SettingsStorage.IsEmpty)
         {
            SettingsStorage = Restore(file);
         }
      }

      #endregion Constructors

      #region Implementation of ISettingsService

      /// <inheritdoc/>
      public Dictionary<string, object> GetAllValues() => SettingsStorage.Values.ToDictionary(pair => pair.Key, pair => pair.Value.Value);

      /// <inheritdoc/>
      public T GetValue<T>(string key)
         => SettingsStorage.Values.TryGetValue(key, out TypeValuePair? tvp)
            ? (T)tvp.Value
            : default!;


      /// <inheritdoc/>
      /// <exception cref="Exception"> В случае ошибки сохранения. .</exception>>
      public async Task SaveAsync(string? file = null)
      {
         if (SettingsStorage != null)
         {
            file ??= $"{AppHelper.GetAppDir()}\\{DefaltFileName}";
            try
            {
               await Serializer.SerializeSettingsAsync(SettingsStorage, file);
            }
            catch (Exception ex)
            {
               #region Debug
#if DEBUG
               AppHelper.DebugOut(debugBranch ?? 0,
                                  $"{AppHelper.GetCallerMemberName(this)}",
                                  $"Error: {ex.Message}.");
#endif
               #endregion Debug
            }
         }
      }

      /// <inheritdoc/>
      public void SetValue<T>(string key, T value)
      {
         if (value == null)
         {
            if (SettingsStorage.Values.ContainsKey(key)) _ = SettingsStorage.Values.Remove(key);
         }
         else if (!SettingsStorage.Values.ContainsKey(key))
         {
            SettingsStorage.Values.Add(key, new TypeValuePair(value!));
         }
         else
         {
            SettingsStorage.Values[key] = new TypeValuePair(value);
         }
      }

      #endregion Implementation of ISettingsService

      #region Methods

      /// <summary> Заполняет хранилище настроек объектами полученными из файла. </summary>
      /// <param name="file"> Строка содержащая имя файла. Если значение не задано, то "settings.json" из папки приложения. </param>
      /// <returns> Экземпляр хранилища настроек. </returns>
      /// <exception cref="Exception"> В случае ошибки восстановления. .</exception>
      private SettingsStorage Restore(string? file = null)
      {
         #region Debug
#if DEBUG
         AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
         if (debugBranch == 2) file = $"c:\\temp\\phantom.json";
         //return default;
#endif
         #endregion Debug
         file ??= $"{AppHelper.GetAppDir()}\\{DefaltFileName}";
         SettingsStorage settingsStorage = new();
         try
         {
            settingsStorage = Serializer.DeSerializeSettings(file) ?? new();
         }
         catch (Exception ex)
         {
            #region Debug
#if DEBUG
            AppHelper.DebugOut(debugBranch ?? 0,
                               $"{AppHelper.GetCallerMemberName(this)}",
                               $"Error: {ex.Message}.");
#endif
            #endregion Debug
         }

         return settingsStorage;
      }

      #endregion Methods
   }
}
