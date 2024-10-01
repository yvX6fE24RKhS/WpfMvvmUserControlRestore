//1.0.9040.*//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfMvvmUserControlRestore.Models.Abstractions
{
   /// <summary>
   /// 
   /// </summary>
   internal abstract partial class ModelSettings : ObservableObject, IModelSettings
   {
      #region Implementation of IModelSettings

      /// <summary> <inheritdoc cref="IModelSettings.NonserializablePropertyCommonToModelSettings"/> </summary>
      [ObservableProperty]
      [property: JsonIgnore]
      private string _nonserializablePropertyCommonToModelSettings = "undefined";

      /// <summary> <inheritdoc cref="IModelSettings.SerializablePropertyCommonToModelSettings"/> </summary>
      [ObservableProperty]
      private string _serializablePropertyCommonToModelSettings = "undefined";

      #endregion Implementation of IModelSettings

      #region Constructors

      /// <summary> Инициализирует новый экземпляр класса <see cref="ModelSettings"/>. </summary>
      public ModelSettings() { }

      /// <summary> Инициализирует новый экземпляр класса <see cref="ModelSettings"/>. </summary>
      /// <param name="serializablePropertyCommonToModelSettings"> Сериализуемое свойство, общее для настроек модели. </param>
      /// <exception cref="ArgumentNullException"></exception>
      public ModelSettings(string serializablePropertyCommonToModelSettings)
      {
         NonserializablePropertyCommonToModelSettings = "default";
         SerializablePropertyCommonToModelSettings = 
            serializablePropertyCommonToModelSettings
            ?? throw new ArgumentNullException(nameof(serializablePropertyCommonToModelSettings));
      }

      #endregion Constructors
   }
}
