//1.0.8993.*:1.0.8991.*//
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;

namespace WpfMvvmUserControlRestore.ViewModels
{
   internal partial class ThirdViewModel : ObservableObject
   {
      #region Fields

      [ObservableProperty]
      private SelectorEnum _selectedEnumItem = SelectorEnum.None;

      /// <summary>Словарь для экземпляров VM страниц</summary>
      readonly Dictionary<SelectorEnum, object> _childViewModels = [];

      /// <summary>VM для текущей страницы</summary>
      [ObservableProperty]
      private object? _childViewModel;

      #endregion Fields

      #region Constructors

      /// <summary> Инициализирует экземпляр класса <see cref="MainViewModel"/>. </summary>
      public ThirdViewModel()
      {
         _childViewModels.Add(SelectorEnum.First, new FirstViewModel());
         _childViewModels.Add(SelectorEnum.Second, new SecondViewModel());
      }

      #endregion Constructors

      #region Event Handlers

      partial void OnSelectedEnumItemChanged(SelectorEnum value)
         => ChildViewModel = _childViewModels.TryGetValue(value, out object? childViewModel)
                             ? childViewModel
                             : null;

      #endregion Event Handlers
   }
}
