//1.0.8991.*//
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace WpfMvvmUserControlRestore.Auxiliary.Converters
{
   /// <summary>
   /// Преобразует значение перечисления в строку содержащую описание или имя перечисления и обратно.
   /// </summary>
   class EnumConverter : Collection<string>, IValueConverter
   {
      #region Fields

      /// <inheritdoc cref="Type"/>
      private Type? _type;

      /// <summary> Словарь описаний элементов перечисления, где ключ значение элемента перечисления </summary>
      private IDictionary<object, object> _valueToNameMap = new Dictionary<object, object>();

      /// <summary> Словарь значений элементов перечисления, где ключ описание элемента перечисления </summary>
      private IDictionary<object, object> _nameToValueMap = new Dictionary<object, object>();

      #endregion Fields

      #region Implementation of IValueConverter

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
         => _valueToNameMap[value];

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
         => _nameToValueMap[value];

      #endregion Implementation of IValueConverter

      #region Properties

      /// <summary> Тип конвертируемого объекта. </summary>
      public Type? Type
      {
         get { return _type; }
         set
         {
            if (!value?.IsEnum ?? true)
               throw new ArgumentException("Type is not an enum.", nameof(value));
            _type = value;
            Initialize();
         }
      }

      #endregion Properties

      #region Methods

      /// <summary> Извлекает описание или имя элемента перечисления. </summary>
      /// <param name="fieldInfo"> Данные о элементе перечисления. </param>
      /// <returns> Объект, содержащий описание или имя элемента перечисления. </returns>
      static object GetDescription(FieldInfo fieldInfo)
         => Attribute.GetCustomAttribute(fieldInfo,
                                         typeof(DescriptionAttribute)
                                        ) is DescriptionAttribute descriptionAttribute
            ? descriptionAttribute.Description
            : fieldInfo.Name;

      /// <summary> Инициализация словарей конвертации из перечисления. </summary>
      private void Initialize()
      {
         if (_type != null)
         {
            _valueToNameMap = _type.GetFields(BindingFlags.Static | BindingFlags.Public)
                                   .ToDictionary(fi => fi.GetValue(null) ?? fi.Name,
                                                 GetDescription);
            _nameToValueMap = _valueToNameMap.ToDictionary(kvp => kvp.Value,
                                                           kvp => kvp.Key);
            Clear();
            foreach (string name in _nameToValueMap.Keys.Cast<string>())
               Add(name);
         }
      }

      #endregion Methods
   }
}
