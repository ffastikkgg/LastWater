using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetSfxLevel(float sfxLvl)
    {
        masterMixer.SetFloat("volSfx", sfxLvl);
    }

    public void SetMusicLevel(float musicLvl)
    {
        masterMixer.SetFloat("volMusic", musicLvl);
    }


}