using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewGameData",menuName ="GameData")]
public class GameData : ScriptableObject
{
    public int curChapter;
    public int money;
    public int priceWarrior;
    public int priceArcher;
    public ulong idleTimeStart;
}
