using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;
    public AudioClip flip;
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenCard()
    {
        audioSource.PlayOneShot(flip);
        anim.SetBool("isOpen", true);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        if (gameManager.I.firstCard == null)
        {
            gameManager.I.firstCard = gameObject;
            gameManager.I.GetComponent<gameManager>().timeLimit = 5.0f;
            //gameManager.I.GetComponent<gameManager>().timeLimitTxt.color = new Color(53f, 54f, 53f, 255);
            gameManager.I.GetComponent<gameManager>().timeLimitTxt.gameObject.SetActive(true);

        }
        else
        {
            gameManager.I.secondCard = gameObject;
            gameManager.I.isMatched();
        }
    }

    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 0.3f);
        gameManager.I.GetComponent<gameManager>().timeLimit = 5.0f;
        //gameManager.I.GetComponent<gameManager>().timeLimitTxt.color = new Color(53f, 54f, 53f, 0);
        gameManager.I.GetComponent<gameManager>().timeLimitTxt.gameObject.SetActive(false);
    }

    public void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 0.3f);
        gameManager.I.GetComponent<gameManager>().timeLimit = 5.0f;
        //gameManager.I.GetComponent<gameManager>().timeLimitTxt.color = new Color32(53, 53, 53, 0);
        gameManager.I.GetComponent<gameManager>().timeLimitTxt.gameObject.SetActive(false);
    }

    public void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);

    }
}
