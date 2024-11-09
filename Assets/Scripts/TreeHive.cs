using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreeHive : MonoBehaviour
{
    public TreeInfoSO treeInfo;
    [SerializeField] GameObject production; //prefab holding production script
    public SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshPro beeCountUI;
    public int currentBeeCount;
    public int currentBeeCapacity;

    private void Awake()
    {
        treeStateChanged();
    }
    void Start()
    {
        currentBeeCount = treeInfo.startingBeeCount;
        currentBeeCapacity = treeInfo.beeCapacity;
    }

    private void Update()
    {
        beeCountUI.text = "" + currentBeeCount;
    }
    public void treeStateChanged()
    {
        if (treeInfo.treeState == TreeState.UNINHABITED)
        {

        }
        else if (treeInfo.treeState == TreeState.PRODUCTION)
        {
            GameObject newProduction = Instantiate(production);
            newProduction.GetComponent<Production>().tree = this;
        }
        else if (treeInfo.treeState == TreeState.ENHANCER)
        {

        }
        else if (treeInfo.treeState == TreeState.WEAPON)
        {

        }
        else
        {
            return;
        }
    }
}
