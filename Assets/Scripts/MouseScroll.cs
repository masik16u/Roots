using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseScroll : MonoBehaviour
{
    public float num = 0;
    public GameObject d;

    public GameObject fireCursor;
    public GameObject magnetCursor;
    public GameObject circle;
    public GameObject rSide;
    public GameObject lSide;
    public GameObject dSide;

    public GameObject mole;
    public GameObject moleDot;
    public GameObject moleFinish;

    public Text score;
    public Text pressStart;

    public AudioSource buttonSound;
    public AudioSource toolChange;

    public GameObject square;
    public Text bscore;

    public int tutorial = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("Tutorial", 0);

        if (PlayerPrefs.GetInt("Tutorial") != 1)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.Save();
            moleDot.SetActive(false);
            fireCursor.SetActive(false);
            magnetCursor.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Tutorial") == 9)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            d.name = "Death1";
            d.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
            Instantiate(d, new Vector2(Random.Range(-7f, -1f), Random.Range(2f, -4f)), d.transform.rotation);
            d.name = "Death2";
            d.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
            Instantiate(d, new Vector2(Random.Range(1f, 7f), Random.Range(2f, -4f)), d.transform.rotation);
            d.name = "Death";
            if (Random.Range(0f, 1f) < 0.5f)
            {
                moleDot.transform.position = new Vector2(10, Random.Range(3f, -4f));
                moleFinish.transform.position = new Vector2(-10, Random.Range(3f, -4f));
                mole.GetComponent<SpriteRenderer>().flipY = false;
            }
            else
            {
                moleDot.transform.position = new Vector2(-10, Random.Range(3f, -4f));
                moleFinish.transform.position = new Vector2(10, Random.Range(3f, -4f));
                mole.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        //Generate Level


        Cursor.visible = false;
    }

    void Update()
    {
        //TUTORIAL CH1
        if (tutorial == 0 && dSide.GetComponent<DrawLine>().width < 0.03f)
        {
            pressStart.GetComponent<Text>().text = "Press |E| to Continue";
            pressStart.GetComponent<Text>().enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                buttonSound.Play(0);

                foreach (GameObject i in GameObject.FindGameObjectsWithTag("DeadLine"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Line"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Copy"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Right"))
                {
                    if (i.name != "RightSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Left"))
                {
                    if (i.name != "LeftSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Down"))
                {
                    if (i.name != "DownSide")
                    {
                        Destroy(i);
                    }
                }

                circle.tag = "Untagged";
                circle.GetComponent<DrawLine>().width = 0.3f;
                circle.GetComponent<DrawLine>().direction = 0.0f;
                circle.GetComponent<DrawLine>().start = GameObject.Find("Circle").transform.position;
                rSide.GetComponent<DrawLine>().width = 0.15f;
                lSide.GetComponent<DrawLine>().width = 0.15f;
                dSide.GetComponent<DrawLine>().width = 0.15f;

                d.name = "Death1";
                d.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
                Instantiate(d, new Vector2(0, -3), d.transform.rotation);
                d.name = "Death";

                pressStart.GetComponent<Text>().text = "Press |Space| to Start";
                tutorial = 1;
            }
        }

        //TUTORIAL CH2
        if (tutorial == 1 && circle.tag == "Dead")
        {
            pressStart.GetComponent<Text>().text = "Press |E| to Continue";
            pressStart.GetComponent<Text>().enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                buttonSound.Play(0);

                Destroy(GameObject.Find("Death1(Clone)"));

                foreach (GameObject i in GameObject.FindGameObjectsWithTag("DeadLine"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Line"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Copy"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Right"))
                {
                    if (i.name != "RightSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Left"))
                {
                    if (i.name != "LeftSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Down"))
                {
                    if (i.name != "DownSide")
                    {
                        Destroy(i);
                    }
                }

                circle.tag = "Untagged";
                circle.GetComponent<DrawLine>().width = 0.3f;
                circle.GetComponent<DrawLine>().direction = 0.0f;
                circle.GetComponent<DrawLine>().start = GameObject.Find("Circle").transform.position;
                rSide.GetComponent<DrawLine>().width = 0.15f;
                lSide.GetComponent<DrawLine>().width = 0.15f;
                dSide.GetComponent<DrawLine>().width = 0.15f;

                moleDot.SetActive(true);
                moleDot.transform.position = new Vector2(-10, -1);
                moleFinish.transform.position = new Vector2(10, 1);
                mole.GetComponent<SpriteRenderer>().flipY = true;

                pressStart.GetComponent<Text>().text = "Press |Space| to Start";
                tutorial = 2;
            }
        }

        //TUTORIAL CH3
        if (tutorial == 2 && moleDot.transform.position == moleFinish.transform.position)
        {
            pressStart.GetComponent<Text>().text = "Press |E| to Continue";
            pressStart.GetComponent<Text>().enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                buttonSound.Play(0);

                moleDot.GetComponent<MoleDotMove>().speed = 0.8f;
                mole.GetComponent<TrailRenderer>().Clear();
                moleDot.SetActive(false);
                moleFinish.transform.position = new Vector2(12, 0);

                foreach (GameObject i in GameObject.FindGameObjectsWithTag("DeadLine"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Line"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Copy"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Right"))
                {
                    if (i.name != "RightSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Left"))
                {
                    if (i.name != "LeftSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Down"))
                {
                    if (i.name != "DownSide")
                    {
                        Destroy(i);
                    }
                }

                circle.tag = "Untagged";
                circle.GetComponent<DrawLine>().width = 0.3f;
                circle.GetComponent<DrawLine>().direction = 0.0f;
                circle.GetComponent<DrawLine>().start = GameObject.Find("Circle").transform.position;
                rSide.GetComponent<DrawLine>().width = 0.15f;
                lSide.GetComponent<DrawLine>().width = 0.15f;
                dSide.GetComponent<DrawLine>().width = 0.15f;

                fireCursor.SetActive(true);
                magnetCursor.SetActive(true);

                pressStart.GetComponent<Text>().text = "Press |R| to End Tutorial";
                tutorial = 3;
            }
        }

        //TUTORIAL CH4
        if (tutorial == 3)
        {
            if (circle.CompareTag("Untagged"))
            {
                circle.tag = "Seed";
            }
            if (dSide.GetComponent<DrawLine>().width < 0.03f)
            {
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("DeadLine"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Line"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Copy"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Right"))
                {
                    if (i.name != "RightSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Left"))
                {
                    if (i.name != "LeftSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Down"))
                {
                    if (i.name != "DownSide")
                    {
                        Destroy(i);
                    }
                }

                circle.tag = "Untagged";
                circle.GetComponent<DrawLine>().width = 0.3f;
                circle.GetComponent<DrawLine>().direction = 0.0f;
                circle.GetComponent<DrawLine>().start = GameObject.Find("Circle").transform.position;
                rSide.GetComponent<DrawLine>().width = 0.15f;
                lSide.GetComponent<DrawLine>().width = 0.15f;
                dSide.GetComponent<DrawLine>().width = 0.15f;

                pressStart.GetComponent<Text>().enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                buttonSound.Play(0);

                moleDot.SetActive(true);

                d.name = "Death1";
                d.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
                Instantiate(d, new Vector2(Random.Range(-7f, -1f), Random.Range(2f, -4f)), d.transform.rotation);
                d.name = "Death2";
                d.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
                Instantiate(d, new Vector2(Random.Range(1f, 7f), Random.Range(2f, -4f)), d.transform.rotation);
                d.name = "Death";

                if (Random.Range(0f, 1f) < 0.5f)
                {
                    moleDot.transform.position = new Vector2(10, Random.Range(3f, -4f));
                    moleFinish.transform.position = new Vector2(-10, Random.Range(3f, -4f));
                    mole.GetComponent<SpriteRenderer>().flipY = false;
                    mole.transform.position = new Vector2(13, -7);
                }
                else
                {
                    moleDot.transform.position = new Vector2(-10, Random.Range(3f, -4f));
                    moleFinish.transform.position = new Vector2(10, Random.Range(3f, -4f));
                    mole.GetComponent<SpriteRenderer>().flipY = true;
                    mole.transform.position = new Vector2(-13, -7);
                }
                moleDot.GetComponent<MoleDotMove>().speed = 0.8f;
                mole.GetComponent<TrailRenderer>().Clear();

                foreach (GameObject i in GameObject.FindGameObjectsWithTag("DeadLine"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Line"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Copy"))
                {
                    Destroy(i);
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Right"))
                {
                    if (i.name != "RightSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Left"))
                {
                    if (i.name != "LeftSide")
                    {
                        Destroy(i);
                    }
                }
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Down"))
                {
                    if (i.name != "DownSide")
                    {
                        Destroy(i);
                    }
                }

                circle.tag = "Untagged";
                circle.GetComponent<DrawLine>().width = 0.3f;
                circle.GetComponent<DrawLine>().direction = 0.0f;
                circle.GetComponent<DrawLine>().start = GameObject.Find("Circle").transform.position;
                rSide.GetComponent<DrawLine>().width = 0.15f;
                lSide.GetComponent<DrawLine>().width = 0.15f;
                dSide.GetComponent<DrawLine>().width = 0.15f;

                pressStart.GetComponent<Text>().text = "Press |Space| to Start";

                tutorial = 4;
            }
        }

        //Scroll between Tools
        if (num != num + Input.GetAxis("Mouse ScrollWheel"))
        {
            toolChange.Play(0);
            num += Mathf.Round(Input.GetAxis("Mouse ScrollWheel") * 10);
            if (num > 1)
            {
                num = 0;
            }
            else if (num < 0)
            {
                num = 1;
            }
            if (num == 0)
            {
                fireCursor.tag = "On";
                magnetCursor.tag = "Off";
            }
            if (num == 1)
            {
                magnetCursor.tag = "On";
                fireCursor.tag = "Off";
            }
        }

        //Update Score IRL
        if (GameObject.FindGameObjectWithTag("Line"))
        {
            score.GetComponent<Text>().text = "" + GameObject.FindGameObjectsWithTag("Line").Length;
            bscore.GetComponent<Text>().text = "Your Score: " + GameObject.FindGameObjectsWithTag("Line").Length;
        }
        else
        {
            score.GetComponent<Text>().text = "0";
            bscore.GetComponent<Text>().text = "Your Score: 0";
        }


        if(Input.GetKeyDown(KeyCode.Space) && circle.CompareTag("Untagged") && tutorial != 3)
        {
            mole.GetComponent<TrailRenderer>().time = 100;
            buttonSound.Play(0);
            pressStart.GetComponent<Text>().enabled = false;
            circle.tag = "Seed";
        }

        //Regenerate Level
        if (Input.GetKeyDown(KeyCode.R) && moleDot.transform.position == moleFinish.transform.position && tutorial != 2)
        {
            buttonSound.Play(0);
            Destroy(GameObject.Find("Death1(Clone)"));
            Destroy(GameObject.Find("Death2(Clone)"));

            d.name = "Death1";
            d.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
            Instantiate(d, new Vector2(Random.Range(-7f, -1f), Random.Range(2f, -4f)), d.transform.rotation);
            d.name = "Death2";
            d.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
            Instantiate(d, new Vector2(Random.Range(1f, 7f), Random.Range(2f, -4f)), d.transform.rotation);
            d.name = "Death";

            if (Random.Range(0f, 1f) < 0.5f)
            {
                moleDot.transform.position = new Vector2(10, Random.Range(3f, -4f));
                moleFinish.transform.position = new Vector2(-10, Random.Range(3f, -4f));
                mole.GetComponent<SpriteRenderer>().flipY = false;
                mole.transform.position = new Vector2(13, -7);
            }
            else
            {
                moleDot.transform.position = new Vector2(-10, Random.Range(3f, -4f));
                moleFinish.transform.position = new Vector2(10, Random.Range(3f, -4f));
                mole.GetComponent<SpriteRenderer>().flipY = true;
                mole.transform.position = new Vector2(-13, -7);
            }
            moleDot.GetComponent<MoleDotMove>().speed = 0.8f;
            mole.GetComponent<TrailRenderer>().Clear();

            foreach (GameObject i in GameObject.FindGameObjectsWithTag("DeadLine"))
            {
                Destroy(i);
            }
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Line"))
            {
                Destroy(i);
            }
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Copy"))
            {
                Destroy(i);
            }
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Right"))
            {
                if(i.name != "RightSide")
                {
                    Destroy(i);
                }
            }
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Left"))
            {
                if (i.name != "LeftSide")
                {
                    Destroy(i);
                }
            }
            foreach (GameObject i in GameObject.FindGameObjectsWithTag("Down"))
            {
                if (i.name != "DownSide")
                {
                    Destroy(i);
                }
            }

            circle.tag = "Untagged";
            circle.GetComponent<DrawLine>().width = 0.3f;
            circle.GetComponent<DrawLine>().direction = 0.0f;
            circle.GetComponent<DrawLine>().start = GameObject.Find("Circle").transform.position;
            rSide.GetComponent<DrawLine>().width = 0.15f;
            lSide.GetComponent<DrawLine>().width = 0.15f;
            dSide.GetComponent<DrawLine>().width = 0.15f;

            pressStart.GetComponent<Text>().text = "Press |Space| to Start";

            square.GetComponent<SpriteRenderer>().enabled = false;
            bscore.GetComponent<Text>().enabled = false;
        }
    }
}
