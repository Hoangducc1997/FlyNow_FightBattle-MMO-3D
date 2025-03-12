using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        this.GameLoading();
    }

    void GameLoading()
    {
        this.StartCoroutine(Ultilities.DoActionAfterSeconds(() => {
            Debug.Log("Fuckkkkk");
            UIManager.Instance.ShowPopup(PopupName.StartGamePopup);
        }, 1));
        
    }
    private IEnumerator DoActionAfterSeconds(float delay, Action CallBack)
    {
        yield return new WaitForSeconds(delay);

        CallBack?.Invoke();
    }
}
