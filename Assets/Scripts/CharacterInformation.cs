using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    [SerializeField]protected int healthPoint;
    [SerializeField]protected int atk;
    [SerializeField]protected int xBoard;
    [SerializeField]protected int yBoard;
    [SerializeField]protected int id;

    public bool isDeath=false;

    public virtual void TakeDamege(int dame)
    {
        if(isDeath)
            return;
        healthPoint -=dame;
        //healthBar.SetHealth(healthPoint);
        //Instantiate(bloodVFX,deathVFXSpawnPos.position,Quaternion.identity);
        //SpawnDame(dame);
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

    }
}
