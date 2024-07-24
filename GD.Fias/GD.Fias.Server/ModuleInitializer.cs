using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Sungero.Domain.Initialization;

namespace GD.Fias.Server
{
  public partial class ModuleInitializer
  {

    public override void Initializing(Sungero.Domain.ModuleInitializingEventArgs e)
    {
      CreateFiasSettings();
      GrantRightsToAllUsersOnDatabooks();
    }

    /// <summary>
    /// Создать запись справочника Настройки интеграции с ФИАС.
    /// </summary>
    public static void CreateFiasSettings()
    {
      if (FiasSettings.GetAll().Any())
        return;
      
      var setting = FiasSettings.Create();
      setting.Url = Constants.Module.FiasUrl;
      setting.Save();
    }

    /// <summary>
    /// Выдать права на справочники.
    /// </summary>
    public static void GrantRightsToAllUsersOnDatabooks()
    {
      InitializationLogger.Debug("Init: Grant rights on databooks.");
      
      var allUsers = Roles.AllUsers;
      
      FiasSettings.AccessRights.Grant(allUsers, DefaultAccessRightsTypes.Read);
      FiasSettings.AccessRights.Save();
    }
  }
}
