//1.0.8991.*//
namespace WpfMvvmUserControlRestore.Services.Settings
{
   /// <summary>
   /// Хранилище настроек.
   /// </summary>
   public class SettingsStorage
   {
      #region Properties

      ///// <summary> Наблюдаемые свойства модели представления подлежащие сохранению. </summary>
      public Dictionary<string, TypeValuePair> Values { get; set; } = [];

      /// <summary> Указывает, является ли хранилище пустым. </summary>
      internal bool IsEmpty => (Values?.Count ?? 0) == 0;

      #endregion Properties
   }
}
