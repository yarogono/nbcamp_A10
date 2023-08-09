using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    Animator anim;
    public Vector3 GoalPosition;
    float x;
    float y;
    bool isFixed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        x = transform.position.x;
        y = transform.position.y;
    }

    void Update() {
        if(Vector3.Distance(GoalPosition, transform.position) > 0.0001f) {
            x = Mathf.Lerp(x, GoalPosition.x, Time.deltaTime);
            y = Mathf.Lerp(y, GoalPosition.y, Time.deltaTime);
            transform.position = new Vector3(x, y, 0);
        }
        else {
            if(!isFixed) {
                isFixed = true;
                transform.position = GoalPosition;
                GameManager.I.cardManager.FixedPosition();
            }
        }
    }

    public void OpenCard() {
        if(!anim.GetBool("isOpen")) {
            SoundManager.Instance.PlaySFX(SoundManager.SFX.flip);
            anim.SetBool("isOpen", true);
            Invoke("flipCard", 0.333f); //card_flip의 길이가 0.667f
            OpenCardBackColorChange();
        }
    }

    void flipCard() {
        if(anim.GetBool("isOpen")) {
            transform.Find("Front").gameObject.SetActive(true);
            transform.Find("Back").gameObject.SetActive(false);

            if(GameManager.I.cardManager.firstCard == null) {
                GameManager.I.cardManager.firstCard = gameObject;
            } else {
                GameManager.I.cardManager.secondCard = gameObject;
                GameManager.I.cardManager.IsMatched();
            }
        }
        else {
            transform.Find("Back").gameObject.SetActive(true);
            transform.Find("Front").gameObject.SetActive(false);
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        Invoke("flipCard", 0.333f);
    }

    private void OpenCardBackColorChange()
    {
        transform.Find("Back").GetComponent<SpriteRenderer>().color = Color.gray;
    }
}
