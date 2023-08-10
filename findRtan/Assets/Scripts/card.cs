using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class card : MonoBehaviour
{
    public Animator anim;
    public AudioClip flip;
    public AudioSource audioSource;
    public Vector3 myDestination;
    public int nametype; // 카드번호
    Vector3 middlePoint;
    float spinTime = 0.0f;
    public float rotationSpeed;
    public float maxSpinTime;
    public bool isMainCard = false;
    public bool isSetting { get; private set; } = false;
    public bool isMiddleSetting { get; private set; } = false;
    public bool SpinStart { get; set; } = false;
    public bool Setting { get; set; } = false;
    public int mode { get; private set; }// 0은 뒷면 1은 뒤 -> 앞 2는 앞면 3은 앞 -> 뒤
    public float openTime { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mode = 0;
        maxSpinTime = 1.5f;
    }
    // Update is called once per frame
    void Update()
    {
        InitCardSetting();
        if (mode == 3)
        {
            transform.Rotate(0.0f, 2.8f , 0.0f);
            if (transform.rotation.eulerAngles.y < 10.0f)
            {
                mode = 0;
                transform.rotation = Quaternion.identity;
            }
            if (transform.rotation.eulerAngles.y > 270.0f)
                transform.Find("front").gameObject.SetActive(false);
        }
        else if (mode == 1)
        {
            transform.Rotate(0.0f, 2.8f, 0.0f);
            if (transform.rotation.eulerAngles.y > 180.0f)
            {
                mode = 2;
                openTime = 0.0f;
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            if (transform.rotation.eulerAngles.y > 90.0f)
                transform.Find("front").gameObject.SetActive(true);
        }
        else if (mode == 2)
        {
            openTime += Time.deltaTime;
            if (openTime > 5.0f)
                closeCardInvoke();
        }
    }
    public void OpenCard()
    {
        if (!gameManager.I.FilpInfoSetting(gameObject))
            return;
        mode = 1;
        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);
        //transform.Find("front").gameObject.SetActive(true);
        //transform.Find("back").gameObject.SetActive(false);
        //if (gameManager.I.firstCard == null)
        //{
        //    gameManager.I.firstCard = gameObject;
        //}
        //else
        //{
        //    gameManager.I.secondCard = gameObject;
        //    gameManager.I.isMatched()
    }
    void InitCardSetting()
    {
        middlePoint = gameManager.I.middlePoint;
        if (isSetting == false)
        {
            if (!isMiddleSetting)
            {
                transform.position = Vector3.MoveTowards(transform.position, middlePoint, 0.2f);
                if ((middlePoint == transform.position))
                {
                    isMiddleSetting = true;
                    rotationSpeed += Random.Range(-180.0f, 180.0f);
                    gameManager.I.PlayPookSound();
                }
            }
            if (SpinStart)
            {
                spinTime += Time.deltaTime;
                if (!isMainCard)
                    transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
                if (spinTime > maxSpinTime)
                {
                    transform.rotation = Quaternion.identity;
                    SpinStart = false;
                }
            }
            if (Setting)
            {
                transform.position = Vector3.MoveTowards(transform.position, myDestination, 0.2f);
                if (transform.position == myDestination)
                {
                    anim.SetBool("isSetting", true);
                    isSetting = true;
                }
            }
        }
    }
    public void destroyCard()
    {
        Vector3 pos = new Vector3(Random.Range(-2.5f , 2.5f) , Random.Range(-5.0f , 5.0f) , 0.0f);
        gameManager.Resource.Instantiate("CFX_Firework_Trails_Gravity" , pos);
        Invoke("destroyCardInvoke", 0.3f);
    }
    public void destroyCardInvoke()
    {
        Destroy(gameObject);
        gameManager.I.CardReset(gameObject);
    }
    public void closeCard()
    {
        Invoke("closeCardInvoke", 0.3f);
    }
    public void closeCardInvoke()
    {
        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", false);
        //transform.Find("back").gameObject.SetActive(true);
        //transform.Find("front").gameObject.SetActive(false);
        mode = 3;
        gameManager.I.CardReset(gameObject);
    }
}















