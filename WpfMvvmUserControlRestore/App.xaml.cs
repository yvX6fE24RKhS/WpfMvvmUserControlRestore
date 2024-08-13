//1.0.8991.*//
using System.Runtime.Versioning;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WpfMvvmUserControlRestore.Auxiliary.Helpers;
using WpfMvvmUserControlRestore.Services.Settings.Abstractions;
using WpfMvvmUserControlRestore.Services.Settings.Implementations;
using WpfMvvmUserControlRestore.ViewModels;

namespace WpfMvvmUserControlRestore
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      #region Fields

      #region Debug
#if DEBUG
#pragma warning disable 0649
      private static readonly int? debugBranch;
#pragma warning restore 0649
#endif
      #endregion Debug

      #endregion Fields

      #region Constructors

      /// <summary> Инициализирует экземпляр класса <see cref="App"/> WPF приложения, являющегося точкой входа. </summary>
      public App()
      {
         // REMARKS: Fix Error: No data is available for encoding 1251
         Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
         Services = ConfigureServices();
      }

      #endregion Constructors

      #region Properties

      /// <summary> Возвращает текущий используемый экземпляр класса <see cref="App"/> WPF приложения. </summary>
      public new static App Current => (App)Application.Current;

      /// <summary> Возвращает экземпляр <see cref="IServiceProvider"/> поставщика услуг, обеспечивающий настраиваемую поддержку для других объектов. </summary>
      public IServiceProvider Services { get; }

      #endregion Properties

      #region Event Handlers

      // REMARKS: вариант обработчика необработанного события исключения
      ///// <summary> Обработчик события, которое генерируется при возникновении исключения, не обработанного ни одним доменом приложения. </summary>
      ///// <param name="sender"> Источник необработанного события исключения. </param>
      ///// <param name="e"> Аргументы, содержащие данные события. </param>
      //private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
      //{
      //   // Обработать необработанное исключение
      //}

      /// <summary> Обработчик исключения в потоке пользовательского интерфейса (диспетчер). </summary>
      /// <param name="sender"> Источник необработанного события исключения. </param>
      /// <param name="e"> Аргументы, содержащие данные события. </param>
      private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
      {
         #region Debug
#if DEBUG
         AppHelper.DebugOut(debugBranch ?? 0,
                            $"{AppHelper.GetCallerMemberName(this)}",
                            $"Not handled Exception: {e.Exception.Message}");
#endif
         #endregion Debug

         // Завершить работу приложения с ошибкой
         e.Handled = true;
         Application.Current.Shutdown(e.Exception.HResult);
      }

      /// <inheritdoc/>
      [SupportedOSPlatform("windows")]
      protected override async void OnExit(ExitEventArgs e)
      {
         ISettingsService? settingsService = Services.GetService<ISettingsService>();
         if (settingsService != null)
         {
            try
            {
               #region Debug
#if DEBUG
               if (debugBranch == 3) throw new Exception("Эмуляция ошибки сохранения параметров приложения.");
#endif
               #endregion Debug
               await settingsService.SaveAsync(default);
            }
            catch (Exception ex)
            {
               #region Debug
#if DEBUG
               AppHelper.DebugOut(debugBranch ?? 0,
                                  $"{nameof(App)}.{AppHelper.GetCallerName()}",
                                  $"Ошибка сохранения параметров приложения. {ex}");
#endif
               #endregion Debug
            }
         }
         base.OnExit(e);
      }

      /// <inheritdoc/>
      [SupportedOSPlatform("windows")]
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         // Подписка на событие которое происходит, когда приложение создает исключение, но не обрабатывает его.
         DispatcherUnhandledException += OnDispatcherUnhandledException;
         // Что аналогично XAML <Application DispatcherUnhandledException = "App_DispatcherUnhandledException"
         // или
         // Подписка на событие которое не обрабатывается доменом приложения.
         // AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnCurrentDomainUnhandledException);
         #region Debug
#if DEBUG
         AppHelper.DebugOut(debugBranch ?? 0,
                            $"{AppHelper.GetCallerMemberName(this)}",
                            $"Application version {AppHelper.GetAssemblyVersion()}.");

         if (debugBranch == 1) throw new Exception("Эмуляция необработанной ошибки Startup Event Handler.");
#endif
         #endregion Debug
      }

      #endregion Event Handlers

      #region Methods

      /// <summary> Настраивает службы для приложения. </summary>
      private static Ioc ConfigureServices()
      {
         ServiceCollection services = new();

         try
         {
            #region Debug
#if DEBUG
            if (debugBranch == 2) throw new Exception("Эмуляция ошибки конфигурации служб приложения.");
#endif
            #endregion Debug
            Ioc.Default.ConfigureServices(
                services
                // Services
                .AddSingleton<ISettingsService, SettingsService>()
                // View Models
                .AddTransient<MainViewModel>()
                .AddTransient<FirstViewModel>()
                .BuildServiceProvider()
            );

         }
         catch (Exception ex)
         {
            #region Debug
#if DEBUG
            AppHelper.DebugOut(debugBranch ?? 0,
                               $"{nameof(App)}.{AppHelper.GetCallerName()}",
                               $"Ошибка конфигурации служб приложения. {ex}");
#endif
            #endregion Debug
            Application.Current.Shutdown(ex.HResult);
         }

         return Ioc.Default;
      }

      #endregion Methods
   }
}
