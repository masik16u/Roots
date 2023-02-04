using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillRoots : MonoBehaviour
{
    public AudioSource rootsDeath;

    Vector2 pos;
    void Start()
    {
        pos = this.transform.position;
    }
    void Update()
    {
        this.transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Line"))
        {
            if (GameObject.FindGameObjectWithTag("Seed"))
            {
                GameObject.FindGameObjectWithTag("Seed").tag = "Dead";
                rootsDeath.Play(0);

            }
            if (GameObject.FindGameObjectWithTag("Right"))
            {
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Right"))
                {
                    if (i.name != "RightSide")
                    {
                        i.tag = "Dead";
                    }
                }
            }
            if (GameObject.FindGameObjectWithTag("Left"))
            {
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Left"))
                {
                    if (i.name != "LeftSide")
                    {
                        i.tag = "Dead";
                    }
                }
            }
            if (GameObject.FindGameObjectWithTag("Down"))
            {
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Down"))
                {
                    if (i.name != "DownSide")
                    {
                        i.tag = "Dead";
                    }
                }
            }
            foreach(GameObject i in GameObject.FindGameObjectsWithTag("Line"))
            {
                i.tag = "DeadLine";
                if (i.GetComponent<LineRenderer>().startColor != new Color(0, 0, 0, 0))
                {
                    i.GetComponent<LineRenderer>().startColor = new Color(0.17f, 0.17f, 0.17f);
                    i.GetComponent<LineRenderer>().endColor = new Color(0.17f, 0.17f, 0.17f);
                }
            }
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("DeadLine"))
            {
                if (i.GetComponent<LineRenderer>().startColor != new Color(0, 0, 0, 0))
                {
                    i.GetComponent<LineRenderer>().startColor = new Color(0.17f, 0.17f, 0.17f);
                    i.GetComponent<LineRenderer>().endColor = new Color(0.17f, 0.17f, 0.17f);
                }
            }
        }
    }
}
