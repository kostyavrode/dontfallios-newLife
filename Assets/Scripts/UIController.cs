using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public UniWebView uniWebView;
    public static UIController instance;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TMP_Text bestScoreBar;
    [SerializeField] private TMP_Text moneyBar;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text scoreBar;
    [SerializeField] public GameObject shopPanel;
    [SerializeField] private TMP_Text winMoney;
    [SerializeField] private TMP_Text loseMoney;
    public static Action onScoreChange;
    public int currentScore;
    private int moneyDelta;
    private bool isMoneyChecked;

    public GameObject StartPanel
    {
        get { return startPanel; }
        set { startPanel=value; }
    }

    private void Awake()
    {
        instance = this;
        PlayerController.onPlayerDeath += CheckBestScore;
        onScoreChange += ChangeScore;
        moneyBar.text = PlayerPrefs.GetInt("money",0).ToString();
    }
    public void UpdateMoney(int temp)
    {
        PlayerPrefs.SetInt("money", temp);
        PlayerPrefs.Save();
        moneyBar.text = temp.ToString();
        if (!isMoneyChecked)
        {
            isMoneyChecked = true;
            moneyDelta = temp-1;
        }
    }
    private void OnDisable()
    {
        PlayerController.onPlayerDeath -= CheckBestScore;
        onScoreChange -= ChangeScore;
    }
    private void ChangeScore()
    {
        currentScore+=1;
        scoreBar.text =currentScore.ToString();
        LevelSpawner.Instance.PlusSCore();
    }
    private void CheckBestScore()
    {
        if (winPanel.activeSelf)
        {
            return;
        }
        else if (deathPanel.activeSelf)
        {
            return;
        }
        moneyDelta = PlayerPrefs.GetInt("money") - moneyDelta;
        loseMoney.text = moneyDelta.ToString();
        deathPanel.SetActive(true);
        if (currentScore>PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            PlayerPrefs.Save();
        }
    }
    public void WinLevel()
    {
        int m = PlayerPrefs.GetInt("money");
        moneyDelta = m - moneyDelta;
        winMoney.text=moneyDelta.ToString();
        winPanel.SetActive(true);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        PlayerPrefs.Save();
    }
    public void OpenShop()
    {
        CameraController.instance.MoveCameraToShop();
        shopPanel.SetActive(true);
        startPanel.SetActive(false);
    }
    public void CloseShop()
    {
        CameraController.instance.MoveCameraToStart();
        shopPanel.SetActive(false);
        startPanel.SetActive(true);
    }
    public void Restart()
    {
        isMoneyChecked = false;
        winPanel.SetActive(false);
        deathPanel.SetActive(false);
        Time.timeScale = 1;
        TimeScaler.instance.Restart();
    }
}
