using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] TreeHive hive;
    [SerializeField] TextMeshPro[] upgradeKeys;
    [SerializeField] TextMeshPro[] upgradeCosts;
    [SerializeField] SpriteRenderer[] upgradeSprites;
    // Start is called before the first frame update
    void Start()
    {
        upgradeCosts[1].text = "" + 20;
        upgradeCosts[2].text = "" + 30;
        upgradeCosts[3].text = "" + 5;
    }

    private void OnEnable()
    {
        upgradeCosts[0].text = "" + (float)hive.currentBeeCapacity * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
