using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfoSO : ScriptableObject
{
    public BeeColor beeColor;
    public BeeType beeType;
    public int buildingCount;
    public int level;
}
