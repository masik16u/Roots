using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleMove : MonoBehaviour
{
    public float animationDuration = 3f;

    public GameObject moleDot;
    public GameObject moleFinish;
    void Start()
    {
        StartCoroutine(RotationStart());
    }

    void Update()
    {
        transform.position = moleDot.transform.position;
        transform.right = -(moleFinish.transform.position - transform.position);
    }

    private IEnumerator RotationStart()
    {
        float startTime = Time.time;

        Quaternion sp = transform.rotation;
        transform.Rotate(new Vector3(0, 0, -5));
        Quaternion ep = transform.rotation;

        Quaternion ang = sp;
        while (ang != ep)
        {
            float t = (Time.time - startTime) / animationDuration;
            ang = Quaternion.Lerp(sp, ep, t);
            transform.rotation = ang;
            yield return null;
        }
        StartCoroutine(RotationEnd());
    }
    private IEnumerator RotationEnd()
    {
        float startTime = Time.time;

        transform.Rotate(new Vector3(0, 0, -5));
        Quaternion sp = transform.rotation;
        transform.Rotate(new Vector3(0, 0, 5));
        Quaternion ep = transform.rotation;

        Quaternion ang = sp;
        while (ang != ep)
        {
            float t = (Time.time - startTime) / animationDuration;
            ang = Quaternion.Lerp(sp, ep, t);
            transform.rotation = ang;
            yield return null;
        }
        StartCoroutine(RotationStart());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.collider.name;
        if (name.Substring(0, 4) == "Line" && name[name.Length - 1] != '~' && collision.collider.GetComponent<LineRenderer>())
        {
            //Debug.Log(name.Substring(4, name.Length - 4));
            if (collision.collider.GetComponent<LineRenderer>().startColor != new Color(0, 0, 0, 0) && collision.collider.tag != "DeadLine")
            {
                collision.collider.GetComponent<LineRenderer>().startColor = new Color(0.37f, 0.37f, 0.37f);
                collision.collider.GetComponent<LineRenderer>().endColor = new Color(0.37f, 0.37f, 0.37f);
            }
            collision.collider.tag = "DeadLine";
            if (GameObject.Find(name.Substring(4, name.Length - 4)) && GameObject.Find(name.Substring(4, name.Length - 4)).name != "Circle")
            {
                GameObject.Find(name.Substring(4, name.Length - 4)).tag = "Cut";
            }
        }
        if (name.Substring(0, 4) == "Line" && name[name.Length - 1] == '~' && collision.collider.GetComponent<LineRenderer>())
        {
            //Debug.Log(name.Substring(4, name.Length - 4));
            if(collision.collider.GetComponent<LineRenderer>().startColor != new Color(0, 0, 0, 0) && collision.collider.tag != "DeadLine")
            {
                collision.collider.GetComponent<LineRenderer>().startColor = new Color(0.37f, 0.37f, 0.37f);
                collision.collider.GetComponent<LineRenderer>().endColor = new Color(0.37f, 0.37f, 0.37f);
            }
            collision.collider.tag = "DeadLine";
            if (GameObject.Find(name.Replace("~", "").Substring(4, name.Length - 5)) && GameObject.Find(name.Replace("~", "").Substring(4, name.Length - 5)).name != "Circle")
            {
                GameObject.Find(name.Replace("~", "").Substring(4, name.Length - 5)).tag = "Cut";
            }
        }
    }
}
