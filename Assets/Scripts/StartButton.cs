using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject stagePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        SoundManager.Instance.PlaySFX(SoundManager.SFX.btnClicked);
        startPanel.SetActive(false);
        stagePanel.SetActive(true);
    }
}
