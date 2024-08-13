//1.0.8973.*//
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfMvvmUserControlRestore.Auxiliary.Helpers
{
   /// <summary>
   /// Представляет методы используемые при отладке и журналировании.
   /// </summary>
   internal static class AppHelper
   {
      #region Methods

#if DEBUG
      /// <summary> Записывает сведения об отладке в прослушиватели трассировки в коллекции Listeners. </summary>
      /// <param name="debugBranch"> Целое, содержащее номер ветви отладки <paramref name="sender"/>. </param>
      /// <param name="sender"> Отправитель сообщения. </param>
      /// <param name="message"> Строк, содержащая сообщение отладчика. </param>
      internal static void DebugOut(int? debugBranch, string sender, string? message = null)
      {
         if (debugBranch != null) Debug.WriteLine($"Отладка: {sender} : debugBranch {debugBranch} : {message ?? "executing."}");
      }
#endif

      /// <summary> Возвращает текущий рабочий каталог приложения. </summary>
      /// <returns> Строка, содержащая абсолютный путь к текущей рабочей папке без обратной косой черты (\) в конце. </returns>
      internal static string GetAppDir() => Directory.GetCurrentDirectory();

      /// <summary> Возвращает имя исполняемого файла приложения. </summary>
      /// <returns> Строка содержащая имя исполняемого файла приложения. </returns>
      internal static string GetAppName() => AppDomain.CurrentDomain.FriendlyName;

      /// <summary> Возвращает строковое представление версии сборки проекта. </summary>
      /// <returns> Строка содержащая версию сборки проекта. </returns>
      internal static string GetAssemblyVersion() => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "version undefined";

      /// <summary> Возвращает имя свойства или метода вызывающего объект. </summary>
      /// <param name="obj">вызываемый объект.</param>
      /// <param name="memberName">имя свойства или метода вызывающего метод объекта.</param>
      /// <returns>Строка, содержащая имя вызывающего объекта.</returns>
      internal static string GetCallerMemberName(object obj, [CallerMemberName] string memberName = "") => $"{obj.GetType()}.{memberName}";

      /// <summary> Возвращает имя свойства или метода. </summary>
      /// <param name="name">имя свойства или метода.</param>
      /// <returns>Строка, содержащая имя вызывающего объекта.</returns>
      internal static string GetCallerName([CallerMemberName] string name = "") => name;

      /// <summary> Приведение значения перечисления в удобочитаемый формат. </summary>
      /// <remarks> Для корректной работы необходимо использовать атрибут [Description("Name")] для каждого элемента перечисления. </remarks>
      /// <example> <see href="https://shwandotru.wordpress.com/2017/12/07/enum-description/"/> </example>
      /// <param name="enumElement">Элемент перечисления</param>
      /// <returns>Название элемента</returns>
      internal static string GetDescription(Enum enumElement)
      {
         Type type = enumElement.GetType();

         MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
         if (memInfo != null && memInfo.Length > 0)
         {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs != null && attrs.Length > 0)
               return ((DescriptionAttribute)attrs[0]).Description;
         }

         return enumElement.ToString();
      }

      /// <summary> Возвращает строковые представление свойств экземпляра объекта. </summary>
      /// <typeparam name="T"> Тип объекта.</typeparam>
      /// <param name="obj"> Экземпляр объекта. </param>
      /// <returns>Строка, содержащая значений свойств. </returns>
      internal static string GetInstanceInfo<T>(T obj)
      {
         StringBuilder sb = new();

         typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
            .Select(x => new KeyValuePair<string, string>(x.Name, x?.GetValue(obj)?.ToString() ?? "null"))
            .ToList()
            .ForEach(x => sb.AppendLine($"\t\"{x.Key}\" : \"{x.Value}\""));

         return sb.ToString();
      }

      /// <summary> Возвращает имя текущего пользователя Windows. </summary>
      /// <returns> Строка, содержащая имя текущего пользователя. </returns>
      internal static string GetUserName() => System.Security.Principal.WindowsIdentity.GetCurrent().Name;

      #endregion Methods
   }
}
