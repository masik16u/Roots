using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCursor0 : MonoBehaviour
{
    public AudioSource burn;

    void Start()
    {
        
    }

    void Update()
    {
        if(CompareTag("On"))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = worldPoint;
        }
        else
        {
            this.transform.position = new Vector2(9, 4);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.collider.name;
        if (collision.collider.CompareTag("Line") && name[name.Length - 1] != '~')
        {
            //Debug.Log(name.Substring(4, name.Length - 4));
            if(GameObject.Find(name.Substring(4, name.Length - 4)) && GameObject.Find(name.Substring(4, name.Length - 4)).name != "Circle")
            {
                GameObject obj = GameObject.Find(name.Replace("~", "").Substring(4, name.Length - 4));
                if (!obj.CompareTag("Cut") && obj.GetComponent<DrawLine>().width > 0.03f)
                {
                    if (!burn.isPlaying)
                    {
                        burn.Play(0);
                    }
                }
                obj.tag = "Cut";
            }
        }
        if (collision.collider.CompareTag("Line") && name[name.Length - 1] == '~')
        {
            //Debug.Log(name.Substring(4, name.Length - 4));
            if (GameObject.Find(name.Replace("~", "").Substring(4, name.Length - 5)) && GameObject.Find(name.Replace("~", "").Substring(4, name.Length - 5)).name != "Circle")
            {
                GameObject obj = GameObject.Find(name.Replace("~", "").Substring(4, name.Length - 5));
                if (!obj.CompareTag("Cut") && obj.GetComponent<DrawLine>().width > 0.03f)
                {
                    if (!burn.isPlaying)
                    {
                        burn.Play(0);
                    }
                }
                obj.tag = "Cut";
            }
        }
    }
}
