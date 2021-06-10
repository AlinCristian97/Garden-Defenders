using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefsController.SetMasterVolume(0.5f);
        Debug.Log(PlayerPrefsController.GetMasterVolume());
    }
}
