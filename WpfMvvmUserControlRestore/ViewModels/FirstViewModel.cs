//1.0.8991.*//
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfMvvmUserControlRestore.ViewModels
{
   internal partial class FirstViewModel : ObservableObject
   {
      [ObservableProperty]
      /// <summary> Свойство для примера. </summary>
      private string _firstProperty = "";
   }
}
