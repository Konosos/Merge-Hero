using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherInformation : CharacterInformation, ICharacterInformation
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
        healthPoint=15*id;
        atk=15*id;

    }
}
