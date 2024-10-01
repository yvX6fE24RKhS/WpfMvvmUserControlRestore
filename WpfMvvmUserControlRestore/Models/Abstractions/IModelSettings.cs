//1.0.9040.*//
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvmUserControlRestore.Models.Abstractions
{
   internal interface IModelSettings
   {
      #region Properties

      /// <summary> Несериализуемое свойство, общее для настроек модели. </summary>
      public string NonserializablePropertyCommonToModelSettings { get; set; }

      /// <summary> Сериализуемое свойство, общее для настроек модели. </summary>
      public string SerializablePropertyCommonToModelSettings { get; set; }

      #endregion Properties
   }
}
