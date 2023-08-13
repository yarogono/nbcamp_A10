using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private float _timer;
    private int _cardsLeft;
    private bool _isCardGenerated;

    private Dictionary<GameObject, Vector3> _cardList = new Dictionary<GameObject, Vector3>();

    public GameObject card;

    [HideInInspector] 
    public GameObject firstCard;

    [HideInInspector] 
    public GameObject secondCard;

    void Start()
    {
        _isCardGenerated = false;
    }

    void Update()
    {
        if(firstCard != null) {
            _timer += Time.deltaTime;
            if(_timer >= 5) {
                firstCard.GetComponent<Card>().CloseCard();
                firstCard = null;
                _timer = 0;
            }
        }

        if (_isCardGenerated == false)
        {
            StartCoroutine(CardListMoveToDestination(0.03f));
        }
    }

    public void GenerateCard() {
        int[] cards = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        cards = cards.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        _cardsLeft = cards.Length;

        float cardTerm = card.transform.localScale.x + 0.1f;

        GameObject cardsGameObject = GameObject.Find("Cards");

        if (cardsGameObject == null)
            return;
        
        for(int i = 0; i < 16; i++) {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = cardsGameObject.transform;

            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            newCard.transform.position = new Vector3(0f, 0f, 0f);

            Vector3 destnation = new Vector3(x, y, 0);
            string cardName = $"card{cards[i]}";
            newCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);
            _cardList.Add(newCard, destnation);
        }
    }

    private IEnumerator CardListMoveToDestination(float waitSeconds)
    {
        foreach (KeyValuePair<GameObject, Vector3> card in _cardList)
        {
            GameObject cardGameObject = card.Key;
            Vector3 destination = card.Value;
            cardGameObject.transform.position = Vector3.Lerp(cardGameObject.transform.position, destination, 0.1f);
            yield return new WaitForSeconds(waitSeconds);
        }
        _isCardGenerated = true;
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
            _cardsLeft -= 2;

            if(_cardsLeft == 0) {
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
        _timer = 0;
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
