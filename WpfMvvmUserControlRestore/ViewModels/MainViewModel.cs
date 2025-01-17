﻿//1.0.9040.*:1.0.9004.*//
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
      #region Debug
#if DEBUG
#pragma warning disable
      private static readonly int? mainViewModelDebugBranch = 1;
#pragma warning restore
#endif
      #endregion Debug

      #region Fields

      /// <summary> Версия текущей сборки приложения. </summary>
      [ObservableProperty]
      private string _applicationAssemblyVersion = AppHelper.GetAssemblyVersion();

      [ObservableProperty]
      private SelectorEnum _selectedEnumItem = SelectorEnum.None;

      /// <summary> Словарь для экземпляров выбираемых моделей представления. </summary>
      readonly Dictionary<SelectorEnum, ISelectedViewModel> _childViewModels = [];

      /// <summary> Текущая модель представления. </summary>
      [ObservableProperty]
      private ISelectedViewModel? _childViewModel;

      #endregion Fields

      #region Constructors

      /// <summary> Инициализирует экземпляр класса <see cref="MainViewModel"/>. </summary>
      public MainViewModel(ISettingsService settingsService) : base(settingsService)
      {
         _childViewModels.TryAdd(SelectorEnum.First, new FirstViewModel());
//         _childViewModels.TryAdd(SelectorEnum.Second, new SecondViewModel());
//         _childViewModels.TryAdd(SelectorEnum.Third, new ThirdViewModel());
      }

      #endregion Constructors

      #region Event Handlers

      partial void OnSelectedEnumItemChanged(SelectorEnum value)
         => ChildViewModel = _childViewModels.TryGetValue(value, out ISelectedViewModel? childViewModel)
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
               _childViewModels[SelectorEnum.First] = ChildViewModel as FirstViewModel ?? new FirstViewModel();
               break;
            //case SelectorEnum.Second:
            //   ChildViewModel = _settingsService?.GetValue<SecondViewModel>("ChildViewModel");
            //   _childViewModels[SelectorEnum.Second] = ChildViewModel as SecondViewModel ?? new SecondViewModel();
            //   break;
            //case SelectorEnum.Third:
            //   ChildViewModel = _settingsService?.GetValue<ThirdViewModel>("ChildViewModel");
            //   _childViewModels[SelectorEnum.Third] = ChildViewModel as ThirdViewModel ?? new ThirdViewModel();
            //   break;
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
