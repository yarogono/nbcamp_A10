using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public Text stageTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectStage() {
        SoundManager.Instance.PlaySFX(SoundManager.SFX.btnClicked);
        DataManager.Instance.currentStage = int.Parse(stageTxt.text);
        SceneManager.LoadScene("MainScene");
    }
}
