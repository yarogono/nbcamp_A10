using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSwapButton : MonoBehaviour
{
    public Animator BtnAnim;
    public Animator BtnSubAnim;
    public Text BtnText;
    int limit;
    // Start is called before the first frame update
    void Start()
    {
        limit = DataManager.Instance.limitStage;
    }

    public void MoveSide(bool left) {
        if(left && (int.Parse(BtnText.text) > 1)) {
            SoundManager.Instance.PlaySFX(SoundManager.SFX.btnClicked);
            BtnAnim.Play("StageBtn_left");
            BtnSubAnim.Play("StageBtn_sub_left");
            BtnText.text = (int.Parse(BtnText.text) - 1).ToString();
        }
        else if(!left && (int.Parse(BtnText.text) < limit)) {
            SoundManager.Instance.PlaySFX(SoundManager.SFX.btnClicked);
            BtnAnim.Play("StageBtn_right");
            BtnSubAnim.Play("StageBtn_sub_right");
            BtnText.text = (int.Parse(BtnText.text) + 1).ToString();
        }
        else {
            SoundManager.Instance.PlaySFX(SoundManager.SFX.matchFail);
        }
    }
}
