using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : MonoBehaviour
{
    float time = 0;
    public float size;
    public float upSizeTime;
    public float scalespeed;
    public float destroytime;
    Color alpha;
    public float alphaspeed;
    
    // Start is called before the first frame update
    void Start()
    {
        alpha = GetComponent<SpriteRenderer>().color; 
        Invoke("DestroyObject", destroytime);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaspeed);
        GetComponent<SpriteRenderer>().color = alpha;
        if (time <= upSizeTime)
        {
            transform.localScale = new Vector3(0.5f,0.5f,1) * (1+ size * time);
        }
        else if (time <= upSizeTime*2)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1) * (2 * size * upSizeTime + 1 - time * size);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }

}
