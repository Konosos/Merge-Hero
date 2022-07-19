using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorInformation : CharacterInformation, ICharacterInformation
{
    
    // Start is called before the first frame update
    void Start()
    {
        SetInfor(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetInfor(int _id)
    {
        id=_id;
        healthPoint=20*id;
        atk=10*id;

    }
}
