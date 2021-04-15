using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetAudio : MonoBehaviour
{
    public AudioMixer mixer;
    public string parameterName = "masterVol";

    protected float Parameter
    {
        get
        {
            float parameter;
            mixer.GetFloat(parameterName, out parameter);
            return parameter;
        }
        set
        {
            mixer.SetFloat(parameterName, value);
        }
    }
}
