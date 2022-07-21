using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] private Text warriorPrice;
    [SerializeField] private Text archerPrice;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text winLoseText;
    [SerializeField]
    private GameObject butPan;
    [SerializeField]
    private GameObject endMatchPan;

    public void IsShowButPan(bool _isShow)
    {
        butPan.gameObject.SetActive(_isShow);
    }
    public void ShowEndMatchPan(string _text)
    {
        endMatchPan.gameObject.SetActive(true);
        winLoseText.text = _text;
    }

    public void ShowWarriorPrice(int _price)
    {
        warriorPrice.text = _price.ToString();
    }
    public void ShowArcherPrice(int _price)
    {
        archerPrice.text = _price.ToString();
    }
    public void ShowMoney(int _money)
    {
        moneyText.text = _money.ToString();
    }

    public void RewardBut()
    {
        GameManager.instance.curGameData.money += 1000;
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
    
}
