using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{
    public TreeHive tree;
    //int hiveLevel;
    float productionWaitTime = 1.5f;
    int upgradeWaitTime = 2;
    int beeUpgradeAmount = 20;
    [SerializeField] Sprite[] sprites;
    bool canUpgrade;
    public string upgradeButton;
    //public Production(TreeHive t)
    //{
    //    tree = t;
    //    tree.spriteRenderer.sprite = sprites[0];
    //    tree.currentBeeCapacity = beeUpgradeAmount;
    //}

    // Start is called before the first frame update
    void Start()
    {
        updateProductionTime();
        canUpgrade = true;
        //tree.spriteRenderer.sprite = sprites[0];
        //hiveLevel = tree.treeInfo.hiveLevel;
        beeUpgradeAmount *= tree.currentHiveLevel;
        //tree.currentBeeCapacity = beeUpgradeAmount;
        StartCoroutine(produceBees());
        //StartCoroutine(upgradeHive());
    }
    private void Update()
    {
        if(tree.treeInfo.treeState != TreeState.PRODUCTION)
        {
            Destroy(this);
        }
        if(tree.currentBeeCount >= tree.currentBeeCapacity * 0.5f && tree.currentHiveLevel < 4)
        {
            canUpgrade = true;
        }
        else
        {
            canUpgrade = false;
        }
        if (canUpgrade && tree.canUpgrade && Input.GetButtonDown(upgradeButton))
        {
            StartCoroutine(upgradeHive());
            Debug.Log("Upgrade Hive");
        }
        if (tree.currentBeeColor == BeeColor.Yellow)
        {
            upgradeButton = "SpawnSmall";
        }
        else if (tree.currentBeeColor == BeeColor.Orange)
        {
            upgradeButton = "SpawnSmall2";
        }
    }

    IEnumerator produceBees()
    {
        yield return new WaitForSeconds(productionWaitTime);
        if(tree.currentBeeCount < tree.currentBeeCapacity)
        {
            tree.currentBeeCount++;
        }
        StartCoroutine(produceBees());
    }

    public IEnumerator upgradeHive()
    {
        tree.currentBeeCount -= (int)(tree.currentBeeCapacity * 0.5f);
        canUpgrade = false;
        yield return new WaitForSeconds(upgradeWaitTime);
        updateProductionTime();
        tree.currentBeeCapacity += beeUpgradeAmount;
        tree.currentHiveLevel++;
        tree.changeTreeSprite(tree.currentHiveLevel);
        canUpgrade = true;
    }

    void updateProductionTime()
    {
        productionWaitTime = 1.5f - (float)(tree.currentHiveLevel - 1) * 0.33f;
    }
}
