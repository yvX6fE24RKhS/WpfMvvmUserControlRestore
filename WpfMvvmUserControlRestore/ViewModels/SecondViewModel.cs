//1.0.9004.*:1.0.8991.*//
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.ViewModels.Abstractions;

namespace WpfMvvmUserControlRestore.ViewModels
{
   internal partial class SecondViewModel() : SelectedViewModel()
   {
      [ObservableProperty]
      /// <summary> Свойство для примера. </summary>
      private string[] _secondProperty = ["Первая строка", "Вторая строка", "Третья строка"];

      [ObservableProperty]
      /// <summary> Свойство для примера. </summary>
      private string _selectedRow = "Первая строка";
   }
}
