using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using TMPro;

public class timeMinus : MonoBehaviour
{
    public float movespeed;          
    public float alphaspeed;    
    TextMeshPro text;
    Color alpha;
    public float destroytime;   
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();  
        alpha = text.color;                  
        Invoke("DestroyObject", destroytime); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, movespeed * Time.deltaTime, 0)); 
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaspeed);          
        text.color = alpha;
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public void InstantiateObject()
    {
        Instantiate(gameObject);
    }
}
