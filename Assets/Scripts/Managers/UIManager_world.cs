using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_world : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;

    public void OpenOptionsMenu(bool status)
    {
        optionsPanel.SetActive(status);
    }

}
