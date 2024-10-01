//1.0.9040.*:1.0.9004.*//
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Models;
using WpfMvvmUserControlRestore.Models.Abstractions;
using WpfMvvmUserControlRestore.Models.Implementations;
using WpfMvvmUserControlRestore.ViewModels.Abstractions;

namespace WpfMvvmUserControlRestore.ViewModels
{
   internal partial class FirstViewModel : SelectedViewModel
   {
      //[ObservableProperty]
      ///// <summary> Свойство для примера. </summary>
      //private string _firstProperty = "";

      #region Fields

      /// <summary> Сериализуемое свойство конкретной модели представления. </summary>
      [ObservableProperty]
      private string _serializablePropertyFirstViewModel = "undefined";

      /// <summary> Несериализуемое свойство конкретной модели представления. </summary>
      [ObservableProperty]
      [property: JsonIgnore]
      private string _nonserializablePropertyFirstViewModel = "undefined";

      #endregion Fields

      #region Constructors

      internal FirstViewModel() : base() {
         SelectedModel = (ISelectedModel<IModelSettings>)new FirstModel();
      }

      [JsonConstructor]
      internal FirstViewModel(string serializablePropertyCommonToSelectedViewModels,
                              string serializablePropertyFirstViewModel,
                              ISelectedModel<IModelSettings>? selectedModel)
         : base(serializablePropertyCommonToSelectedViewModels, selectedModel)
      {
         NonserializablePropertyFirstViewModel = "default";
         SerializablePropertyFirstViewModel =
            serializablePropertyFirstViewModel
            ?? throw new ArgumentNullException(nameof(serializablePropertyFirstViewModel));
      }

      #endregion Constructors
   }
}
