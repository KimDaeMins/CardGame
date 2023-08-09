using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        ResetPrefs(2);
        SceneManager.LoadScene("LobbyScene");
    }

    public void ResetPrefs(int maxLevel)
    {
        for(int i = 1; i <= maxLevel; i++)
        {
            PlayerPrefs.SetInt("Stage" + i , 0);
            PlayerPrefs.SetInt("bestScore" + i, 0);
        }
    }
}
