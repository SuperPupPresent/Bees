using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class HiveManager : MonoBehaviour
{
    List<GameObject> neutralHives = new List<GameObject>();
    List<GameObject> pOneHives = new List<GameObject>();
    List<GameObject> pTwoHives = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.GetComponent<TreeHive>().currentBeeColor == BeeColor.Grey)
            {
                neutralHives.Add(transform.GetChild(i).gameObject);
            }
            else if(transform.GetChild(i).gameObject.GetComponent<TreeHive>().currentBeeColor == BeeColor.Yellow)
            {
                pOneHives.Add(transform.GetChild(i).gameObject);
            }
            else
            {
                pTwoHives.Add(transform.GetChild(i).gameObject);
            }
        }
        Debug.Log(neutralHives.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void snapCursorToHive(Component sender, object data)
    {
        if(sender is CursorMovement && data is BeeColor)
        {
            BeeColor teamInfo = (BeeColor)data;
            Transform Cursor = sender.gameObject.transform;
            //Debug.Log(teamInfo);

            if (teamInfo == BeeColor.Yellow)
            {
                GameObject closestHive = null;
                float closestDistance = -1f;

                for(int i = 0; i < pOneHives.Count; i++)
                {   
                    float currentDistance = getDistance(Cursor.position, pOneHives[i].transform.position);
                  //  Debug.Log(currentDistance);
                    if (closestDistance > currentDistance || closestHive == null)
                    {
                        closestHive = pOneHives[i];
                        closestDistance = currentDistance;
                    }
                }

                if(closestHive != null)
                {
                   // Debug.Log("Snapped!");
                    Cursor.position = closestHive.transform.position;
                    Cursor.gameObject.GetComponent<CursorMovement>().selectedHive = closestHive;
                }
            }

            else if(teamInfo == BeeColor.Orange)
            {
                GameObject closestHive = null;
                float closestDistance = -1f;

                for (int i = 0; i < pTwoHives.Count; i++)
                {
                    float currentDistance = getDistance(Cursor.position, pTwoHives[i].transform.position);
                    //  Debug.Log(currentDistance);
                    if (closestDistance > currentDistance || closestHive == null)
                    {
                        closestHive = pTwoHives[i];
                        closestDistance = currentDistance;
                    }
                }

                if (closestHive != null)
                {
                    // Debug.Log("Snapped!");
                    Cursor.position = closestHive.transform.position;
                    Cursor.gameObject.GetComponent<CursorMovement>().selectedHive = closestHive;
                }
            }

            else
            {
                GameObject closestHive = null;
                GameObject currentHive = Cursor.gameObject.GetComponent<CursorMovement>().selectedHive;
                float closestDistance = -1f;

                for (int i = 0; i < pTwoHives.Count; i++)
                {
                    float currentDistance = getDistance(Cursor.position, pTwoHives[i].transform.position);
                    //  Debug.Log(currentDistance);
                    if ((closestDistance > currentDistance || closestHive == null) && currentHive != pTwoHives[i])
                    {
                        closestHive = pTwoHives[i];
                        closestDistance = currentDistance;
                    }
                }

                for (int i = 0; i < pOneHives.Count; i++)
                {
                    float currentDistance = getDistance(Cursor.position, pOneHives[i].transform.position);
                    //  Debug.Log(currentDistance);
                    if ((closestDistance > currentDistance || closestHive == null) && currentHive != pOneHives[i])
                    {
                        closestHive = pOneHives[i];
                        closestDistance = currentDistance;
                    }
                }

                for (int i = 0; i < neutralHives.Count; i++)
                {
                    float currentDistance = getDistance(Cursor.position, neutralHives[i].transform.position);
                    //  Debug.Log(currentDistance);
                    if (closestDistance > currentDistance || closestHive == null)
                    {
                        closestHive = neutralHives[i];
                        closestDistance = currentDistance;
                    }
                }

                if (closestHive != null)
                {
                    // Debug.Log("Snapped!");
                    Cursor.position = closestHive.transform.position;
                    Cursor.gameObject.GetComponent<CursorMovement>().focusedHive = closestHive;
                }
            }
            
        }
    }

    public void UpdateLists(Component sender, object data)
    {
        if(sender is TreeHive && data is BeeColor)
        {
            GameObject targetHive = sender.gameObject;
            BeeColor team = (BeeColor)data;
            neutralHives.Remove(targetHive);
            pOneHives.Remove(targetHive);
            pTwoHives.Remove(targetHive);

            if(team == BeeColor.Yellow)
            {
                pOneHives.Add(targetHive);
            }
            
            else if(team == BeeColor.Orange) 
            {
                pTwoHives.Add(targetHive);
            }
        }
    }

    private float getDistance(Vector3 a, Vector3 b)
    {
        float xDistance = Mathf.Pow((a.x - b.x), 2);
        float yDistance = Mathf.Pow((a.y - b.y), 2);
        return Mathf.Sqrt(xDistance + yDistance);
    }
}
