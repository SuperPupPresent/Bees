using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    public BeeColor team;
    public GameEvent snapToHive;
    public GameEvent sendBees;

    public GameObject selectedHive;
    public GameObject focusedHive;
    
    private bool isMoving;
    private bool isSnapped;
    private bool canSnap;
    private bool fSnapped;
    private bool focusMode;

    public string[] movementNames;
    public Color originalColor;
    public Color selectColor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isSnapped = false;
        canSnap = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Player1Input = new Vector2(Input.GetAxisRaw(movementNames[0]), Input.GetAxisRaw(movementNames[1])).normalized;
        rb.velocity = Player1Input * moveSpeed;

        //Debug.Log(canSnap);
        if(Player1Input == new Vector2(0,0) && canSnap && (!isSnapped || (focusMode && !fSnapped)))
        { //TODO: Use fsnap instead of normal is snapped
            canSnap = false;
            isMoving = false;
            StartCoroutine(startSnap(focusMode));
            
        }
        else if (Player1Input != new Vector2(0, 0))
        {
            isMoving = true;
           
            if (!focusMode)
            {
                isSnapped = false;
                selectedHive = null;
            }

            else
            {
                fSnapped = false;
                focusedHive = null;
            }
            
        }
        
        if (Input.GetButton(movementNames[5]) && isSnapped)
        {
            focusMode = true;
            gameObject.GetComponent<SpriteRenderer>().color = selectColor;
            if (fSnapped)
            {
                List<object> spawnInfo = new List<object>();
                spawnInfo.Add(selectedHive);
                spawnInfo.Add(focusedHive);
                if (Input.GetButtonDown(movementNames[2]))
                {
                    spawnInfo.Add(.10f);
                    sendBees.Raise(this, spawnInfo);
                }
                else if (Input.GetButtonDown(movementNames[3]))
                {
                    spawnInfo.Add(.25f);
                    sendBees.Raise(this, spawnInfo);
                }
                else if (Input.GetButtonDown(movementNames[4]))
                {
                    spawnInfo.Add(.50f);
                    sendBees.Raise(this, spawnInfo);
                }
            }
            
        }
        else
        {
            if (focusMode)
            {
                isSnapped = false;
            }
            focusMode = false;
            gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        }
    }

    IEnumerator startSnap(bool isFocus)
    {
        if (isFocus)
        {
            //Debug.Log("FOCUS");
            yield return (new WaitForSeconds(.25f));
            canSnap = true;
            if (!isMoving && !fSnapped)
            {
                //Debug.Log("FOCUS INSIDE inside");
                fSnapped = true;
                if (focusMode)
                {
                    snapToHive.Raise(this, BeeColor.Grey);
                }
            }
        }

        else
        {
            //Debug.Log("starting Snap");
            yield return (new WaitForSeconds(.25f));
            canSnap = true;
            if (!isMoving && !isSnapped)
            {
                //Debug.Log("inside");
                isSnapped = true;
                snapToHive.Raise(this, team);
            }
            // Debug.Log("finish snap");
        }
    }

    
}
