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
    [SerializeField] GameObject upgradeMenu;
    public SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshPro beeCountUI;
    public int currentBeeCount;
    public int currentBeeCapacity;
    public int currentHiveLevel;
    public TreeState currentTreeState;
    public BeeColor currentBeeColor; //Who owns the hive
    [SerializeField] Sprite[] sprites; //All possible tower sprites stored in a list

    public bool canUpgrade = false;

    private void Awake()
    {
        treeStateChanged();
    }
    void Start()
    {
        upgradeMenu.SetActive(false);
        currentHiveLevel = treeInfo.hiveLevel;
        currentBeeCount = treeInfo.startingBeeCount;
        currentBeeCapacity = treeInfo.beeCapacity;
        currentTreeState = treeInfo.treeState;
        changeTreeSprite(currentHiveLevel);
    }

    private void Update()
    {
        beeCountUI.text = "" + currentBeeCount;
        if (Input.GetButton("UpgradeMenu")) // && playerIsHovering
        {
            upgradeMenu.SetActive(true);
            canUpgrade = true;
        }
        else
        {
            upgradeMenu.SetActive(false);
            canUpgrade = false;
        }
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
        if (currentTreeState == TreeState.PRODUCTION)
        {
            GameObject newProduction = Instantiate(productionPrefab);
            newProduction.transform.parent = transform;
            newProduction.GetComponent<Production>().tree = this;
        }
        else if (currentTreeState == TreeState.ENHANCER)
        {

        }
        else if (currentTreeState == TreeState.WEAPON)
        {

        }
        else
        {
            return;
        }
    }

    public void changeTreeSprite(int hiveLevel)
    {
        if(currentTreeState == TreeState.PRODUCTION)
        {
            //Debug.Log((int)currentBeeColor + " Current bee color");
            spriteRenderer.sprite = sprites[(int)currentBeeColor * 4 + hiveLevel];
        }
    }
}
