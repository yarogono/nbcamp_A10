using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject card;
    float timer;
    private bool isCardGenerated;

    Dictionary<GameObject, Vector3> cardList = new Dictionary<GameObject, Vector3>();

    [HideInInspector] public GameObject firstCard;
    [HideInInspector] public GameObject secondCard;
    // Start is called before the first frame update
    void Start()
    {
        isCardGenerated = false;
    }

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

        if (isCardGenerated == false)
        {
            StartCoroutine(GenerateCardMoveToTarget(0.03f));
        }
    }

    public void GenerateCard() {
        int[] cards = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        cards = cards.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
            
        for(int i = 0; i < 16; i++) {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("Cards").transform;
            
            float x = (i%4) * 1.4f - 2.1f;
            float y = (i/4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(0f, 0f, 0f);

            Vector3 target = new Vector3(x, y, 0);
            string cardName = "card" + cards[i].ToString();
            newCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
            cardList.Add(newCard, target);
        }
    }

    IEnumerator GenerateCardMoveToTarget(float waitSeconds)
    {
        foreach (KeyValuePair<GameObject, Vector3> card in cardList)
        {
            GameObject cardGameObject = card.Key;
            Vector3 cardVector3 = card.Value;
            cardGameObject.transform.position = Vector3.Lerp(cardGameObject.transform.position, cardVector3, 0.1f);
            yield return new WaitForSeconds(waitSeconds);
        }
        isCardGenerated = true;
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

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if(cardsLeft == 2) {
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
}
