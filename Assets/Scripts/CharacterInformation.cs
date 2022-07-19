using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    public int healthPoint;
    public int atk;

    [SerializeField]
    protected HealthBar healthBar;
    public int xBoard;
    public int yBoard;
    public int id;

    public bool isDeath=false;

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
        }
    }
    public virtual void SetPosition(int _xBoard, int _yBoard)
    {
        xBoard = _xBoard;
        yBoard = _yBoard;
        transform.position = new Vector3(-8 + 4 * xBoard, 1.5f, -12 + 4 * yBoard);
    }
    
}
