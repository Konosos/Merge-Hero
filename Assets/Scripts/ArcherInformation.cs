using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherInformation : CharacterInformation
{
    // Start is called before the first frame update
    void Start()
    {
        //SetInfor(1);
        healthBar.SetMaxHealth(healthPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SetInfor(int _id)
    {
        base.SetInfor(_id);
        healthPoint = 150 * id;
        atk = 15 * id;

    }
}
