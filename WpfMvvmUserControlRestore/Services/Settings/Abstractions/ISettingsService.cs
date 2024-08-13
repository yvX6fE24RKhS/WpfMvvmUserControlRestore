//1.0.8991.*//
using System.Diagnostics.Contracts;

namespace WpfMvvmUserControlRestore.Services.Settings.Abstractions
{
   /// <summary>
   /// Интерфейс службы параметров.
   /// </summary>
   internal interface ISettingsService
   {
      #region Methods

      /// <summary>
      /// Возвращает все объекты из хранилища.
      /// </summary>
      /// <returns> Коллекция именованных объектов. </returns>
      Dictionary<string, object> GetAllValues();

      /// <summary> Считывает значение из текущего экземпляра <see cref="IServiceProvider"/> и возвращает его приведение к правильному типу. </summary>
      /// <typeparam name="T"> Тип извлекаемого объекта. </typeparam>
      /// <param name="key"> Ключ, связанный с запрошенным объектом. </param>
      [Pure]
      T GetValue<T>(string key);

      /// <summary> Сохраняет объекты из хранилища в файл. </summary>
      /// <param name="file"> Строка содержащая имя файла. Если значение не задано, то используется имя при инициализации экземпляра класса. </param>
      /// <returns> Задача, представляющая асинхронную операцию сериализации. </returns>
      Task SaveAsync(string? file);

      /// <summary> Создает пару ключ - значение. </summary>
      /// <typeparam name="T"> Тип объекта, привязанного к ключу. </typeparam>
      /// <param name="key"> Ключ доступа. </param>
      /// <param name="value"> Значение, присваиваемое ключу. </param>
      void SetValue<T>(string key, T value);

      #endregion Methods
   }
}
