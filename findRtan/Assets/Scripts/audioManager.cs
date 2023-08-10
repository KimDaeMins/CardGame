using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class audioManager : MonoBehaviour
{
    public static audioManager instance;
    public AudioMixer masterMixer;
    public Slider audioSlider;
    public float sound;


    /*
     private void Awake()
     {
         backgroundMusic = GameObject.Find("audioManager");
         backMusic = backgroundMusic.GetComponent<AudioSource>();
         if(backMusic.isPlaying)
         {
             return;
         } else
         {
             backMusic.Play();
             DontDestroyOnLoad(backgroundMusic);
         }
     }
    */
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance!= null)
        {
            
            Destroy(gameObject);
            return;

        } 
            instance = this;
            DontDestroyOnLoad(gameObject);        



    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioControl()
    {
        sound = audioSlider.value;
        if(sound == -40f)
        {
            masterMixer.SetFloat("BGM", -80);
            masterMixer.SetFloat("SFX", -80);
        } else
        {
            masterMixer.SetFloat("BGM", sound);
            masterMixer.SetFloat("SFX", sound);
        }
    }
}
