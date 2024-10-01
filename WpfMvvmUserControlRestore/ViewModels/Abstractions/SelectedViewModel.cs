//1.0.9040.*:1.0.9004.*//
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Models;
using WpfMvvmUserControlRestore.Models.Abstractions;
using WpfMvvmUserControlRestore.Models.Implementations;

namespace WpfMvvmUserControlRestore.ViewModels.Abstractions
{
   internal abstract partial class SelectedViewModel : ObservableObject, ISelectedViewModel
   {
      #region Implementation of ISelectedViewModel

      ///// <inheritdoc cref="ISelectedViewModel.OverallProperty"/>
      //[ObservableProperty]
      //private string _overallProperty = "";

      /// <inheritdoc cref="ISelectedViewModel.NonserializablePropertyCommonToSelectedViewModels"/>
      [ObservableProperty]
      [property: JsonIgnore]
      private string _nonserializablePropertyCommonToSelectedViewModels = "undefined";

      /// <inheritdoc cref="ISelectedViewModel.SerializablePropertyCommonToSelectedViewModels"/>
      [ObservableProperty]
      private string _serializablePropertyCommonToSelectedViewModels = "undefined";

      /// <inheritdoc cref="ISelectedViewModel.SelectedModel"/>
      [ObservableProperty]
      private ISelectedModel<IModelSettings>? _selectedModel;

      #endregion Implementation of ISelectedViewModel

      #region Constructors

      /// <summary> Инициализирует новый экземпляр класса <see cref="SelectedViewModel"/>. </summary>
      public SelectedViewModel() { }

      /// <summary> Инициализирует новый экземпляр класса <see cref="SelectedViewModel"/>. </summary>
      /// <param name="serializablePropertyCommonToSelectedViewModels"> Сериализуемое свойство, общее для выбираемых моделей представления. </param>
      /// <exception cref="ArgumentNullException"></exception>
      public SelectedViewModel(string serializablePropertyCommonToSelectedViewModels,
                               ISelectedModel<IModelSettings>? selectedModel)
      {
         SerializablePropertyCommonToSelectedViewModels
            = serializablePropertyCommonToSelectedViewModels
              ?? throw new ArgumentNullException(nameof(serializablePropertyCommonToSelectedViewModels));

         SelectedModel = selectedModel ?? throw new ArgumentNullException(nameof(selectedModel)); ;
         NonserializablePropertyCommonToSelectedViewModels = "default";
      }

      #endregion Constructors
   }
}
