using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public CardManager cardManager;
    public UIManager uiManager;

    private void Awake() {
        I = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if(cardManager != null) {
           cardManager.GenerateCard();
        }
        Time.timeScale = 1f;
        
        SoundManager.Instance.ChangeBGM(SoundManager.BGM.easy);
        uiManager.ShowBestScore();
    }
}
