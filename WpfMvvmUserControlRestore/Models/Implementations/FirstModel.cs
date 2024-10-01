//1.0.9040.*//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Models.Implementations;
using WpfMvvmUserControlRestore.Models.Abstractions;

namespace WpfMvvmUserControlRestore.Models
{
   internal partial class FirstModel : SelectedModel<FirstModelSettings>
   {
      #region Fields

      /// <summary> Несериализуемое свойство конкретной модели. </summary>
      [ObservableProperty]
      [property: JsonIgnore]
      private string _nonserializablePropertyFirstModel = "undefined";

      /// <summary> Сериализуемое свойство конкретной модели. </summary>
      [ObservableProperty]
      private string _serializablePropertyFirstModel = "undefined";

      #endregion Fields

      #region Constructors

      /// <summary> Инициализирует новый экземпляр класса <see cref="FirstModel"/>. </summary>
      public FirstModel() : base()
      {
         ModelSettings = new FirstModelSettings();
      }

      /// <summary> Инициализирует новый экземпляр класса <see cref="FirstModel"/>. </summary>
      /// <param name="serializablePropertyCommonToSelectedModels"> Сериализуемое свойство, общее для выбираемых моделей. </param>
      /// <param name="modelSettings"> Настройки модели. </param>
      /// <param name="serializablePropertyFirstModel"> Сериализуемое свойство конкретной модели. </param>
      /// <exception cref="ArgumentNullException"></exception>
      [JsonConstructor]
      public FirstModel(string serializablePropertyCommonToSelectedModels, 
                        FirstModelSettings modelSettings,
                        string serializablePropertyFirstModel)
         : base(serializablePropertyCommonToSelectedModels, modelSettings)
      {
         NonserializablePropertyFirstModel = "default";
         SerializablePropertyFirstModel =
            serializablePropertyFirstModel
            ?? throw new ArgumentNullException(nameof(serializablePropertyFirstModel));
      }

      #endregion Constructors
   }
}
