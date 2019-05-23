using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public Text p1Win;
    public Text p2Win;

    public Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.musicSource.Stop();
        Debug.Log("displays connected: " + Display.displays.Length);

        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();
        if (Display.displays.Length > 3)
            Display.displays[3].Activate();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
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
