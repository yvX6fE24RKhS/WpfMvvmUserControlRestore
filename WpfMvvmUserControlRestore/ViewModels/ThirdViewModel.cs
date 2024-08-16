//1.0.8994.*:1.0.8993.*//
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Auxiliary.Converters;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;

namespace WpfMvvmUserControlRestore.ViewModels
{
   [JsonConverter(typeof(ThirdViewModelJsonConverter))]
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

      /// <summary> Инициализирует экземпляр класса <see cref="ThirdViewModel"/>. </summary>
      public ThirdViewModel()
      {
         _childViewModels.TryAdd(SelectorEnum.First, new FirstViewModel());
         _childViewModels.TryAdd(SelectorEnum.Second, new SecondViewModel());
      }

      /// <summary> Инициализирует экземпляр класса <see cref="ThirdViewModel"/>. </summary>
      [JsonConstructor]
      public ThirdViewModel(object? childViewModel, SelectorEnum selectedEnumItem)
      {
         _childViewModels.TryAdd(
            SelectorEnum.First, 
            (selectedEnumItem == SelectorEnum.First) ? childViewModel ?? new FirstViewModel() : new FirstViewModel());
         _childViewModels.TryAdd(
            SelectorEnum.Second,
            (selectedEnumItem == SelectorEnum.Second) ? childViewModel ?? new SecondViewModel() : new SecondViewModel());
         ChildViewModel = childViewModel;
         SelectedEnumItem = selectedEnumItem;
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
