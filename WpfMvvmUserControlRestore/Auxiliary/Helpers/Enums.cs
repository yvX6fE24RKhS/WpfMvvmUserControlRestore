//1.0.8992.*:1.0.8991.*//
using System.ComponentModel;

namespace WpfMvvmUserControlRestore.Auxiliary.Helpers
{
   /// <summary>
   /// Перечисление ключей навигатора.
   /// </summary>
   public enum SelectorEnum
   {
      /// <summary>Не выбран</summary>
      None = 0,
      /// <summary>Первый вариант</summary>
      [Description("Первый вариант")]
      First = 1,
      /// <summary>Второй вариант</summary>
      Second = 2,
      /// <summary>Третий вариант</summary>
      Third = 3
   }
}
