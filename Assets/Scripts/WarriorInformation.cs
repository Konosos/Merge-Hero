using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorInformation : CharacterInformation
{

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void SetInfor(int _id)
    {
        base.SetInfor(_id);
        healthPoint=(int)(200* Mathf.Pow(2.3f, id - 1));
        atk=(int)(10* Mathf.Pow(2.3f, id - 1));
        healthBar.SetMaxHealth(healthPoint);
        SetModel(ModelManager.instance.warriorModels[id - 1]);
    }
}
