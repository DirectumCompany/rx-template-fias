using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.Fias.Structures.Module
{
  /// <summary>
  /// Структура для хранения информации об объектах из ФИАС.
  /// </summary>
  partial class AddressFias
  {
    public string full_name { get; set; }
    
    public string object_guid { get; set; }
    
    public string object_id { get; set; }
    
    public string postal_code { get; set; }
    
    public Dictionary<string, string> address_details { get; set; }
    
    public List<GD.Fias.Structures.Module.Hierarchy> hierarchy { get; set; }
  }
  
  /// <summary>
  /// Структура для хранения информации об элементах адреса из ФИАС.
  /// </summary>
  partial class Hierarchy
  {
    public int object_level_id { get; set; }

    public string name { get; set; }

    public string type_short_name { get; set; }

    public string full_name_short { get; set; }
    
    public string number { get; set; }
  }  
  
  /// <summary>
  /// Структура для хранения результатов поиска адреса в ФИАС.
  /// </summary>
  partial class AddressesFias
  {
    public List<GD.Fias.Structures.Module.AddressFias> addresses { get; set; }
    
    public bool status { get; set; }
    
    public string Error { get; set; }
  }

  /// <summary>
  /// Структура для хранения параметров подключения к сервису интеграции ФИАС.
  /// </summary>
  partial class FiasConnectParams
  {
    public string Url { get; set; }
    
    public string Token { get; set; }
  }
  
  /// <summary>
  /// Структура для заполнения полей в справочниках.
  /// </summary>
  [Public]
  partial class FiasResult
  {
    public string Address { get; set; }
    
    public string FiasGuid { get; set; }
    
    public bool FilledFromFias { get; set; }
  }
}