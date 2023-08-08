using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class moveUP : MonoBehaviour
{
    public float movespeed;     // 움직이는 속도      
    public float alphaspeed;    // 투명도되는 속도
    TextMeshPro text;        
    Color alpha;     
    public float destroytime;   // 오브젝트파괴하는 시간
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();  //text에 텍스트메쉬프로 정보 가져오기
        alpha = text.color;                  //GetComponent<Color>(); 이거랑 저거랑 차이점  // algha = 텍스트의 컬러
        Invoke("DestroyObject", destroytime); //destroytime시간뒤에 오브젝트 파괴함수 실행
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, movespeed * Time.deltaTime, 0));  // 텍스트 객체 움직이는 함수  
        alpha.a = Mathf.Lerp(alpha.a,0, Time.deltaTime*alphaspeed);          // 투명도 변하는 함수
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
