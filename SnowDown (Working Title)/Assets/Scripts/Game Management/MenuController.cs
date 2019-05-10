using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject start;
    public GameObject credits;

    public Vector3 originalStart;
    public Vector3 originalCredits;

    bool startSelected;
    bool creditsSelected;

    public string startSelection;
    public string creditsSelection;
    public string confirm;

    public string gameScene;
    public string creditsScene;

    public Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        startSelected = true;
        originalStart = start.transform.localScale;
        originalCredits = credits.transform.localScale;
        Enlarge(start);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        ResetSize(start, startSelected, originalStart);
        ResetSize(credits, creditsSelected, originalCredits);

        if(Input.GetButtonDown(startSelection))
        {
            if(!startSelected)
            {
                Enlarge(start);
            }
            startSelected = true;
            creditsSelected = false;

        }
        else if(Input.GetButtonDown(creditsSelection))
        {
            if(!creditsSelected)
            {
                Enlarge(credits);
            }
            startSelected = false;
            creditsSelected = true;
  
        }


        if(startSelected && Input.GetButtonDown(confirm))
        {
           StartCoroutine(OnSceneLoad(gameScene));
        }
        if(creditsSelected && Input.GetButtonDown(confirm))
        {
            StartCoroutine(OnSceneLoad(creditsScene));
        }
    }

    void Enlarge(GameObject button)
    {
          button.transform.localScale += new Vector3(0.3f, 0.3f, 0.0f);
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
