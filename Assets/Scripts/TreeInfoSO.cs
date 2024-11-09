using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TreeInfoSO : ScriptableObject
{
    public int beeCapacity;
    public int startingBeeCount;
    public int hiveLevel;
    public TreeState treeState;
    public BeeType startingBeeType;
}
