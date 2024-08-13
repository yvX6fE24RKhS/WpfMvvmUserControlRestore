//1.0.8991.*//
namespace WpfMvvmUserControlRestore.ViewModels.Abstractions
{
   /// <summary>
   /// Свойств подлежащие сохранению.
   /// </summary>
   /// <remarks> Модель представления как словарь: <see href="https://habrahabr.ru/post/208326/"/></remarks>
   internal interface IPersistedProperties
   {
      //private const string IndexerName = System.Windows.Data.Binding.IndexerName; /* "Item[]" */

      #region Indexers

      /// <summary> Коллекция свойств подлежащих сохранению. </summary>
      /// <param name="key"> Строка, содержащая имя свойства. </param>
      /// <returns> Объект, содержащий значение свойства. </returns>
      object? this[string key] { get; set; }

      /// <summary> Коллекция свойств подлежащих сохранению. </summary>
      /// <param name="key"> Строка, содержащая имя свойства. </param>
      /// <param name="defaultValue"> Объект, содержащий значение свойства по умолчанию.</param>
      /// <returns> Объект, содержащий значение свойства. </returns>
      object this[string key, object defaultValue] { get; set; }

      #endregion Indexers
   }
}
