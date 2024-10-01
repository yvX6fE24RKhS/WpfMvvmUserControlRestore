//1.0.9040.*//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Models.Abstractions;

namespace WpfMvvmUserControlRestore.Models.Implementations
{
   internal partial class FirstModelSettings : ModelSettings
   {
      #region Fields

      /// <summary> Несериализуемое свойство, для настроек конкретной модели. </summary>
      [ObservableProperty]
      [property: JsonIgnore]
      private string _nonserializablePropertyFirstModeSettings = "undefined";

      /// <summary> Сериализуемое свойство, для настроек конкретной модели. </summary>
      [ObservableProperty]
      private string _serializablePropertyFirstModeSettings = "undefined";

      #endregion Fields

      #region Constructors

      /// <summary> Инициализирует новый экземпляр класса <see cref="FirstModelSettings"/>. </summary>
      public FirstModelSettings() : base() {}

      /// <summary> Инициализирует новый экземпляр класса <see cref="FirstModelSettings"/>. </summary>
      /// <param name="serializablePropertyCommonToModelSettings"> Сериализуемое свойство, общее для настроек модели. </param>
      /// <param name="serializablePropertyFirstModeSettings"> Сериализуемое свойство, для настроек конкретной модели. </param>
      /// <exception cref="ArgumentNullException"></exception>
      [JsonConstructor]
      public FirstModelSettings(string serializablePropertyCommonToModelSettings,
                                string serializablePropertyFirstModeSettings)
         : base(serializablePropertyCommonToModelSettings)
      {
         NonserializablePropertyFirstModeSettings = "default";
         SerializablePropertyFirstModeSettings = 
            serializablePropertyFirstModeSettings
            ?? throw new ArgumentNullException(nameof(serializablePropertyFirstModeSettings));
      }

      #endregion Constructors
   }
}
