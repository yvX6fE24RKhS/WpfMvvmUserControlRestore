//1.0.8991.*//
using System.ComponentModel;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;
using WpfMvvmUserControlRestore.Services.Settings.Abstractions;

namespace WpfMvvmUserControlRestore.ViewModels.Abstractions
{
   /// <summary>
   /// Абстрактная модель представления.
   /// </summary>
   internal abstract partial class ViewModel : ObservableValidator, IPersistedProperties
   {
      #region Fields

      #region Debug
#if DEBUG
#pragma warning disable 0649
      private static readonly int? debugBranch = 1;
#pragma warning restore 0649
#endif
      #endregion Debug

      private const string IndexerName = System.Windows.Data.Binding.IndexerName; /* "Item[]" */

      private readonly ISettingsService? _settingsService;

      /// <summary> Наблюдаемые свойства модели представления подлежащие сохранению. </summary>
      private readonly Dictionary<string, object> _values = [];

      #endregion Fields

      #region Constructors

      /// <summary> Инициализирует экземпляр класса <see cref="MainViewModel"/>. </summary>
      public ViewModel(ISettingsService settingsService)
      {
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
#endif
         #endregion Debug
         _settingsService = settingsService;
         SettingsRestore();
      }

      #endregion Constructors

      #region Implementation of IPersistedProperties

      public object? this[string key]
      {
         get { return _values.TryGetValue(key, out object? value) ? value : null; }
         set
         {
            OnPropertyChanging(IndexerName);
            if (!_values.TryAdd(key, value!)) _values[key] = value!;
            OnPropertyChanged(IndexerName);
         }
      }

      public object this[string key, object defaultValue]
      {
         get
         {
            if (_values.TryGetValue(key, out object? value)) return value!;
            _values.Add(key, defaultValue);
            return defaultValue;
         }
         set
         {
            this[key] = value;
         }
      }

      #endregion Implementation of IPersistedProperties

      #region Properties

      /// <summary> Явные свойства модели представления. </summary>
      private string[] ExplicitProperties
      {
         get => GetType()
               .GetProperties(BindingFlags.Instance
                  | BindingFlags.Static
                  | BindingFlags.Public
                  | BindingFlags.NonPublic)
               .Select(p => p.Name)
               .ToArray();
      }

      /// <summary> Неявные свойства модели представления подлежащие сохранению. </summary>
      private Dictionary<string, object> ImplicitProperties
      {
         get => _values
            .Where(pair => !ExplicitProperties.Contains(pair.Key))
            .ToDictionary(
               pair => pair.Key,
               pair => pair.Value is ValueType
                  ? pair.Value.ToString() ?? throw new NullReferenceException()
                  : pair.Value);
         set => value?
            .ToList()
            .ForEach(pair => this[pair.Key] = pair.Value);
      }

      #endregion Properties

      #region Event Handlers

      /// <summary> Обработка события завершения работы приложения. </summary>
      /// <param name="sender"> Объект, источник события. </param>
      /// <param name="e"> Объект класса <see cref="CancelEventArgs"/>, содержащий данные отменяемого события. </param>
      internal void OnClosing(object? sender, CancelEventArgs e)
      {
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
#endif
         #endregion Debug
         SettingsSave();
         // на случай если нужно отменить закрытие окна
         //e.Cancel = true;
      }

      #endregion Event Handlers


      #region Methods

      /// <summary> Метод восстановления свойств модели представления. </summary>
      private void SettingsRestore()
      {
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
#endif
         #endregion Debug
         try
         {
            ImplicitProperties = _settingsService?.GetAllValues() ?? [];
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

      /// <summary> Метод сохранения свойств модели представления. </summary>
      private void SettingsSave()
      {
         #region Debug
#if DEBUG
         if (debugBranch > 0) AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
#endif
         #endregion Debug

         ImplicitProperties.ToList()
                           .ForEach(pair
                              => _settingsService?.SetValue(pair.Key, pair.Value));
      }

      #endregion Methods
   }
}
