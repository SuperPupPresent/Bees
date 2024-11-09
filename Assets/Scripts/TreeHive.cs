using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TreeHive : MonoBehaviour
{
    public TreeInfoSO treeInfo;
    [SerializeField] GameObject productionPrefab; //prefab holding production script
    [SerializeField] GameObject enhancerPrefab;
    [SerializeField] GameObject weaponPrefab;
    public SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshPro beeCountUI;
    public int currentBeeCount;
    public int currentBeeCapacity;
    public BeeColor currentBeeColor; //Who owns the hive

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

    //Call this function when a bee attacks
    public void beeAttacks(PlayerInfoSO playerInfo)
    {
        currentBeeCount -=  playerInfo.level;
        if(currentBeeCount < 0)
        {
            currentBeeColor = playerInfo.beeColor;
            currentBeeCount *= -1;
            spriteRenderer.color = Color.red;
            //TODO change sprite color
        }
    }
    public void treeStateChanged()
    {
        //if (treeInfo.treeState == TreeState.UNINHABITED)
        //{
        //    GameObject newUninhabited = Instantiate(uninhabitedPrefab);
        //    newUninhabited.transform.parent = transform;
        //    newUninhabited.GetComponent<Uninhabited>().tree = this;
        //}
        if (treeInfo.treeState == TreeState.PRODUCTION)
        {
            GameObject newProduction = Instantiate(productionPrefab);
            newProduction.transform.parent = transform;
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
