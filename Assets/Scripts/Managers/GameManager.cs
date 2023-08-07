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

    public Text timeTxt;
    public GameObject endTxt;
    
    private float time;
    

    private void Awake() {
        I = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        cardManager.GenerateCard();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if(time >= 30) {
            PauseGame();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0;
        endTxt.SetActive(true);
    }

    public void RetryGame() {
        SceneManager.LoadScene("MainScene");
    }
}
