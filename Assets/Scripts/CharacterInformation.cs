using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    public int healthPoint;
    public int atk;

    [SerializeField]
    protected HealthBar healthBar;
    [SerializeReference] protected Transform modelParent;
    public int xBoard;
    public int yBoard;
    public int id;

    public bool isDeath=false;

    protected virtual void Start()
    {
        healthBar.SetMaxHealth(healthPoint);
    }

    public virtual void TakeDamege(int dame)
    {
        if(isDeath)
            return;
        healthPoint -=dame;
        healthBar.SetHealth(healthPoint);
        if(healthPoint<=0)
        {
            isDeath=true;
            Die();
        }
        else
        {
            //isHurting=true;
            //cur_TimeHurt=0f;
            //isHurt=true;
        }
    }
    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
    public virtual void SetInfor(int _id)
    {
        id = _id; 
        SetPosition(xBoard, yBoard);
        if(this.gameObject.tag=="Player")
        {
            healthBar.SetColor(false);
        }
        if (this.gameObject.tag == "Enemy")
        {
            healthBar.SetColor(true);
            this.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
    }
    public virtual GameObject  SetModel(GameObject _model)
    {
        foreach (Transform child in modelParent)
        {
            Destroy(child.gameObject);
        }
        GameObject curModel = Instantiate(_model as GameObject);
        curModel.transform.SetParent(modelParent);
        curModel.transform.localPosition = new Vector3(1, 1, 1);
        curModel.transform.position = transform.position;
        return curModel;
    }
    public virtual void SetPosition(int _xBoard, int _yBoard)
    {
        xBoard = _xBoard;
        yBoard = _yBoard;
        transform.position = new Vector3(-8 + 4 * xBoard, 1.5f, -12 + 4 * yBoard);
    }
    
}
