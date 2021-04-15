using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetSfxLevel(float sfxLvl)
    {
        masterMixer.SetFloat("volSFX", sfxLvl);
    }

    public void SetMusicLevel(float musicLvl)
    {
        masterMixer.SetFloat("volMusic", musicLvl);
    }

    public void Toggle_changed(bool newValue)
    {
        if (newValue == true)
        {
            masterMixer.SetFloat("volMaster", -80);
        }
        else 
        {
            masterMixer.SetFloat("volMaster", 0);
        }
    }
}