using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private InputController inputController;
    [SerializeField] private CameraFollower cameraFollower;
    [SerializeField] private TimeScaler timeScaler;
    [SerializeField] private LevelSpawner levelSpawner;
    [SerializeField] private CameraController cameraController;
    public static Action onGameStarted;
    public static bool isGameStarted;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (!PlayerPrefs.HasKey("Level"))
        PlayerPrefs.SetInt("Level", 0);
    }
    private void OnEnable()
    {
        onGameStarted += StartGame;
    }
    private void OnDisable()
    {
        onGameStarted -= StartGame;
    }
    private void StartGame()
    {
        PlayerController.instance.completedBlocks = 0;
        PlayerController.instance.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        isGameStarted = true;
        uiController.StartPanel.SetActive(false);
        inputController.IsGameStarted = true;
        timeScaler.IsGameStarted = true;
        levelSpawner.IsGameStarted = true;
        levelSpawner.enabled = true;
        cameraController.MoveCameraToGame();
    }
}
