using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class teamName : MonoBehaviour
{
    public float scalespeed;
    public TextMeshPro text;
    public float time;
    public float movespeed;
    public float alphaspeed;
    public float destroytime;
    Color alpha;
    // Start is called before the first frame update
    void Start()
    {
       
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = gameManager.I. firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name; ;
        Invoke("DestroyObject", destroytime);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
       transform.localScale = new Vector3(1.0f + scalespeed*time, 1.0f +  scalespeed*time,1);
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
