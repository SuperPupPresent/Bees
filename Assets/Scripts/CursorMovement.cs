using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    public BeeColor team;
    public GameEvent snapToHive;

    public GameObject selectedHive;
    public GameObject focusedHive;
    
    private bool isMoving;
    private bool isSnapped;
    private bool canSnap;
    private bool fcanSnap;
    private bool focusMode;
    
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
        Vector2 Player1Input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = Player1Input * moveSpeed;

        //Debug.Log(canSnap);
        if(Player1Input == new Vector2(0,0) && canSnap && (!isSnapped || (focusMode && !isSnapped)))
        { //TODO: Use fsnap instead of normal is snapped
            canSnap = false;
            //Debug.Log("not moving");
            isMoving = false;
            StartCoroutine(startSnap(focusMode));
            
        }
        else if (Player1Input != new Vector2(0, 0))
        {
            isMoving = true;
            isSnapped = false;
        }
        
        if (Input.GetButton("Focus") && isSnapped)
        {
            focusMode = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            
        }
        else
        {
            focusMode = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void SpawnBees()
    {

    }

    IEnumerator startSnap(bool isFocus)
    {
        if (isFocus)
        {
            Debug.Log("FOCUS");
            yield return (new WaitForSeconds(.5f));
            canSnap = true;
            if (!isMoving && !isSnapped)
            {
                Debug.Log("inside");
                isSnapped = true;
                snapToHive.Raise(this, BeeColor.Grey);
            }
        }

        else
        {
            //Debug.Log("starting Snap");
            yield return (new WaitForSeconds(.5f));
            canSnap = true;
            if (!isMoving && !isSnapped)
            {
                Debug.Log("inside");
                isSnapped = true;
                snapToHive.Raise(this, team);
            }
            // Debug.Log("finish snap");
        }
    }



    
}
