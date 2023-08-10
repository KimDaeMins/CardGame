using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;  //중요 - 배열 섞는 기능 사용 목적
using Unity.VisualScripting;

public class gameManager : MonoBehaviour
{
    public static gameManager I;
    public Text timeTxt;
    float time;
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endTxt;
    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return I._resource; } }
    public AudioClip flip;
    public AudioClip setting;
    public AudioSource audioSource;
    public int stageLevel = 1;
    public Text scoreTxt;
    public Text countTxt;
    public float timeLimit = 5f;
    public Text timeLimitTxt;
    public Text bestScoreTxt;
    public float score; //
    public int count; //카드를 클릭하면 ++
    public float bestScore;
    public Animator anim;
    public GameObject clearTxt;
    public GameObject endPanel;
    public GameObject timeMinus;
    public GameObject teamName;
    public GameObject fail;
    public timeLimit timeLimitbar;
    public bool IsTimelimit = false;
    public bool allMiddleSetting = false;
    List<card> cards = new List<card>();
    int index = 0;
    public float waitTime = 0.5f;
    int settingLevel = 4;
    public Vector3 middlePoint;
    float angleGap;
    List<float> angles = new List<float>();
    bool mainCardSetting = false;
    int cardsCount = 0;
    bool checking = false;
    float[] stageInfo;
    bool isClear = false;
    List<string>[] playerSprites = {new List<string>(), new List<string>() , new List<string>() };
    List<string> names = new List<string>();
    void Awake()
    {
        I = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelSetting();
    }
    // Update is called once per frame
    void Update()
    {
        if (settingLevel == 0)
        {
            if (!checking && secondCard != null && secondCard.GetComponent<card>().mode == 2)
            {
                checking = true;
                isMatched();
            }
            if (secondCard == null)
                checking = false;
            if(!isClear)
                time -= Time.deltaTime;

            timeTxt.text = time.ToString("N2");
            if (time < 0.0f)
            {
                time = 0.0f;
                Invoke("GameEnd", 0f);
            }
            if (time < 10.0f)
            {
                Invoke("TimeOut" , 0f);
            }
        }
        else if (settingLevel == 1)
        {
            if (cards[0].isSetting)
            {
                waitTime -= Time.deltaTime;
                if (waitTime < 0.0f)
                    settingLevel = 0;
            }
        }
        else if (settingLevel == 2)
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0)
            {
                audioSource.PlayOneShot(setting);
                cards[index].Setting = true;
                index++;
                waitTime = 0.1f;
                if (cards.Count == index)
                {
                    settingLevel = 1;
                    cards.Reverse();
                    waitTime = 0.3f;
                }
            }
        }
        else if (settingLevel == 3)
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0)
            {
                foreach (card a in cards)
                {
                    a.SpinStart = true;
                }
                waitTime = cards[0].maxSpinTime + 0.5f;
                settingLevel = 2;
            }
        }
        else if (settingLevel == 4)
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0)
            {
                GameObject go = Resource.Instantiate("card");
                go.transform.parent = GameObject.Find("Cards").transform;
                cards.Add(go.GetComponent<card>());
                //public float[] cards1 = { 3 , 1.7f , 1.7f , 2.6f , 1.2f , 6 };
                float x = (cardsCount % (int)stageInfo[0]) * stageInfo[1] - stageInfo[2];
                float y = (cardsCount / (int)stageInfo[0]) * stageInfo[3] - stageInfo[4];
                go.GetComponent<card>().myDestination = new Vector3(x, y, 0);
                go.transform.position = Quaternion.AngleAxis(angles[cardsCount], Vector3.forward) * new Vector3(10.0f, 0.0f, 0.0f);
                if (!mainCardSetting)
                {
                    mainCardSetting = true;
                    go.GetComponent<card>().isMainCard = true;
                    go.transform.Find("back").GetComponent<SpriteRenderer>().sortingOrder = 3;
                    go.transform.Find("back").transform.Find("Canvas").GetComponent<Canvas>().sortingOrder = 3;
                }
                string rTanName = names[cardsCount];
                go.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resource.Load<Sprite>($"Images/FindRTan/{rTanName}");
                ++cardsCount;
                waitTime = 0.1f;
                if (cardsCount == (int)stageInfo[5])
                {
                    waitTime = 0.8f;
                    settingLevel = 3;
                }
            }
        }

        if(firstCard != null && secondCard == null)
        {
            timeLimit = Mathf.Max(0.0f , 5.0f - firstCard.GetComponent<card>().openTime);
            timeLimitTxt.transform.gameObject.SetActive(true);
            timeLimitTxt.text = timeLimit.ToString("N2");
            IsTimelimit = true;
        }
        else
        {
            timeLimitTxt.transform.gameObject.SetActive(false);
            timeLimitbar.Reset();
            IsTimelimit = false;
            
        }

    }
    void InputName(int level)
    {
        playerSprites[0].Add("강의듣는 민열");
        playerSprites[1].Add("카트에 있는 재훈");
        playerSprites[2].Add("자습 대민");
        playerSprites[0].Add("사진찍는 민열");
        playerSprites[1].Add("술취한 재훈");
        playerSprites[2].Add("나무늘보 대민");

        for (int i = 0 ; i < level ; ++i)
        {
            for (int j = 0 ; j < 3 ; ++j)
            {
                int index = Random.Range(0 , playerSprites[j].Count);
                names.Add(playerSprites[j][index]);
                names.Add(playerSprites[j][index]);
                playerSprites[j].RemoveAt(index);
            } 
        }
    }
    void LevelSetting()
    {
        Time.timeScale = 1.0f;
        anim = timeTxt.GetComponent<Animator>();
        if (stageLevel == 1)
        {
            time = 30f;
            stageInfo = new float[] { 3 , 1.7f , 1.7f , 2.6f , 1.2f , 6 };
        }
        else if (stageLevel == 2)
        {
            time = 45f;
            stageInfo = new float[] { 3 , 1.7f , 1.7f , 2.0f , 3.0f , 12 };
        }
        InputName(stageLevel);
        timeTxt.text = time.ToString("N2");
        angleGap = 360.0f / stageInfo[5];

        for (int i = 0 ; i < stageInfo[5] ; i += 2)
        {
            angles.Add(angleGap * i);
            angles.Add(angleGap * i + angleGap);
        }
        for (int i = 0 ; i < names.Count ; ++i)
        {
            int j = Random.Range(i , names.Count);
            string temp = names[i];
            names[i] = names[j];
            names[j] = temp;

            int k = Random.Range(i , names.Count);
            float temp2 = angles[i];
            angles[i] = angles[k];
            angles[k] = temp2;
        }
    }
    public void PlayPookSound()
    {
        audioSource.PlayOneShot(setting);
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
            Instantiate(teamName);
            if (cardsLeft == 2)
            {
                Invoke("GameClear", 0.33f);
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
            time -= 1;
            Instantiate(timeMinus);
            Instantiate(fail);
        }
        count++;
    }
    public bool FilpInfoSetting(GameObject go)
    {
        if (firstCard == null)
            firstCard = go;
        else if (secondCard == null && firstCard != go)
        {
            secondCard = go;
        }
        else
            return false;
        return true;
    }
    public void CardReset(GameObject go)
    {
        if (firstCard == go)
            firstCard = null;
        else
            secondCard = null;
    }
    void GameEnd() // 게임오버(클리어는 따로)
    {
        Time.timeScale = 0f;
        score = 0;
        scoreTxt.text = score.ToString("N0");
        countTxt.text = count.ToString();
        string key = "bestScore" + stageLevel;
        bestScore = PlayerPrefs.GetFloat(key);
        bestScoreTxt.text = bestScore.ToString("N0");
        anim.SetBool("isTimeOut", true);
        endPanel.SetActive(true);
        endTxt.SetActive(true);
    }
    void GameClear()
    {
        isClear = true;
        float timeScore = Mathf.Max(50 , time * 2);
        float countScore = Mathf.Max(0.0f, 50.0f - ( count - stageInfo[5] / 2 ) * 2.5f);
        score = (int)( timeScore + countScore );
        //25초부터 1초당 -2점 
        //카드갯수 /2 번 안에 맞추면 50점 그이후론 시도횟수당 -2.5점
        anim.SetBool("isTimeOut" , true);
        anim.SetBool("isTimeOut" , true);
        scoreTxt.text = score.ToString("N0");
        countTxt.text = count.ToString();
        endPanel.SetActive(true);
        clearTxt.SetActive(true);
        string key = "bestScore" + stageLevel;

        if (PlayerPrefs.GetFloat(key) > 0.0f)
        {
            if (PlayerPrefs.GetFloat(key) < score)
            {
                PlayerPrefs.SetFloat(key , score);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(key , score);
        }
        bestScore = PlayerPrefs.GetFloat(key);
        bestScoreTxt.text = bestScore.ToString("N0");

        if (stageLevel == 1)
            PlayerPrefs.SetInt("Stage1" , 1);
        else if (stageLevel == 2)
            PlayerPrefs.SetInt("Stage2" , 1);

    }
    void TimeOut()
    {
        anim.SetBool("isTimeOut", true);
    }
    void Retry()
    {
    }
}