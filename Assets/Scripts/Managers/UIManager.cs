using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private float time;
    public bool TimeOver;
    public bool enterRedTime;
    public float FailPanelty = 3; // 실패 패널티
    public float RedTime; // 경고 시간
    public float EndTime; // 제한 시간
    public int NumTotal;
    public int NumFail;
    int totalScore;
    public Text timeTxt;
    public Text FailNumTxt;
    public Text TotalNumTxt;
    public Text totalScoreTxt;
    public Text matchTxt;
    public Text bestTxt;
    public GameObject endTxt;
    public GameObject NumCanvas;
    // Update is called once per frame

    private void Awake()
    {
        TimeOver = false;
        enterRedTime = false;
        EndTime -= DataManager.Instance.currentStage;
    }
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time >= RedTime && !enterRedTime)
        {
            enterRedTime = true;
            timeTxt.color = Color.red;
            SoundManager.Instance.ChangeBGM(SoundManager.BGM.busy);
        }
        if (time >= EndTime && !TimeOver)
        {
            TimeOver = true;
            ActiveEndText();
        }
    }

    public void ActiveEndText()
    {
        Time.timeScale = 0;
        MakeScore();
        DataManager.Instance.SaveData(totalScore);
        SoundManager.Instance.ChangeBGM(SoundManager.BGM.stop);
        SetNumCanvas();
        NumCanvas.SetActive(true);
        endTxt.SetActive(true);
    }

    public void PlusNumFail()
    {
        NumFail++;
        NumTotal++;
    }
    public void PlusTotal()
    {
        NumTotal++;
    }
    public void SetNumCanvas()
    {
        FailNumTxt.text = "총 실패 횟수: " + NumFail.ToString();
        TotalNumTxt.text = "총 시도 횟수: " + NumTotal.ToString();
        totalScoreTxt.text = "점수 합계: " + totalScore.ToString();
    }
    public void Penalty()
    {
        time += FailPanelty;
    }
    public void MatchResult(string result) {
        matchTxt.text = result;
        matchTxt.gameObject.SetActive(true);
        Invoke("MatchResultHide", 1);
    }
    void MatchResultHide() {
        matchTxt.gameObject.SetActive(false);
    }
    void MakeScore() {
        totalScore = (int)((EndTime - time) * 100) - NumTotal - NumFail;
        if(!enterRedTime) totalScore = (int)(totalScore * 1.2f);
    }
    public void ShowBestScore() {
        bestTxt.text = $"{DataManager.Instance.currentStage} 스테이지\n최고 점수: {DataManager.Instance.StageBestScore()}";
    }
}
