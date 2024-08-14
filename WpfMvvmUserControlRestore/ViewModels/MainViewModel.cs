//1.0.8992.*:1.0.8991.*//
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

      #region Debug
#if DEBUG
#pragma warning disable 0649
      private static readonly int? mainViewModelDebugBranch = 1;
#pragma warning restore 0649
#endif
      #endregion Debug

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
         _childViewModels.TryAdd(SelectorEnum.First, new FirstViewModel());
         _childViewModels.TryAdd(SelectorEnum.Second, new SecondViewModel());
         _childViewModels.TryAdd(SelectorEnum.Third, new ThirdViewModel());
      }

      #endregion Constructors

      #region Event Handlers

      partial void OnSelectedEnumItemChanged(SelectorEnum value)
         => ChildViewModel = _childViewModels.TryGetValue(value, out object? childViewModel)
                             ? childViewModel
                             : null;

      #endregion Event Handlers

      #region Overrides

      protected override void SettingsRestore()
      {
         #region Debug
#if DEBUG
         if (mainViewModelDebugBranch > 0) AppHelper.DebugOut(mainViewModelDebugBranch,
                                                              $"{AppHelper.GetCallerMemberName(this)}",
                                                              "Overriding method executing.");
#endif
         #endregion Debug
         SelectedEnumItem = _settingsService?.GetValue<SelectorEnum>("SelectedEnumItem") ?? SelectorEnum.None;
         switch (SelectedEnumItem)
         {
            case SelectorEnum.None:
               break;
            case SelectorEnum.First:
               ChildViewModel = _settingsService?.GetValue<FirstViewModel>("ChildViewModel");
               _childViewModels[SelectorEnum.First] = ChildViewModel ?? new FirstViewModel();
               break;
            case SelectorEnum.Second:
               ChildViewModel = _settingsService?.GetValue<SecondViewModel>("ChildViewModel");
               _childViewModels[SelectorEnum.Second] = ChildViewModel ?? new SecondViewModel();
               break;
            case SelectorEnum.Third:
               ChildViewModel = _settingsService?.GetValue<ThirdViewModel>("ChildViewModel");
               break;
            default:
               break;
         }
         base.SettingsRestore();
      }

      protected override void SettingsSave()
      {
         #region Debug
#if DEBUG
         if (mainViewModelDebugBranch > 0) AppHelper.DebugOut(mainViewModelDebugBranch,
                                                              $"{AppHelper.GetCallerMemberName(this)}",
                                                              "Overriding method executing.");
#endif
         #endregion Debug
         _settingsService?.SetValue("SelectedEnumItem", SelectedEnumItem);
         _settingsService?.SetValue("ChildViewModel", ChildViewModel);
         base.SettingsSave();
      }

      #endregion Overrides
   }
}
