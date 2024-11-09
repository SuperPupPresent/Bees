using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{
    public TreeHive tree;
    int hiveLevel;
    float productionWaitTime = 1.5f;
    int upgradeWaitTime = 2;
    int beeUpgradeAmount = 20;
    [SerializeField] Sprite[] sprites;
    bool canUpgrade;
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
        tree.spriteRenderer.sprite = sprites[0];
        hiveLevel = tree.treeInfo.hiveLevel;
        beeUpgradeAmount *= hiveLevel;
        tree.currentBeeCapacity = beeUpgradeAmount;
        StartCoroutine(produceBees());
        //StartCoroutine(upgradeHive());
    }
    private void Update()
    {
        if(tree.treeInfo.treeState != TreeState.PRODUCTION)
        {
            Destroy(this);
        }
        if(tree.currentBeeCount >= tree.currentBeeCapacity * 0.5f)
        {
            canUpgrade = true;
        }
        else
        {
            canUpgrade = false;
        }
    }

    IEnumerator produceBees()
    {
        yield return new WaitForSeconds(productionWaitTime);
        if(tree.currentBeeCount < tree.currentBeeCapacity)
        {
            tree.currentBeeCount++;
        }
        else if(canUpgrade && hiveLevel < 4)
        {
            StartCoroutine(upgradeHive());
            Debug.Log("Upgrade Hive");
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
        tree.changeTreeSprite(hiveLevel);
        hiveLevel++;
        canUpgrade = true;
    }

    void updateProductionTime()
    {
        productionWaitTime = 1.5f - (float)(hiveLevel - 1) * 0.33f;
    }
}
