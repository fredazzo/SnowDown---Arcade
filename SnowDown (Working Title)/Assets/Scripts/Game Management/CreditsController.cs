using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{

    public Animator transitionAnim;


    public string altConfirmP1;
    public string altConfirmP2;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(altConfirmP1) || Input.GetButtonDown(altConfirmP2))
            StartCoroutine(OnSceneLoad("Menu"));
        if (Input.GetKeyDown(KeyCode.Delete))
            Application.Quit();


    }

    IEnumerator OnSceneLoad(string scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }

}
