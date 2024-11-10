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
    [SerializeField] GameObject beePrefab;
    public SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshPro beeCountUI;
    public int currentBeeCount;
    public int currentBeeCapacity;
    public int currentHiveLevel;
    public TreeState currentTreeState;
    public BeeColor currentBeeColor; //Who owns the hive
    [SerializeField] Sprite[] sprites; //All possible tower sprites stored in a list

    public bool currentlySelected = false;

    public bool canUpgrade = false;

    string upgradeButton = "UpgradeMenu";

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
        if(currentBeeColor == BeeColor.Yellow)
        {
            upgradeButton = "UpgradeMenu";
        }
        else if (currentBeeColor == BeeColor.Orange)
        {
            upgradeButton = "UpgradeMenu2";
        }
        if (Input.GetButton(upgradeButton) && currentlySelected) // && playerIsHovering
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

    ////Call this function when a bee attacks
    //public void beeAttacks(PlayerInfoSO playerInfo)
    //{
    //    currentBeeCount -=  playerInfo.level;
    //    if(currentBeeCount < 0)
    //    {
    //        currentBeeColor = playerInfo.beeColor;
    //        currentBeeCount *= -1;
    //        spriteRenderer.color = Color.red;
    //        //TODO change sprite color
    //    }
    //}
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
            spriteRenderer.sprite = sprites[(int)currentBeeColor * 5 + hiveLevel - 1];
        }
    }

    public void sendBees(Component sender, object data)
    {
        if(data is List<object>)
        {
            List<object> senderInfo = (List<object>)data;
            GameObject hostHive = (GameObject)senderInfo[0];
            GameObject targetHive = (GameObject)senderInfo[1];
            float beeAmount = (float)senderInfo[2];

            if (hostHive == gameObject) {
                int beesSent = (int)(currentBeeCount * beeAmount + .5);
                if (beesSent > 0)
                {
                    currentBeeCount -= beesSent;
                    StartCoroutine(bufferSpawn(hostHive, targetHive, beesSent));
                }
            }
        }
    }

    public void setSelected(Component sender, object data)
    {
        if(sender is CursorMovement && (BeeColor)data == currentBeeColor)
        {
            CursorMovement cursorControler = (CursorMovement)sender;

            if(cursorControler.selectedHive == gameObject)
            {
                currentlySelected = true;
            }
            else
            {
                currentlySelected = false;
            }
        }
    }

    IEnumerator bufferSpawn(GameObject hostHive, GameObject targetHive, int beesSent)
    {
        for (int i = 0; i < beesSent; i++)
        {
            GameObject bee = Instantiate(beePrefab);
            bee.transform.position = new Vector3(hostHive.transform.position.x + Random.Range(-.75f, .75f), hostHive.transform.position.y + Random.Range(-.75f, .75f));
            bee.GetComponent<Bee>().targetPosition = targetHive.transform;
            bee.GetComponent<Bee>().targetCollider = targetHive.transform.Find("Hitbox").GetComponent<Collider2D>();
            bee.GetComponent<Bee>().team = currentBeeColor;
            bee.GetComponent<Bee>().targetScript = targetHive.GetComponent<TreeHive>();

            Vector2 direction = new Vector2(targetHive.transform.position.x - bee.transform.position.x, targetHive.transform.position.y - bee.transform.position.y);
            if(direction.x < 0)
            {
                bee.GetComponent<SpriteRenderer>().flipX = true;
            }
            float magnitude = Mathf.Sqrt(direction.magnitude);
            float speed = .5f;
            direction.x = speed * (direction.x / magnitude);
            direction.y = speed * (direction.y / magnitude);

            bee.GetComponent<Rigidbody2D>().velocity = direction;

            yield return (new WaitForSeconds(.1f));
        }
    }

}
