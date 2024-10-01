//1.0.9040.*//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Models.Abstractions;

namespace WpfMvvmUserControlRestore.Models.Implementations
{
   internal partial class SelectedModel<T> : ObservableObject, ISelectedModel<T> where T : class, IModelSettings
   {
      #region Implementation of ISelectedModel

      /// <summary> Несериализуемое свойство, общее для выбираемых моделей. </summary>
      [ObservableProperty]
      [property: JsonIgnore]
      private string _nonserializablePropertyCommonToSelectedModels = "undefined";

      /// <summary> Сериализуемое свойство, общее для выбираемых моделей. </summary>
      [ObservableProperty]
      private string _serializablePropertyCommonToSelectedModels = "undefined";

      /// <summary> Настройки модели. </summary>
      [ObservableProperty]
      private T? _modelSettings;

      #endregion Implementation of ISelectedModel

      #region Constructors

      /// <summary> Инициализирует новый экземпляр класса <see cref="SelectedModel"/>. </summary>
      public SelectedModel() {}
      
      /// <summary> Инициализирует новый экземпляр класса <see cref="SelectedModel"/>. </summary>
      /// <param name="serializablePropertyCommonToSelectedModels"> Сериализуемое свойство, общее для выбираемых моделей. </param>
      /// <param name="modelSettings"> Настройки модели. </param>
      /// <exception cref="ArgumentNullException"></exception>
      public SelectedModel(string serializablePropertyCommonToSelectedModels,
                           T modelSettings)
      {
         NonserializablePropertyCommonToSelectedModels = "default";
         SerializablePropertyCommonToSelectedModels =
            serializablePropertyCommonToSelectedModels
            ?? throw new ArgumentNullException(nameof(serializablePropertyCommonToSelectedModels));
         ModelSettings = modelSettings ?? throw new ArgumentNullException(nameof(modelSettings));
      }

      #endregion Constructors
   }
}
