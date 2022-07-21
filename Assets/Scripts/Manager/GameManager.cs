using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject[,] heros = new GameObject[5, 7];
    [SerializeField] private GameObject warrior;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject chooseHero;

    [SerializeField]
    private GameObject meshCell;
    [SerializeField] private LayerMask character;
    [SerializeField]
    private int curXBoard;
    [SerializeField]
    private int curYBoard;

    public bool isStarted = false;
    public List<HerosInChapter> herosInChapters;
    public HerosInChapter myHeros;
    public GameData curGameData;
    
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.ShowMoney(curGameData.money);
        UIManager.instance.ShowArcherPrice(curGameData.priceArcher);
        UIManager.instance.ShowWarriorPrice(curGameData.priceWarrior);
        //CreateHero(true,true, 1, 0, 2);
        //CreateHero(true, true, 1, 1, 2);
        //CreateHero(true, false, 1, 3, 0);
        //CreateHero(true, false, 2, 4, 0);
        //CreateHero(false, true, 1, 0, 4);
        //CreateHero(false, true, 1, 1, 4);
        //CreateHero(false, false, 1, 0, 6);
        foreach (HeroData data in myHeros.heroDatas)
        {
            CreateHero(data.isPlayer, data.isWarrior, data.id, data.xBoard, data.yBoard);
        }
        foreach (HeroData data in herosInChapters[curGameData.curChapter].heroDatas)
        {
            CreateHero(data.isPlayer, data.isWarrior, data.id, data.xBoard, data.yBoard);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {

            return;
        }
            
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, character))
                return;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Vector3 boardPos = new Vector3(-8f + 4 * j, 0, -12f + 4 * i);
                    Vector3 dir = hit.point - boardPos;
                    if (dir.magnitude > 2f)
                        continue;
                    if (heros[j, i] != null)
                    {
                        chooseHero = heros[j, i];
                        chooseHero.GetComponent<CapsuleCollider>().isTrigger = true;
                    }
                }
            }
        }
        if (chooseHero == null)
            return;
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, character))
                return;           
            chooseHero.transform.position = hit.point + Vector3.up;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Vector3 boardPos = new Vector3(-8f + 4 * j, 0, -12f + 4 * i);
                    Vector3 dir = hit.point - boardPos;
                    if (dir.magnitude > 2f)
                        continue;
                    curXBoard = j;
                    curYBoard = i;
                    meshCell.transform.position = new Vector3(-8f + 4 * curXBoard, 0.52f, -12f + 4 * curYBoard);
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            meshCell.transform.position = new Vector3(0, -1f, 0);
            chooseHero.GetComponent<CapsuleCollider>().isTrigger = false;
            CharacterInformation charInfor = chooseHero.GetComponent<CharacterInformation>();
            if (heros[curXBoard, curYBoard] == null)
            {
                SetPosEmty(charInfor.xBoard, charInfor.yBoard);
                charInfor.SetPosition(curXBoard, curYBoard);
                SetHeroArray(chooseHero);
            }
            else
            {
                GameObject curHero = heros[curXBoard, curYBoard];
                CharacterInformation curHeroInfor = curHero.GetComponent<CharacterInformation>();
                if (curHero.layer==chooseHero.layer && curHero.tag==chooseHero.tag && curHeroInfor.id==charInfor.id && curHero!=chooseHero)
                {
                    curHeroInfor.id += 1;
                    curHeroInfor.SetInfor(curHeroInfor.id);
                    SetPosEmty(charInfor.xBoard, charInfor.yBoard);
                    Destroy(chooseHero);
                }
                else
                {
                    curHeroInfor.SetPosition(charInfor.xBoard, charInfor.yBoard);
                    charInfor.SetPosition(curXBoard, curYBoard);
                    SetHeroArray(chooseHero);
                    SetHeroArray(curHero);
                }              
            }
            chooseHero = null;
        }
       
    }
    void CreateHero(bool isPlayer,bool isWarrior, int _id, int _xBoard, int _yBoard)
    {
        GameObject hero;
        if(isWarrior)
        {
            hero = Instantiate(warrior, Vector3.zero, Quaternion.identity);
        }
        else
        {
            hero = Instantiate(archer, Vector3.zero, Quaternion.identity);
        }
        if(isPlayer)
        {
            hero.tag = "Player";
        }
        else
        {
            hero.tag = "Enemy";
        }
        CharacterInformation charInfor = hero.GetComponent<CharacterInformation>();
        charInfor.xBoard = _xBoard;
        charInfor.yBoard = _yBoard;
        charInfor.id = _id;
        charInfor.SetInfor(_id);
        heros[_xBoard, _yBoard] = hero;
    }
    private void SetHeroArray(GameObject obj)
    {
        CharacterInformation charInfor = obj.GetComponent<CharacterInformation>();
        heros[charInfor.xBoard, charInfor.yBoard] = obj;
    }
    private void SetPosEmty(int _xBoard, int _yBoard)
    {
        heros[_xBoard, _yBoard] = null;
    }
    public void CheckMatch()
    {
        if(heros.Length<=0)
        {
            Debug.Log("WTF");
            return;
        }
        bool playerLose = true;
        bool enemyLose = true;
        foreach(GameObject hero in heros )
        {
            if (hero == null)
                continue;
            if(hero.tag=="Player")
            {
                playerLose = false;
            }
            else
            {
                enemyLose = false;
            }
        }
        if(playerLose)
        {
            Lose();
        }
        if(enemyLose)
        {
            Win();
        }
    }
    public void DelayCheck()
    {
        Invoke("CheckMatch", 1f);
    }
    private void Win()
    {
        curGameData.curChapter++;
        UIManager.instance.ShowEndMatchPan("You Win");
        Time.timeScale = 0f;
    }
    private void Lose()
    {
        UIManager.instance.ShowEndMatchPan("You Lose");
        Time.timeScale = 0f;
    }
    public void StartBut()
    {
        SaveMyHerosData();
        isStarted = true;
        UIManager.instance.IsShowButPan(false);
        
    }

    private void SaveMyHerosData()
    {
        myHeros.heroDatas = new List<HeroData>();
        for(int j=0;j<3;j++)
        {
            for(int i=0;i<5;i++)
            {
                if (heros[i, j] == null)
                    continue;
                if (heros[i, j].tag != "Player")
                    continue;
                CharacterInformation charInfor = heros[i, j].GetComponent<CharacterInformation>();
                bool _isWarrior = heros[i, j].layer == 8;
                myHeros.heroDatas.Add(new HeroData {isPlayer=true, isWarrior=_isWarrior, id=charInfor.id, xBoard=charInfor.xBoard, yBoard=charInfor.yBoard });
            }
        }
    }

    public void SellWarriorBut()
    {
        if (curGameData.money < curGameData.priceWarrior)
            return;
        for (int j = 2; j >= 0; j--)
        {
            for (int i = 0; i < 5; i++)
            {
                if (heros[i, j] == null)
                {
                    CreateHero(true, true, 1, i, j);
                    curGameData.money -= curGameData.priceWarrior;
                    curGameData.priceWarrior += 200;
                    UIManager.instance.ShowMoney(curGameData.money);
                    UIManager.instance.ShowWarriorPrice(curGameData.priceWarrior);
                    goto go;
                }
            }
        }
    go:;
    }
    public void SellArcherBut()
    {
        if (curGameData.money < curGameData.priceArcher)
            return;
        for (int j = 0; j <= 2; j++)
        {
            for (int i = 4; i >= 0; i--)
            {
                if (heros[i, j] == null)
                {
                    CreateHero(true, false, 1, i, j);
                    curGameData.money -= curGameData.priceArcher;
                    curGameData.priceArcher += 200;
                    UIManager.instance.ShowMoney(curGameData.money);
                    UIManager.instance.ShowArcherPrice(curGameData.priceArcher);
                    goto go;
                }
            }
        }
    go:;
    }
}
