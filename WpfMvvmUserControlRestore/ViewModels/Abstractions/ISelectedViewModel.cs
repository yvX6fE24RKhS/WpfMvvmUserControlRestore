//1.0.9040.*:1.0.9004.*//
using WpfMvvmUserControlRestore.Models.Abstractions;

namespace WpfMvvmUserControlRestore.ViewModels.Abstractions
{
   internal interface ISelectedViewModel
   {
      //      string OverallProperty { get; }
      #region Properties

      /// <summary> Несериализуемое свойство, общее для выбираемых моделей представления. </summary>
      public string NonserializablePropertyCommonToSelectedViewModels { get; set; }

      /// <summary> Сериализуемое свойство, общее для выбираемых моделей представления. </summary>
      public string SerializablePropertyCommonToSelectedViewModels { get; set; }

      /// <summary> Дата контекст, выбранной модели представления. </summary>
      public ISelectedModel<IModelSettings> SelectedModel { get; set; }

      #endregion Properties
   }
}
