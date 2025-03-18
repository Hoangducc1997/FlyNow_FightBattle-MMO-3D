using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
public class StartGamePopup : UIPopup
{
    [SerializeField] Button _StartGameBtn;
    [SerializeField] Button _TutorialBtn;
    [SerializeField] Button _SettingBtn;
    [SerializeField] Button _ExitGameBtn;

    public override void OnShown(object parament = null)
    {
        base.OnShown(parament);
        this.RegisterButtonEvents();
    }
    void RegisterButtonEvents()
    {
        _StartGameBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayVFX("Click UI");
            GameSceneManager.Instance.LoadScene(SceneName.Fight);
            this.OnHide();
        });

        _SettingBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayVFX("Click UI");
            this.OnHide();
            UIManager.Instance.ShowPopup(PopupName.SettingPopup);
        });

        _TutorialBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayVFX("Click UI");
            this.OnHide();
            GameSceneManager.Instance.LoadScene(SceneName.Tutorial);
        });
        _ExitGameBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayVFX("Click UI");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else

                    Application.Quit();
#endif
        });
    }
}
