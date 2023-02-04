using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    public Vector2 start;
    Vector2 end;
    public float width = 0.2f;
    public float direction = 0f;

    float time = 0f;
    public float timeDelay = 1f;

    public float animationDuration = 1f;
    LineRenderer lineRenderer;

    public GameObject circle;
    public GameObject rSide;
    public GameObject lSide;
    public GameObject dSide;
    public GameObject magnet;

    int rightCount = 0;
    int leftCount = 0;
    int downCount = 0;

    [System.Obsolete]
    void Start()
    {
        start = this.transform.position;
    }

    void Update()
    {
        if (magnet.CompareTag("On") && circle.CompareTag("Seed"))
        {
            if(magnet.transform.position.x / 22 > 0.3f)
            {
                direction = 0.3f;
            }
            else if (magnet.transform.position.x / 22 < -0.3f)
            {
                direction = -0.3f;
            }
            else
            {
                direction = magnet.transform.position.x / 22;
            }
        }
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        time += 1f * Time.deltaTime;

        if (time > timeDelay && circle.CompareTag("Seed"))
        {
            time = 0f;
            if (width > 0.02f)
            {
                Draw();
            }
        }
    }

    [System.Obsolete]
    void Draw()
    {
        if (!CompareTag("Cut") && !CompareTag("Dead"))
        {
            if (CompareTag("Seed"))
            {
                end = MainEnd(start);
                Line(start, end + new Vector2(0, (width / 3 * 10) * -0.05f), new Color(0.67f, 0.67f, 0.67f), width);
                if (Random.Range(0f, 1f) < 0.4f)
                {
                    rSide.name = "Right" + rightCount;
                    Instantiate(rSide, start, Quaternion.identity);
                    rSide.name = "RightSide";
                    rightCount += 1;
                }
                if (Random.Range(0f, 1f) < 0.4f)
                {
                    lSide.name = "Left" + leftCount;
                    Instantiate(lSide, start, Quaternion.identity);
                    lSide.name = "LeftSide";
                    leftCount += 1;
                }
            }
            else if (CompareTag("Right"))
            {
                end = RightEnd(start);
                Line(start, end + new Vector2((width / 3 * 10) * 0.03f, 0), new Color(0.67f, 0.67f, 0.67f), width);
                if (Random.Range(0f, 1f) < 0.4f && width > 0.05f && name != "RightSide" && name != "LeftSide")
                {
                    dSide.name = "Down" + downCount + name;
                    Instantiate(dSide, start, Quaternion.identity);
                    dSide.name = "DownSide";
                    downCount += 1;
                }
            }
            else if (CompareTag("Left"))
            {
                end = LeftEnd(start);
                Line(start, end + new Vector2((width / 3 * 10) * -0.03f, 0), new Color(0.67f, 0.67f, 0.67f), width);
                if (Random.Range(0f, 1f) < 0.4f && width > 0.05f && name != "RightSide" && name != "LeftSide")
                {
                    dSide.name = "Down" + downCount + name;
                    Instantiate(dSide, start, Quaternion.identity);
                    dSide.name = "DownSide";
                    downCount += 1;
                }
            }
            else if (CompareTag("Down"))
            {
                end = DownEnd(start);
                Line(start, end + new Vector2(0, (width / 3 * 10) * -0.02f), new Color(0.67f, 0.67f, 0.67f), width);
            }
            start = end;
            width -= 0.01f;
        }
        else if (CompareTag("Cut") || CompareTag("Dead"))
        {
            if (CompareTag("Dead"))
            {
                width = 0;
            }
            if (GameObject.Find("Line" + name + "~"))
            {
                if (GameObject.Find("Line" + name + "~").GetComponent<LineRenderer>())
                {
                    StartCoroutine(Burn());
                }
            }
        }
    }

    [System.Obsolete]
    void Line(Vector2 start, Vector2 end, Color color, float width)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lineRenderer = lr;
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(width, width - 0.015f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        if(GameObject.Find("Line" + name + "~"))
        {
            lr.name = "Line" + name + "~";
            GameObject.Find("Line" + name + "~").name = "Line" + name;
        }
        else
        {
            lr.name = "Line" + name + "~";
        }

        myLine.AddComponent<BoxCollider2D>();
        BoxCollider2D col;
        col = myLine.transform.gameObject.GetComponent<BoxCollider2D>();

        myLine.GetComponent<LineRenderer>().sortingLayerName = "Roots";

        if (name != "RightSide" && name != "LeftSide" && name != "DownSide")
        {
            myLine.tag = "Line";
        }
        else
        {
            myLine.tag = "Copy";
        }

        float a = end.y - start.y;
        float b = start.x - end.x;
        float cos = b / (Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2)));
        float deg = Mathf.Acos(cos) * Mathf.Rad2Deg;

        col.transform.eulerAngles = new Vector3(0, 0, deg);
        col.transform.position = new Vector3((start.x + end.x) / 2, (start.y + end.y) / 2, 0);
        col.offset = new Vector2(0, 0);
        col.size = new Vector2(Mathf.Sqrt(Mathf.Pow(end.x - start.x, 2) + Mathf.Pow(end.y - start.y, 2)), width);

        StartCoroutine(AnimateLine());
    }

    private IEnumerator AnimateLine()
    {
        float startTime = Time.time;

        Vector2 sp = lineRenderer.GetPosition(0);
        Vector2 ep = lineRenderer.GetPosition(1);

        Vector2 pos = sp;
        while(pos != ep)
        {
            float t = (Time.time - startTime) / animationDuration;
            pos = Vector2.Lerp(sp, ep, t);
            try
            {
                lineRenderer.SetPosition(1, pos);
            }
            catch
            {
                pos = ep;
            }
            yield return null;
        }
    }

    private IEnumerator Burn()
    {
        float startTime = Time.time;

        GameObject line = GameObject.Find("Line" + name + "~");

        Color sc = line.GetComponent<LineRenderer>().endColor;
        Color ec = new Color(0, 0, 0, 0);

        Color col = sc;
        while (col != ec)
        {
            float t = (Time.time - startTime) / animationDuration;
            col = Color.Lerp(sc, ec, t);
            line.GetComponent<LineRenderer>().GetComponent<LineRenderer>().startColor = col;
            line.GetComponent<LineRenderer>().GetComponent<LineRenderer>().endColor = col;
            yield return null;
        }

        line.tag = "DeadLine";
    }

    Vector2 MainEnd(Vector2 start)
    {
        Vector2 end = new Vector2(start.x + Random.Range(direction - 0.1f, direction + 0.1f), start.y - 0.32f);
        return end;
    }
    Vector2 RightEnd(Vector2 start)
    {
        Vector2 end = new Vector2(start.x + 0.4f, start.y + Random.Range(- 0.3f, - 0.1f));
        return end;
    }
    Vector2 LeftEnd(Vector2 start)
    {
        Vector2 end = new Vector2(start.x - 0.4f, start.y + Random.Range(- 0.3f, - 0.1f));
        return end;
    }
    Vector2 DownEnd(Vector2 start)
    {
        Vector2 end = new Vector2(start.x + Random.Range(-0.1f, 0.1f), start.y - 0.2f );
        return end;
    }
}
