//1.0.9004.*:1.0.8991.*//
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.ViewModels.Abstractions;

namespace WpfMvvmUserControlRestore.ViewModels
{
   internal partial class FirstViewModel : SelectedViewModel
   {
      [ObservableProperty]
      /// <summary> Свойство для примера. </summary>
      private string _firstProperty = "";

      #region Constructors

      internal FirstViewModel() : base("") { }

      internal FirstViewModel(string overallProperty) : base(overallProperty) { }

      [JsonConstructor]
      internal FirstViewModel(string firstProperty, string overallProperty) : this(overallProperty)
      {
         FirstProperty = firstProperty;
      }

      #endregion Constructors
   }
}
