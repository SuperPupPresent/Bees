using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bee : MonoBehaviour
{
    public Transform targetPosition;
    public Collider2D targetCollider;
    public BeeColor team;
    public Rigidbody rb;
    public TreeHive targetScript;
    public GameEvent updateLists;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == targetCollider)
        {
            Debug.Log(targetScript.currentBeeColor);
            if (targetScript.currentBeeColor == team)
            {
                targetScript.currentBeeCount++;
            }
            else
            {
                targetScript.currentBeeCount--;
                if(targetScript.currentBeeCount < 0)
                {
                    targetScript.currentBeeCount *= -1;
                    targetScript.currentBeeColor = team;
                    targetScript.changeTreeSprite(targetScript.currentHiveLevel);
                    updateLists.Raise(targetScript, team);
                }
            }
            Destroy(gameObject);
        }
    }

}
