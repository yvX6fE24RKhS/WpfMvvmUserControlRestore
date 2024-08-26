//1.0.9004.*:1.0.8994.*//
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Auxiliary.Converters;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;
using WpfMvvmUserControlRestore.ViewModels.Abstractions;

namespace WpfMvvmUserControlRestore.ViewModels
{
   [JsonConverter(typeof(ThirdViewModelJsonConverter))]
   internal partial class ThirdViewModel : SelectedViewModel
   {
      #region Fields

      [ObservableProperty]
      private SelectorEnum _selectedEnumItem = SelectorEnum.None;

      /// <summary>Словарь для экземпляров VM страниц</summary>
      readonly Dictionary<SelectorEnum, ISelectedViewModel> _childViewModels = [];

      /// <summary>VM для текущей страницы</summary>
      [ObservableProperty]
      private ISelectedViewModel? _childViewModel;

      #endregion Fields

      #region Constructors

      /// <summary> Инициализирует экземпляр класса <see cref="ThirdViewModel"/>. </summary>
      public ThirdViewModel() : base("")
      {
         _childViewModels.TryAdd(SelectorEnum.First, new FirstViewModel());
         _childViewModels.TryAdd(SelectorEnum.Second, new SecondViewModel());
      }

      /// <summary> Инициализирует экземпляр класса <see cref="ThirdViewModel"/>. </summary>
      [JsonConstructor]
      public ThirdViewModel(ISelectedViewModel? childViewModel, SelectorEnum selectedEnumItem, string overallProperty) : base(overallProperty)
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
         => ChildViewModel = _childViewModels.TryGetValue(value, out ISelectedViewModel? childViewModel)
                             ? childViewModel
                             : null;

      #endregion Event Handlers
   }
}
