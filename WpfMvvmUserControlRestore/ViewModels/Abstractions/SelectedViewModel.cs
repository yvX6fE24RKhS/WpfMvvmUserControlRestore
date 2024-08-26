//1.0.9004.*//
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfMvvmUserControlRestore.ViewModels.Abstractions
{
   internal abstract partial class SelectedViewModel : ObservableObject, ISelectedViewModel
   {
      #region Implementation of ISelectedViewModel

      /// <inheritdoc cref="ISelectedViewModel.OverallProperty"/>
      [ObservableProperty]
      private string _overallProperty = "";

      #endregion Implementation of ISelectedViewModel

      #region Constructors

      public SelectedViewModel(string overallProperty) => OverallProperty = overallProperty;

      #endregion Constructors
   }
}
