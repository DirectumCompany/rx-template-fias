using System;
using Sungero.Core;

namespace GD.Fias.Constants
{
  public static class Module
  {

    /// <summary>
    // Адрес сервиса ФИАС по умолчанию.
    /// </summary>
    public const string FiasUrl = "https://fias-public-service.nalog.ru/api/spas/v2.0/";
    
    /// <summary>
    // Метод сервиса ФИАС для получения адреса.
    /// </summary>
    public const string MethodSearchAddress = "SearchAddressItems";
    
    /// <summary>
    // Строка параметров метода сервиса ФИАС для получения адреса.
    /// </summary>
    public const string ParamsSearchAddress = "search_string={0}&address_type=1";
    
    /// <summary>
    // Имя заголовка для авторизации.
    /// </summary>
    public const string TokenHeader = "master-token";
    
    /// <summary>
    /// Максимальное количество записей, которое возвращает сервис ФИАС.
    /// </summary>
    public const int MaxNumbersOfResult = 5;
  }
}
