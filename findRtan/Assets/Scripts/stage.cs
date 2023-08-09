using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage : MonoBehaviour
{

    bool isOpen = false;
    public int myStage = 0;


    // Start is called before the first frame update
    private void Start()
    {
        if (myStage == 1)
        {
            isOpen = true;
        }
        else if (myStage == 2)
        {
            if (PlayerPrefs.GetInt("Stage1") == 1)
                isOpen = true;
        }
        else if(myStage == 3)
        {
            if (PlayerPrefs.GetInt("Stage2") == 1)
                isOpen = true;
        }
        if(myStage != 0)
        {
            transform.Find("levelTxt").GetComponent<Text>().text = "Level " + myStage.ToString();
        }
        if (isOpen)
        {
            transform.Find("lock").gameObject.SetActive(false);
            transform.Find("black").gameObject.SetActive(false);
            transform.GetComponent<Button>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterStage(int level)
    {
        if(level == 0)
        {
            SceneManager.LoadScene("StartScene");
        }
        else if (level == 1)
        {

            SceneManager.LoadScene("Stage1");
            //SceneManager.LoadScene("stage1");
            Debug.Log("stage1");

        }
        else if (level == 2)
        {
            SceneManager.LoadScene("Stage2");
            Debug.Log("stage2");


        }
        else if (level == 3)
        {
            //SceneManager.LoadScene("stage3");

        }
    }
    public void ResetBestScore(int maxLevel)
    {
        for (int i = 1; i <= maxLevel; i++)
        {
           PlayerPrefs.SetInt("bestScore" + i, 0);


        }

        if (myStage != 1)
        {
            
            isOpen = false;
            transform.Find("lock").gameObject.SetActive(true);
            transform.Find("black").gameObject.SetActive(true);
            transform.GetComponent<Button>().enabled = false;
        }
    }


}
