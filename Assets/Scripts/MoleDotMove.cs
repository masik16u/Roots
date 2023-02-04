using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleDotMove : MonoBehaviour
{
    public GameObject circle;
    public GameObject dSide;
    public GameObject moleFinish;
    public Text pressStart;
    public GameObject cam;

    public GameObject square;
    public Text bscore;

    public float speed = 0.8f;

    public AudioSource moleDig;

    void Update()
    {
        if(circle.CompareTag("Seed") || circle.CompareTag("Dead"))
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, moleFinish.transform.position, step);
            if (!moleDig.isPlaying)
            {
                moleDig.Play(0);
            }
        }
        if(circle.CompareTag("Dead") || dSide.GetComponent<DrawLine>().width < 0.03f)
        {
            speed = 3;
        }
        if(transform.position == moleFinish.transform.position)
        {
            if (cam.GetComponent<GameManager>().tutorial != 2)
            {
                pressStart.GetComponent<Text>().text = "Press |R| to Restart";
                pressStart.GetComponent<Text>().enabled = true;
                square.GetComponent<SpriteRenderer>().enabled = true;
                bscore.GetComponent<Text>().enabled = true;
            }
            moleDig.Stop();
        }
    }
}
