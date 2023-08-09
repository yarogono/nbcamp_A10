using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    [HideInInspector] public int limitStage;
    [HideInInspector] public int currentStage;
    int clearStage;
    int bestScore;


    void Awake() {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if(!PlayerPrefs.HasKey("data"))
            Init();
        bestScore = PlayerPrefs.GetInt("score");
        clearStage = PlayerPrefs.GetInt("clear");
        limitStage = PlayerPrefs.GetInt("limit");
    }

    void Init() {
        PlayerPrefs.SetInt("data", 1);
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("clear", 0);
        PlayerPrefs.SetInt("limit", 1);
    }

    public void SaveData(int score) {
        if(bestScore < score) {
            bestScore = score;
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.SetInt(currentStage.ToString(), score);
        }
        if(!GameManager.I.uiManager.TimeOver && clearStage < currentStage) {
            clearStage = currentStage;
            PlayerPrefs.SetInt("clear", currentStage);
            limitStage++;
            PlayerPrefs.SetInt("limit", limitStage);
        }
    }

    public int StageBestScore() {
        if(PlayerPrefs.HasKey(currentStage.ToString())) {
            return PlayerPrefs.GetInt(currentStage.ToString());
        }
        else {
            return 0;
        }
    }
}
