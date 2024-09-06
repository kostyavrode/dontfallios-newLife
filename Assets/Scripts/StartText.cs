using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StartText : MonoBehaviour
{
    public GameObject onek;
    public void Start()
    {
        if (!PlayerPrefs.HasKey("Start"))
        {
            onek.SetActive(true);
            PlayerPrefs.SetString("Start", "dsf");
            PlayerPrefs.Save();
        }
    }
}
