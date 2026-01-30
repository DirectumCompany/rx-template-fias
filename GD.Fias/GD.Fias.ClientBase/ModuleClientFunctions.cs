using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace GD.Fias.Client
{
  public partial class ModuleFunctions
  {
    /// <summary>
    /// Отобразить настройки интеграции с ФИАС.
    /// </summary>
    [LocalizeFunction("ShowFiasSettingsName", "")]
    public virtual void ShowFiasSettings()
    {
      var settings = Functions.Module.Remote.GetFiasSettings();
      if (settings == null)
        Dialogs.NotifyMessage(Resources.FiasSettingsIsEmptyError);
      else
        settings.Show();
    }

    /// <summary>
    /// Отобразить диалог получения идентификатора адреса из ФИАС.
    /// </summary>
    [LocalizeFunction("ShowFiasDialogName", "")]
    public virtual void GetFiasInfo()
    {
      var fiasResult = ShowDialogFias(string.Empty);
      if (!string.IsNullOrEmpty(fiasResult?.Address))
        Dialogs.ShowMessage(Fias.Resources.FiasResulMessageFormat(fiasResult.Address, fiasResult.FiasGuid));
    }
    
    /// <summary>
    /// Диалоговое окно работы с адресами ФИАС.
    /// </summary>
    /// <param name="postalAddress">Адрес в виде строки.</param>
    /// <returns>Найденная в ФИАС информация.</returns>
    [Public]
    public Structures.Module.IFiasResult ShowDialogFias(string postalAddress)
    {
      var result = Structures.Module.FiasResult.Create(string.Empty, string.Empty, false);
      if (Sungero.Company.Employees.Current == null)
      {
        Dialogs.NotifyMessage(Resources.FiasSettingsIsEmptyError);
        return result;
      }
      
      var settings = Functions.Module.Remote.GetUrlFias(Sungero.Company.Employees.Current.Department.BusinessUnit);
      var url = settings.Url;
      var token = settings.Token;
      if (string.IsNullOrEmpty(token))
      {
        Dialogs.NotifyMessage(Resources.FiasSettingsIsEmptyError);
        return result;
      }
      var addressStruct = Structures.Module.AddressesFias.Create();
      var addressesString = new string[Constants.Module.MaxNumbersOfResult];
      var error = string.Empty;
      if (!string.IsNullOrEmpty(postalAddress))
      {
        addressStruct = Functions.Module.Remote.RefreshAddress(postalAddress, url, token);
        if (!string.IsNullOrEmpty(addressStruct.Error))
        {
          error = addressStruct.Error;
          Logger.ErrorFormat("OpenDialogFias - Start - Error - {0}", error);
          addressStruct.Error = string.Empty;
        }
        else
          addressesString = addressStruct.addresses.Select(a => a.full_name).ToArray();
      }
      
      // Создать диалог ввода.
      var dialog = Dialogs.CreateInputDialog(Resources.FiasDialogTitle);
      dialog.Text = string.IsNullOrEmpty(error) ? Resources.FiasDialogTextFormat(addressesString.Length.ToString()) : Resources.FiasDialogErrorFormat(error);
      
      // Добавить поле для выбора адреса
      var addressString = dialog.AddString(Resources.PostalAddressLabel, true, postalAddress);
      var addressField = dialog.AddSelect(Resources.FiasAddressLabel, true, 0).From(addressesString);
      var missingFiasAddress = dialog.AddBoolean(Resources.NotFoundLabel, false);
      addressString.SetOnValueChanged((x) =>
                                      {
                                        if (x.NewValue != x.OldValue && !string.IsNullOrEmpty(x.NewValue) && !missingFiasAddress.Value.Value)
                                        {
                                          addressStruct = Functions.Module.Remote.RefreshAddress(x.NewValue,
                                                                                                 url, token);
                                          if (!string.IsNullOrEmpty(addressStruct.Error))
                                          {
                                            Logger.ErrorFormat("OpenDialogFias - Text - Error - {0}", addressStruct.Error);
                                            dialog.Text = Resources.FiasDialogErrorFormat(addressStruct.Error);
                                            addressField.From(string.Empty);
                                            addressField.Value = string.Empty;
                                            addressStruct.Error = string.Empty;
                                          }
                                          else
                                          {
                                            addressesString = addressStruct.addresses.Select(a => a.full_name).ToArray();
                                            if (addressesString.Length > 0)
                                            {
                                              dialog.Text = Resources.FiasDialogTextFormat(addressesString.Length.ToString());
                                              addressField.From(addressesString);
                                              addressField.ValueIndex = 0;
                                            }
                                            else
                                            {
                                              dialog.Text = Resources.FiasDialogTextFormat("0");
                                              addressField.From(string.Empty);
                                              addressField.Value = string.Empty;
                                            }
                                          }
                                        }
                                      });
      missingFiasAddress.SetOnValueChanged((x) =>
                                           {
                                             addressField.IsEnabled = !x.NewValue.Value;
                                             addressField.IsRequired = !x.NewValue.Value;
                                             if (x.NewValue.Value == false && !string.IsNullOrEmpty(addressString.Value))
                                             {
                                               addressStruct = Functions.Module.Remote.RefreshAddress(addressString.Value, url, token);
                                               if (!string.IsNullOrEmpty(addressStruct.Error))
                                               {
                                                 Logger.ErrorFormat("OpenDialogFias - CheckBox - Error - {0}", addressStruct.Error);
                                                 dialog.Text = Resources.FiasDialogErrorFormat(addressStruct.Error);
                                                 addressField.From(string.Empty);
                                                 addressField.Value = string.Empty;
                                                 addressStruct.Error = string.Empty;
                                               }
                                               else
                                               {
                                                 addressesString = addressStruct.addresses.Select(a => a.full_name).ToArray();
                                                 if (addressesString.Length > 0)
                                                 {
                                                   dialog.Text = Resources.FiasDialogTextFormat(addressesString.Length.ToString());
                                                   addressField.From(addressesString);
                                                   addressField.ValueIndex = 0;
                                                 }
                                                 else
                                                 {
                                                   dialog.Text = Resources.FiasDialogTextFormat("0");
                                                   addressField.From(string.Empty);
                                                   addressField.Value = string.Empty;
                                                 }
                                               }
                                             }
                                             else
                                             {
                                               dialog.Text = Resources.FiasDialogTextFormat("0");
                                               addressField.From(string.Empty);
                                               addressField.Value = string.Empty;
                                             }
                                           });
      
      if (dialog.Show() == DialogButtons.Ok)
      {
        // Сформировать строку из результатов ввода.
        if (missingFiasAddress.Value.Value || string.IsNullOrEmpty(addressField.Value))
        {
          result.Address = addressString.Value;
          return result;
        }
        else
        {
          var currentAddress = addressStruct.addresses.Where(a => a.full_name == addressField.Value).FirstOrDefault();
          if (currentAddress != null)
          {
            var postalCode =  currentAddress.address_details["postal_code"];
            if (!string.IsNullOrEmpty(postalCode))
              postalCode = string.Format("{0}, ", postalCode);
            var gostAddress = string.Join(", ", currentAddress.hierarchy
                                          .Select(h => { if (h.object_level_id >= 9) return h.full_name_short;
                                                    else return string.Format("{0} {1}", h.name, h.type_short_name); }));
            
            result.Address = string.Format("{0}{1}", postalCode, gostAddress);
            result.FiasGuid = currentAddress.object_guid;
            result.FilledFromFias = true;
          }
          else
          {
            result.Address = addressField.Value;
            result.FiasGuid = string.Empty;
            result.FilledFromFias = false;
          }
          return result;
        }
      }
      return result;
    }
  }
}