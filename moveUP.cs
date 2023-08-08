using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class moveUP : MonoBehaviour
{
    public float movespeed;     // �����̴� �ӵ�      
    public float alphaspeed;    // �����Ǵ� �ӵ�
    TextMeshPro text;        
    Color alpha;     
    public float destroytime;   // ������Ʈ�ı��ϴ� �ð�
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();  //text�� �ؽ�Ʈ�޽����� ���� ��������
        alpha = text.color;                  //GetComponent<Color>(); �̰Ŷ� ���Ŷ� ������  // algha = �ؽ�Ʈ�� �÷�
        Invoke("DestroyObject", destroytime); //destroytime�ð��ڿ� ������Ʈ �ı��Լ� ����
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, movespeed * Time.deltaTime, 0));  // �ؽ�Ʈ ��ü �����̴� �Լ�  
        alpha.a = Mathf.Lerp(alpha.a,0, Time.deltaTime*alphaspeed);          // ���� ���ϴ� �Լ�
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
