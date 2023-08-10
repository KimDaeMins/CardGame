using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeLimit : MonoBehaviour
{
    public float timespeed;
    //public bool IsTimelimit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.I.IsTimelimit)
        {
            transform.localScale -= new Vector3(Time.deltaTime / 5.0f, 0.0f, 0.0f); ;
           
        }
    }
    public void Reset()
    {
       // float x = transform.localScale.x;
      //  if (x <= 0)
      //  {
           // x = 1;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //}
    
    }
}
