using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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
    [SerializeField] private Text idleTimeText;
    [SerializeField] private Text idleTimeRewardText;
    [SerializeField]
    private GameObject butPan;
    [SerializeField]
    private GameObject endMatchPan;
    [SerializeField]
    private GameObject idleRewardPan;

    private int idleTimeMoney;
    [SerializeField] private bool delayIdleTimeCheck = false;
    private void Start()
    {
        if(GameManager.instance.curGameData.idleTimeStart==0)
        {
            GameManager.instance.curGameData.idleTimeStart =(ulong) DateTime.Now.Ticks;
        }
    }

    private void Update()
    {
        if (idleRewardPan.activeSelf)
        {
            if (delayIdleTimeCheck)
                return;
            StartCoroutine(IdleTimeCoroutine());
 
        }

    }
    private IEnumerator IdleTimeCoroutine()
    {
        delayIdleTimeCheck = true;
        yield return new WaitForSeconds(0.5f);
        int idleTime_Second = (int)(((ulong)DateTime.Now.Ticks - GameManager.instance.curGameData.idleTimeStart) / (ulong)TimeSpan.TicksPerSecond);
        idleTimeMoney = idleTime_Second * 2;
        idleTimeRewardText.text = SimpleMoneyText(idleTimeMoney);
        string r = "";
        //HOURS
        r += ((int)idleTime_Second / 3600).ToString() + "h";
        idleTime_Second -= ((int)idleTime_Second / 3600) * 3600;
        //MINUTES
        r += ((int)idleTime_Second / 60).ToString("00") + "m ";
        //SECONDS
        r += (idleTime_Second % 60).ToString("00") + "s";

        idleTimeText.text = r;

        delayIdleTimeCheck = false;
    }
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
        warriorPrice.text = SimpleMoneyText(_price);
    }
    public void ShowArcherPrice(int _price)
    {
        archerPrice.text = SimpleMoneyText(_price);
    }
    public void ShowMoney(int _money)   
    {
        moneyText.text = SimpleMoneyText(_money);
    }
    private string SimpleMoneyText(int _money)
    {
        int thousand = _money / 10000;
        if (thousand > 0)
            return (thousand * 10).ToString() + "K";
        return _money.ToString();
    }

    public void RewardBut()
    {
        GameManager.instance.curGameData.money += 1000;

        SceneManager.LoadSceneAsync(0);
    }
    public void IdleRewardBut()
    {
        idleRewardPan.gameObject.SetActive(true);

    }
    public void RewardIdleMoneyBut()
    {
        GameManager.instance.curGameData.idleTimeStart = (ulong)DateTime.Now.Ticks;
        GameManager.instance.curGameData.money += idleTimeMoney;
        ShowMoney(GameManager.instance.curGameData.money);
    }
    public void CloseIdleRewardBut()
    {
        idleRewardPan.gameObject.SetActive(false);

        
    }
}
