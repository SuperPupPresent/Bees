using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{
    public TreeHive tree;
    int hiveLevel = 1;
    float productionWaitTime = 1.5f;
    int upgradeWaitTime = 2;
    int beeUpgradeAmount = 20;
    [SerializeField] Sprite[] sprites;
    //public Production(TreeHive t)
    //{
    //    tree = t;
    //    tree.spriteRenderer.sprite = sprites[0];
    //    tree.currentBeeCapacity = beeUpgradeAmount;
    //}

    // Start is called before the first frame update
    void Start()
    {
        tree.spriteRenderer.sprite = sprites[0];
        tree.currentBeeCapacity = beeUpgradeAmount;
        StartCoroutine(produceBees());
    }
    private void Update()
    {
        if(tree.treeInfo.treeState != TreeState.PRODUCTION)
        {
            Destroy(this);
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
        yield return new WaitForSeconds(upgradeWaitTime);
        tree.currentBeeCapacity += beeUpgradeAmount;
        tree.spriteRenderer.sprite = sprites[hiveLevel];
        hiveLevel++;
    }
}
