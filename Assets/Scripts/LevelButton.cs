using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Sprite winsprite;
    [SerializeField] private int level;
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Level")>=level)
        {
            GetComponent<Image>().sprite = winsprite;
            GetComponent<Button>().interactable = true;
            return;
        }   
        else if (PlayerPrefs.GetInt("Level") == level-1 || level==1)
        {
            GetComponent<Button>().interactable = true;
            return;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
