using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    public static TimeScaler instance;
    private bool isGameStarted;
    public bool IsGameStarted
    {
        get { return isGameStarted; }
        set { isGameStarted = value; }
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void FixedUpdate()
    {
        if (isGameStarted)
            if (Time.timeScale<3f)
        Time.timeScale += 0.001f;
    }
    public void Restart()
    {
        isGameStarted = false;
        Time.timeScale = 1;
    }
}
