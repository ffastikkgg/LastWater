using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_game : MonoBehaviour
{
    public void NewGame()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayerPrefs.DeleteAll();
    }

}
