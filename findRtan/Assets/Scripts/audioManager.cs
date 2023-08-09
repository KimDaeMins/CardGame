using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class audioManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider audioSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioControl()
    {
        float sound = audioSlider.value;
        if(sound == -40f)
        {
            masterMixer.SetFloat("BGM", -80);
        } else
        {
            masterMixer.SetFloat("BGM", sound);
        }
    }
}
