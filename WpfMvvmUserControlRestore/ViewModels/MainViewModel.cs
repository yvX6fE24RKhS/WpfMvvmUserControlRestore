//1.0.8991.*//
using CommunityToolkit.Mvvm.ComponentModel;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;
using WpfMvvmUserControlRestore.Services.Settings.Abstractions;
using WpfMvvmUserControlRestore.ViewModels.Abstractions;

namespace WpfMvvmUserControlRestore.ViewModels
{
   /// <summary>
   /// Главная модель представления.
   /// </summary>
   internal sealed partial class MainViewModel : ViewModel
   {
      #region Fields

      /// <summary> Версия текущей сборки приложения. </summary>
      [ObservableProperty]
      private string _applicationAssemblyVersion = AppHelper.GetAssemblyVersion();

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
      public MainViewModel(ISettingsService settingsService) : base(settingsService)
      {
         _childViewModels.Add(SelectorEnum.First, new FirstViewModel());
         _childViewModels.Add(SelectorEnum.Second, new SecondViewModel());
         _childViewModels.Add(SelectorEnum.Third, new ThirdViewModel());
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
