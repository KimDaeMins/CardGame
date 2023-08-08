using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;  //중요 - 배열 섞는 기능 사용 목적

public class gameManager : MonoBehaviour
{
    public static gameManager I;
    public Text timeTxt;
    float time;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endTxt;

    public AudioClip flip;
    public AudioSource audioSource;
    public int stageLevel = 1;
    public Text scoreTxt;
    public Text countTxt;
    public Text bestScoreTxt;
    public float score; // 
    public int count; //카드를 클릭하면 ++
    public float bestScore;
    public Animator anim;
    public GameObject clearTxt;
    public GameObject endPanel;


    public float[] cards1 = { 3, 1.7f, 1.7f, 2.6f, 1.2f, 6 };
    public float[] cards2 = { 3, 1.7f, 1.7f, 2.0f, 3.0f, 12 };

    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        time = 30f;
        anim = timeTxt.GetComponent<Animator>();

        if (stageLevel == 1) {
            int[] cards = { 0, 0, 1, 1, 2, 2 };
            cards = cards.OrderBy(ContextMenuItemAttribute => Random.Range(-1.0f, 1.0f)).ToArray();  //OrderBy(a, b) = 정렬하겠다. a를 b순서로         ToArray() = 리스트화 하겠다.;

            for (int i = 0; i < (stageLevel * 6); i++)
            {
                GameObject newCard = Instantiate(card);
                newCard.transform.parent = GameObject.Find("Cards").transform;
                float x = (i % 3) * 1.7f - 1.7f;
                float y = (i / 3) * 2.6f - 1.2f;
                newCard.transform.position = new Vector3(x, y, 0);

                string cardName = "rtan" + cards[i].ToString();
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);

            }
        }


        else if (stageLevel == 2)
        {

            int[] cards = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
            cards = cards.OrderBy(ContextMenuItemAttribute => Random.Range(-1.0f, 1.0f)).ToArray();

            for (int i = 0; i < 12; i++)
            {
                GameObject newCard = Instantiate(card);
                newCard.transform.parent = GameObject.Find("Cards").transform;
                float x = (i % 3) * 1.7f - 1.7f;
                float y = (i / 3) * 2.0f - 3.0f;
                newCard.transform.position = new Vector3(x, y, 0);

                string cardName = "rtan" + cards[i].ToString();
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(cardName);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");


        if (time < 0.0f)
        {
            time = 0.0f;
            Invoke("GameEnd", 0f);
        }

        if (time < 5.0f)
        {

            Invoke("GameClear", 0f);
        }
        
        if (time < 10.0f)
        {
            Invoke("TimeOut", 0f);

        }
        

    }

    public void isMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(flip);

            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if (cardsLeft == 2)
            {
                Invoke("GameClear", 0.1f);

            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();

        }

        firstCard = null;
        secondCard = null;
    }

    void GameEnd() // 게임오버(클리어는 따로)
    {
        Time.timeScale = 0f;
        score = 0;
        anim.SetBool("isTimeOut", true);

        endPanel.SetActive(true);
        endTxt.SetActive(true);


    }

    void GameClear()
    {
        Time.timeScale = 0f;

        score = (time * 2) + (40 - count);
        anim.SetBool("isTimeOut", true);


        if (PlayerPrefs.GetFloat("bestScore") > 0.0f)
        {
            if (PlayerPrefs.GetFloat("bestScore") < score)
            {
                PlayerPrefs.SetFloat("bestScore", score);
                
            }
        }
        else
        {
            PlayerPrefs.SetFloat("bestScore", score);

        }
        bestScore = PlayerPrefs.GetFloat("bestScore");
        anim.SetBool("isTimeOut", true);

        bestScoreTxt.text = bestScore.ToString("N0");
        scoreTxt.text = score.ToString("N0");
        countTxt.text = count.ToString();
        endPanel.SetActive(true);
        clearTxt.SetActive(true);
    }

    
    void TimeOut()
    {
        
        anim.SetBool("isTimeOut", true);
        
        
    }

    void Retry()
    {

        
    }
    


}
