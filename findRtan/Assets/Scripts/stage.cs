using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage : MonoBehaviour
{

    public bool isOpen = false;


    // Start is called before the first frame update
    private void Start()
    {
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

        if (level == 1)
        {

            SceneManager.LoadScene("MainScene");
            //SceneManager.LoadScene("stage1");
            Debug.Log("stage1");

        }
        else if (level == 2)
        {
            SceneManager.LoadScene("stage2");
            Debug.Log("stage2");


        }
        else if (level == 3)
        {
            //SceneManager.LoadScene("stage3");

        }
    }


}
