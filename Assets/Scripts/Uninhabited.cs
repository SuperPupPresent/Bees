using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uninhabited : MonoBehaviour
{
    TreeHive tree;
    float productionWaitTime = 1.5f;
    Sprite uninhabitedSprite;

    // Start is called before the first frame update
    void Start()
    {
        tree.spriteRenderer.sprite = uninhabitedSprite;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
