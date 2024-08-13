//1.0.8991.*//
using System.Text.Json.Serialization;
using WpfMvvmUserControlRestore.Auxiliary.Converters;

namespace WpfMvvmUserControlRestore.Services.Settings
{
   /// <summary>
   /// Представляет сериализуемый объект.
   /// </summary>
   /// <param name="value"> Экземпляр объекта. </param>
   [JsonConverter(typeof(TypeValuePairJsonConverter))]
   public class TypeValuePair(object value)
   {
      #region Properties

      /// <summary> Имя типа экземпляра объекта, необходимое для десериализации в соответствующий тип. </summary>
      public string Type { get; set; } = value.GetType().Name;

      /// <summary> Экземпляр объекта. </summary>
      public object Value { get; set; } = value;

      #endregion Properties
   }
}
