//1.0.8992.*:1.0.8991.*//
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfMvvmUserControlRestore.ViewModels
{
   internal partial class ThirdViewModel : ObservableObject
   {
      [ObservableProperty]
      [property: JsonIgnore]
      /// <summary> Свойство для примера. </summary>
      private Brush _thirdProperty = Brushes.Coral;

      [ObservableProperty]
      [property: JsonIgnore]
      private PropertyInfo? _selectedPropColor;

      public static IEnumerable<PropertyInfo> s_AllColors
          = typeof(Brushes).GetProperties()
              .Where(prop => prop.PropertyType == typeof(SolidColorBrush));

      [JsonIgnore]
      public static IEnumerable<PropertyInfo> AllColors => s_AllColors;

      partial void OnSelectedPropColorChanged(PropertyInfo? value)
         => ThirdProperty = (Brush?)value?.GetValue(null) ?? Brushes.CadetBlue;

   }
}
