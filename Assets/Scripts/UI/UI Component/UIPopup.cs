using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [Header("Popup Name")]
    [SerializeField] PopupName _popupName;
    protected object _popupParament;

    #region Unity Function

    #endregion

    #region UI Function
    public virtual void OnShown(object parament = null)
    {
        if (parament != null)
        {
            _popupParament = parament;
        }
        this.gameObject.SetActive(true);
    }

    public virtual void OnHide()
    {
        this.gameObject.SetActive(false);
    }

    public PopupName GetPopupName() { return _popupName; }
    public void SetParamenter(object parament)
    {
        this._popupParament = parament;
    }

    public virtual bool OnBackClick()
    {
        return true;
    }
    #endregion


}