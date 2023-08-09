using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private float time;
    public bool TimeOver;
    public float FailPanelty = 3; // 실패 패널티
    public float RedTime; // 경고 시간
    public float EndTime; // 제한 시간
    public int NumTotal;
    public int NumFail;
    public Text timeTxt;
    public Text FailNumTxt;
    public Text TotalNumTxt;
    public Text matchTxt;
    public GameObject endTxt;
    public GameObject NumCanvas;
    // Update is called once per frame

    private void Awake()
    {
        TimeOver = false;
    }
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time >= RedTime)
        {
            timeTxt.color = Color.red;
            //GameManager.I.soundManager.ChangeBGM(SoundManager.BGM.busy);
            if (time >= EndTime)
            {
                TimeOver = true;
                ActiveEndText();
            }

        }
    }

    public void ActiveEndText()
    {
        SetNumCanvas();
        NumCanvas.SetActive(true);
        Time.timeScale = 0;
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
        FailNumTxt.text = "총 실패 횟수 : " + NumFail.ToString();
        TotalNumTxt.text = "총 시도 횟수 : " + NumTotal.ToString();
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
}
