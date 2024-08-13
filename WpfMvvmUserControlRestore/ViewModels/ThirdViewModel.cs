//1.0.8991.*//
using System.Reflection;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfMvvmUserControlRestore.ViewModels
{
   internal partial class ThirdViewModel : ObservableObject
   {
      [ObservableProperty]
      /// <summary> Свойство для примера. </summary>
      private Brush _thirdProperty = Brushes.Coral;

      [ObservableProperty]
      private PropertyInfo? _selectedPropColor;

      public static IEnumerable<PropertyInfo> s_AllColors
          = typeof(Brushes).GetProperties()
              .Where(prop => prop.PropertyType == typeof(SolidColorBrush));

      public IEnumerable<PropertyInfo> AllColors => s_AllColors;

      partial void OnSelectedPropColorChanged(PropertyInfo? value)
         => ThirdProperty = (Brush?)value?.GetValue(null) ?? Brushes.CadetBlue;

   }
}
