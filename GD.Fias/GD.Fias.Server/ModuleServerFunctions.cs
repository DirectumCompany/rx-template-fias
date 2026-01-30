using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.Fias.Server
{
  public partial class ModuleFunctions
  {
    /// <summary>
    /// Обновить адреса в выпадающем списке.
    /// </summary>
    /// <param name="address">Строка для поиска.</param>
    /// <param name="url">Адрес сервиса интеграции.</param>
    /// <param name="token">Токен авторизации.</param>
    /// <returns>Структура с найденными адресами.</returns>
    [Remote(IsPure = true)]
    public Structures.Module.AddressesFias RefreshAddress(string address,
                                                          string url,
                                                          string token)
    {
      using (var client = new HttpClient())
      {
        var addressesFias = Structures.Module.AddressesFias.Create();
        try
        {
          var builder = new UriBuilder(url + Constants.Module.MethodSearchAddress);
          builder.Query = string.Format(Constants.Module.ParamsSearchAddress, address);
          url = builder.ToString();
          client.DefaultRequestHeaders.Add(Constants.Module.TokenHeader, token);
          var response = client.GetAsync(url).Result;
          if (response.StatusCode == HttpStatusCode.OK)
          {
            var result = response.Content.ReadAsStringAsync().Result;
            addressesFias = JsonConvert.DeserializeObject<Structures.Module.AddressesFias>(result);
          }
          else if (response.StatusCode == HttpStatusCode.NotFound)
            addressesFias.Error = Resources.ServiceNotFound;
          else if (response.StatusCode == HttpStatusCode.InternalServerError)
            addressesFias.Error = Resources.ServiceIsUnavailable;
          else if (response.StatusCode == HttpStatusCode.Forbidden)
            addressesFias.Error = Resources.InvalidToken;
          else
            addressesFias.Error = response.Content.ReadAsStringAsync().Result;
            
          return addressesFias;
        }
        catch (Exception ex)
        {
          addressesFias.Error = ex.Message;
          return addressesFias;
        }
      }
    }
    
    /// <summary>
    /// Получение параметров подключения к сервису интеграции ФИАС.
    /// </summary>
    /// <param name="businessUnit">Наша организация.</param>
    /// <returns>Структура с параметрами подключения.</returns>
    [Remote(IsPure = true)]
    public Structures.Module.FiasConnectParams GetUrlFias(Sungero.Company.IBusinessUnit businessUnit)
    {
      var connectParams = Structures.Module.FiasConnectParams.Create();
      var settings = GetFiasSettings();
      if (settings == null)
        return connectParams;
      connectParams.Url = settings.Url;
      var token = settings.BusinessUnitSettings.Where(x => Equals(x.BusinessUnit, businessUnit)).Select(x => x.Token).FirstOrDefault();
      if (string.IsNullOrEmpty(token))
        token = settings.BusinessUnitSettings.Where(x => x.BusinessUnit == null).Select(x => x.Token).FirstOrDefault();
      connectParams.Token = token;
      return connectParams;
    }
    
    /// <summary>
    /// Получение записи справочника Настройки интеграции с ФИАС.
    /// </summary>
    /// <returns>Настройки интеграции с ФИАС.</returns>
    [Public, Remote(IsPure = true)]
    public IFiasSetting GetFiasSettings()
    {
      return FiasSettings.GetAll().FirstOrDefault();
    }
  }
}