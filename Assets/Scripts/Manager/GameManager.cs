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
    public int curChapter = 0;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateHero(true,true, 1, 0, 2);
        CreateHero(true, true, 1, 1, 2);
        CreateHero(true, false, 1, 3, 0);
        //CreateHero(true, false, 2, 4, 0);
        //CreateHero(false, true, 1, 0, 4);
        //CreateHero(false, true, 1, 1, 4);
        //CreateHero(false, false, 1, 0, 6);
        foreach(HeroData data in herosInChapters[curChapter].heroDatas)
        {
            CreateHero(data.isPlayer, data.isWarrior, data.id, data.xBoard, data.yBoard);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
            return;
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
    public void StartBut()
    {
        isStarted = true;
        
    }
    public void SellWarriorBut()
    {
        for(int j=2;j>=0;j--)
        {
            for(int i=0;i<5;i++)
            {
                if(heros[i,j]==null)
                {
                    CreateHero(true, true, 1, i, j);
                    goto go;
                }
            }
        }
        go:;
    }
    public void SellArcherBut()
    {
        for (int j = 0; j <= 2; j++)
        {
            for (int i = 4; i >=0; i--)
            {
                if (heros[i, j] == null)
                {
                    CreateHero(true, false, 1, i, j);
                    goto go;
                }
            }
        }
        go:;
    }
}
