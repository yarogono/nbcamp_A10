using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text timeTxt;
    public GameObject endTxt;
    public GameObject card;
    float time;
    public static GameManager I;
    [HideInInspector] public GameObject firstCard;
    [HideInInspector] public GameObject secondCard;
    public AudioClip match;
    public AudioSource audioSource;
    

    private void Awake() {
        I = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        int[] rtans = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
            
        for(int i = 0; i < 16; i++) {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("Cards").transform;
            
            float x = (i%4) * 1.4f - 2.1f;
            float y = (i/4) * 1.4f-3.0f;
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if(time >= 30) {
            GameEnd();
        }
    }

    public void IsMatched() {
        string firstCardImage = firstCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;

        if(firstCardImage == secondCardImage) {
            audioSource.PlayOneShot(match);
            firstCard.GetComponent<Card>().DestroyCard();
            secondCard.GetComponent<Card>().DestroyCard();

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if(cardsLeft == 2) {
                GameEnd();
            }
        }
        else {
            firstCard.GetComponent<Card>().CloseCard();
            secondCard.GetComponent<Card>().CloseCard();  
        }

        firstCard = null;
        secondCard = null;
    
    }

    void GameEnd() {
        Time.timeScale = 0;
        endTxt.SetActive(true);
    }

    public void RetryGame() {
        SceneManager.LoadScene("MainScene");
    }
}
