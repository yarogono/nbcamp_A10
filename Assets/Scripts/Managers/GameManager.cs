using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public SoundManager soundManager;
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
        
        soundManager.ChangeBGM(SoundManager.BGM.easy);
    }

    public void RetryGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void PauseGame()
    {
        uiManager.ActiveEndText();
    }

}
