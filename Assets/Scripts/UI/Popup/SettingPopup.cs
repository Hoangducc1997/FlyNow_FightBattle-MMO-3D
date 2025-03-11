using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
public class SettingPopup : UIPopup
{

    public override void OnShown(object parament = null)
    {
        base.OnShown(parament);
        this.RegisterButtonEvents();
    }
    void RegisterButtonEvents()
    {
       
    }
}
