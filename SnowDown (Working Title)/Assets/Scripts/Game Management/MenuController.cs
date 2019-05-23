﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject start;
    public GameObject credits;
    public GameObject cursorP1;
    public GameObject cursorP2;

    public Transform[] positionsP1;
    public Transform[] positionsP2;

    public Vector3 originalStart;
    public Vector3 originalCredits;

    public bool startSelectedP1;
    public bool creditsSelectedP1;
    public bool confirmSelectedP1;

    public bool startSelectedP2;
    public bool creditsSelectedP2;
    public bool confirmSelectedP2;

    public KeyCode startP1;
    public KeyCode creditsP1;
    public KeyCode confirmP1;

    public KeyCode startP2;
    public KeyCode creditsP2;
    public KeyCode confirmP2;

    public string gameScene;
    public string creditsScene;

    public Text readyP1;
    public Text readyP2;

    public SpriteRenderer startSprite;
    public SpriteRenderer creditsSprite;
    public Sprite startSnowball;
    public Sprite creditsSnowball;

    public Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("displays connected: " + Display.displays.Length);

        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();
        if (Display.displays.Length > 3)
            Display.displays[3].Activate();

        startSelectedP1 = true;
        startSelectedP2 = true;
        creditsSelectedP1 = false;
        creditsSelectedP2 = false;
        //originalStart = start.transform.localScale;
        //originalCredits = credits.transform.localScale;
        //Enlarge(start);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.Delete))
            Application.Quit();

        if (Input.GetKeyDown(startP1))
        {
            startSelectedP1 = true;
            creditsSelectedP1 = false;
        }
        else if (Input.GetKeyDown(creditsP1))
        {
            startSelectedP1 = false;
            creditsSelectedP1 = true;
        }
        if (startSelectedP1)
        {
            cursorP1.transform.position = positionsP1[0].position;
        }
        else if (creditsSelectedP1)
        {
            cursorP1.transform.position = positionsP1[1].position;
        }

        if (Input.GetKeyDown(confirmP1))
        {
            confirmSelectedP1 = true;
        }

        if (confirmSelectedP1)
        {
            readyP1.enabled = true;
            if (Input.GetKeyDown(startP1) || Input.GetKeyDown(creditsP1))
            {
                confirmSelectedP1 = false;
            }
        }
        else
            readyP1.enabled = false;

        if (Input.GetKeyDown(startP2))
        {
            startSelectedP2 = true;
            creditsSelectedP2 = false;
        }
        else if (Input.GetKeyDown(creditsP2))
        {
            startSelectedP2 = false;
            creditsSelectedP2 = true;
        }
        if (startSelectedP2)
        {
            cursorP2.transform.position = positionsP2[0].position;
        }
        if (creditsSelectedP2)
        {
            cursorP2.transform.position = positionsP2[1].position;
        }
        if (Input.GetKeyDown(confirmP2))
        {
            confirmSelectedP2 = true;
        }
        if (confirmSelectedP2)
        {
            readyP2.enabled = true;
            if (Input.GetKeyDown(startP2) || Input.GetKeyDown(creditsP2))
            {
                confirmSelectedP2 = false;
            }
        }
        else
            readyP2.enabled = false;



        if (startSelectedP1 && startSelectedP2 && confirmSelectedP1 && confirmSelectedP2)
        {
            startSprite.sprite = startSnowball;
            StartCoroutine(OnSceneLoad(gameScene));
        }
 

        if (creditsSelectedP1 && creditsSelectedP2 && confirmSelectedP1 && confirmSelectedP2)
        {
            creditsSprite.sprite = creditsSnowball;
            StartCoroutine(OnSceneLoad(creditsScene));
        }
    }

    void Enlarge(GameObject button)
    {
          button.transform.localScale += new Vector3(0.2f, 0.2f, 0.0f);
    }

    void ResetSize(GameObject button, bool selection, Vector3 originalSize)
    {
        if (!selection)
        {
            button.transform.localScale = originalSize;
        }
    }

    IEnumerator OnSceneLoad(string scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }

}
