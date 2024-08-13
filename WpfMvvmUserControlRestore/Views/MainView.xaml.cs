//1.0.8991.*//
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;
using WpfMvvmUserControlRestore.ViewModels;

namespace WpfMvvmUserControlRestore.Views
{
   /// <summary>
   /// Логика взаимодействия для MainView.xaml
   /// </summary>
   public partial class MainView : Window
   {
      #region Fields

      #region Debug
#if DEBUG
#pragma warning disable 0649
      private static readonly int? debugBranch;
#pragma warning restore 0649
#endif
      #endregion Debug

      private readonly MainViewModel _mainViewModel;

      #endregion Fields

      #region Constructors

      /// <summary> Инициализирует экземпляр класса <see cref="MainView"/> </summary>
      public MainView()
      {
         #region Debug
#if DEBUG
         if (debugBranch == 1) AppHelper.DebugOut(debugBranch, $"{AppHelper.GetCallerMemberName(this)}");
#endif
         #endregion Debug
         InitializeComponent();
         _mainViewModel = App.Current.Services.GetService<MainViewModel>()!;
         DataContext = _mainViewModel;
         Closing += _mainViewModel.OnClosing;
      }

      #endregion Constructors
   }
}
