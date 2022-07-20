using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    
    public void ClickStartBut()
    {
        OnClicked();
    }
}
