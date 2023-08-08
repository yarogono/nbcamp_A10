using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSwapButton : MonoBehaviour
{
    public Animator BtnAnim;
    public Animator BtnSubAnim;
    public Text BtnText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveSide(bool left) {
        if(left && (int.Parse(BtnText.text) > 1)) {
            BtnAnim.Play("StageBtn_left");
            BtnSubAnim.Play("StageBtn_sub_left");
            BtnText.text = (int.Parse(BtnText.text) - 1).ToString();
        }
        else if(!left) {
            BtnAnim.Play("StageBtn_right");
            BtnSubAnim.Play("StageBtn_sub_right");
            BtnText.text = (int.Parse(BtnText.text) + 1).ToString();
        }
        else {
            //사운드매니저가 여기 없음
            //GameManager.I.soundManager.PlaySFX(SoundManager.SFX.matchFail);
        }
    }
}
