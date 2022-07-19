using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]private GameObject iC;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Dame",4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Dame()
    {
        Debug.Log("aaa");
        iC.GetComponent<CharacterInformation>().TakeDamege(2);
    }
}
