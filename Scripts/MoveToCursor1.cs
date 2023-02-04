using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCursor1 : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (CompareTag("On"))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = worldPoint;
        }
        else
        {
            this.transform.position = new Vector2(9, 3);
        }
    }
}
