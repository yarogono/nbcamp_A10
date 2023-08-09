using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject card;
    float timer;
    int cardsLeft;
    int cardCnt;
    [SerializeField] private Vector3 deckPosition; 
    [HideInInspector] public GameObject firstCard;
    [HideInInspector] public GameObject secondCard;

    // Update is called once per frame
    void Update()
    {
        if(firstCard != null) {
            timer += Time.deltaTime;
            if(timer >= 5) {
                firstCard.GetComponent<Card>().CloseCard();
                firstCard = null;
                timer = 0;
            }
        }
    }

    public void GenerateCard() {
        int[] cards = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        cards = cards.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        cardsLeft = cards.Length;
        cardCnt = cards.Length;

        float cardTerm = card.transform.localScale.x + 0.1f;
            
        for(int i = 0; i < 16; i++) {
            float x = (i%4) * cardTerm - 2.1f;
            float y = (i/4) * cardTerm - 3.0f;

            GameObject newCard = Instantiate(card, deckPosition, Quaternion.identity);
            newCard.transform.parent = GameObject.Find("Cards").transform;
            
            newCard.GetComponent<Card>().GoalPosition = new Vector3(x, y, 0);

            string cardName = "card" + cards[i].ToString();
            newCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
            
        }

        
    }

    public void IsMatched() {
        string firstCardImage = firstCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;

        if(firstCardImage == secondCardImage) {
            GameManager.I.uiManager.MatchResult(WhoAreYou(firstCardImage));
            GameManager.I.uiManager.PlusTotal();
            SoundManager.Instance.PlaySFX(SoundManager.SFX.matchSuccess);
            firstCard.GetComponent<Card>().DestroyCard();
            secondCard.GetComponent<Card>().DestroyCard();
            cardsLeft -= 2;

            if(cardsLeft == 0) {
                GameManager.I.uiManager.ActiveEndText();
            }
        }
        else {
            GameManager.I.uiManager.MatchResult("실패");
            GameManager.I.uiManager.PlusNumFail();
            GameManager.I.uiManager.Penalty();
            SoundManager.Instance.PlaySFX(SoundManager.SFX.matchFail);
            firstCard.GetComponent<Card>().CloseCard();
            secondCard.GetComponent<Card>().CloseCard();  
        }

        firstCard = null;
        secondCard = null;
        timer = 0;
    }

    string WhoAreYou(string name) {
        if(name == "card0" || name == "card1" || name == "card2" || name == "card3") {
            return "이정환";
        }
        else if (name == "card4") {
            return "임전혁";
        }
        else {
            return "기현빈";
        }
    }

    public void FixedPosition() {
        cardCnt--;
        if(cardCnt == 0) {
            //카드 분배 끝
        }
    }
}
