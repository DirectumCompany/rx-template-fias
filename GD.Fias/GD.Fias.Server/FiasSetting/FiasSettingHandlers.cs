using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using GD.Fias.FiasSetting;

namespace GD.Fias
{
  partial class FiasSettingServerHandlers
  {

    public override void Created(Sungero.Domain.CreatedEventArgs e)
    {
      _obj.Name = FiasSettings.Resources.FiasSettingsName;
    }
  }

}