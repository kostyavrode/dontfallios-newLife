using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerModelController : MonoBehaviour
{
    public static PlayerModelController instance;
    [SerializeField] private GameObject[] models;
    [SerializeField] private Transform playerObject;
    [SerializeField] private Vector3 scaleFactor = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private GameObject buyButton;
    private GameObject currentModel;
    private int index;
    private bool isModelBrought;

    public GameObject[] Models
    {
        get { return models; }
    }
    private void Awake()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("money"))
        {
            PlayerPrefs.SetInt("money", 0);
        }
        if (!PlayerPrefs.HasKey("skin0"))
        {
            PlayerPrefs.SetString("skin0", "true");
        }
        CheckBuy();
        index = PlayerPrefs.GetInt("Model");
        ShowNewModel();
    }
    public void Left()
    {
        index--;
        ShowNewModel();
    }
    public void Right()
    {
        index++;
        ShowNewModel();
    }
    private void ShowNewModel()
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }
        if (index>models.Length-1)
        {
            index = 0;
        }
        if (index<0)
        {
            index = models.Length - 1;
        }
        PlayerPrefs.SetInt("Model", index);
        SetModel(index);
        CheckBuy();
        PlayerPrefs.Save();
    }
    public void SetModel(int modelIndex)
    {
        GameObject newModel = Instantiate(models[index], playerObject.transform);
        //newModel.transform.localScale = scaleFactor;
        currentModel = newModel;
    }
    public void CheckBuy()
    {
        buyButton.gameObject.SetActive(true);
        if (PlayerPrefs.HasKey("skin" + index.ToString()))
        {
            buyButton.GetComponentInChildren<TMP_Text>().text = "0";
            isModelBrought = true;
        }
        else
        {
            isModelBrought = false;
            buyButton.GetComponentInChildren<TMP_Text>().text = ""+GetPrice();

        }
    }
    public void AddMoney()
    {
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 1);
        PlayerPrefs.Save();
        UpdateMoney();
    }
    public void Buy()
    {
        if (isModelBrought)
        {
            PlayerPrefs.SetInt("Model", index);
            PlayerPrefs.Save();
            //buyButton.gameObject.SetActive(false);
            UIController.instance.CloseShop();
        }
        else if (GetPrice() < PlayerPrefs.GetInt("money"))
        {
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - GetPrice());
            PlayerPrefs.SetString("skin" + index.ToString(), "true");
            PlayerPrefs.Save();
            CheckBuy();
            UpdateMoney();
        }
    }
    private int GetPrice()
    {
        switch (index)
            {
            case 1:
                return 30;
            case 2:
                return 50;
            case 3:
                return 60;
            case 4:
                return 70;
            default: 
                return 0;
        }
    }
    private void UpdateMoney()
    {
        UIController.instance.UpdateMoney(PlayerPrefs.GetInt("money"));
    }
}
