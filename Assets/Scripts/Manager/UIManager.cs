using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    void Awake()
    {
        this.InitUIManager();
    }

    [SerializeField] List<UIPopup> _UIPopupList = new List<UIPopup>();
    private UIPopup _currentPopup;
    private UIPopup _lastPopup;
    public UIPopup CurrentPopup => _currentPopup;


    #region UIManager
    private void InitUIManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public void ShowPopup(PopupName popupName, object popupParamenter = null, bool hideOther = false)
    {
        // Hide current popup if required
        if (hideOther && _currentPopup != null)
        {
            HidePopup(_currentPopup.GetPopupName());
            _lastPopup = _currentPopup;
        }

        // Loop through the list and find the popup that matches the PopupName
        foreach (var popup in _UIPopupList)
        {
            if (popup.GetPopupName() == popupName)
            {
                popup.OnShown(popupParamenter); // Show the new popup
                _currentPopup = popup;  // Set the new popup as the current popup
                break;
            }
        }
    }



    public void HidePopup(PopupName popupName, bool isShowLast = false)
    {
        if (_currentPopup != null && _currentPopup.GetPopupName() == popupName)
        {
            _currentPopup.OnHide();
            return;
        }

        foreach (var popup in _UIPopupList)
        {
            if (popup.GetPopupName() == popupName)
            {
                popup.OnHide();
            }
        }

        if (isShowLast)
        {
            _lastPopup?.OnShown();
        }
    }

    public UnityEngine.GameObject GetPopupByName(PopupName popupName)
    {
        foreach (var popup in _UIPopupList)
        {
            if (popup.GetPopupName() == popupName) { return popup.gameObject; }
        }
        return null;
    }
}