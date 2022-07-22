using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delected : MonoBehaviour
{
    /*    
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
        }*/
}
