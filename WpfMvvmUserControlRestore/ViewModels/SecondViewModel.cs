//1.0.8991.*//
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfMvvmUserControlRestore.ViewModels
{
   internal partial class SecondViewModel : ObservableObject
   {
      [ObservableProperty]
      /// <summary> Свойство для примера. </summary>
      private string[] _secondProperty = ["Первая строка", "Вторая строка", "Третья строка"];

      [ObservableProperty]
      /// <summary> Свойство для примера. </summary>
      private string _selectedRow = "Первая строка";
   }
}
