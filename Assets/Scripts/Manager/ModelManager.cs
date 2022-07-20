using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{

    public static ModelManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject[] warriorModels;
    public GameObject[] archerModels;
}
