//1.0.9040.*//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvmUserControlRestore.Models.Abstractions
{
   internal interface ISelectedModel<T> where T : class, IModelSettings
   {
      #region Properties

      /// <summary> Несериализуемое свойство, общее для выбираемых моделей. </summary>
      public string NonserializablePropertyCommonToSelectedModels { get; set; }

      /// <summary> Сериализуемое свойство, общее для выбираемых моделей. </summary>
      public string SerializablePropertyCommonToSelectedModels { get; set; }

      /// <summary> Настройки модели. </summary>
      public T ModelSettings { get; set; }

      #endregion Properties
   }
}
