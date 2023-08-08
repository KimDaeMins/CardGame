using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class stage : MonoBehaviour
{

    int i;
    public Text stageNum;
    

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < gameManager.I.GetComponent<gameManager>().stageLevel; i++)
        {
            if(stageNum.Equals("stage" + i))
            {
                
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(true)
        {
            GameObject stage = gameManager.I.GetComponent<gameManager>().stages[gameManager.I.GetComponent<gameManager>().stageLevel];
            stage.SetActive(false);
        }
    }


}
