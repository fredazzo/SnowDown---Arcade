using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public Text p1Text;
    public Text p2Text;
    private int winner;

    public SpriteRenderer screen;
    public Sprite playerOneWin;
    public Sprite playerTwoWin;

    public Animator transitionAnim;

    private float timer = 0f;
    public float maxTime;

    public KeyCode p1Confirm;
    public KeyCode p2Confirm;

    private bool p1Ready;
    private bool p2Ready;

    // Start is called before the first frame update
    void Start()
    {
        winner = PlayerPrefs.GetInt("Winner");
        SoundManager.instance.musicSource.Stop();
       
        if(winner == 1)
        {
            p1Text.text = "You Won!";
            p2Text.text = "You Lost!";
            screen.sprite = playerOneWin;
        }
        else if(winner == 2)
        {
            p1Text.text = "You Lost!";
            p2Text.text = "You Won!";
            screen.sprite = playerTwoWin;
        }

        p1Ready = false;
        p2Ready = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
            Application.Quit();

        if (Input.GetKeyDown(p1Confirm))
            p1Ready = true;
        if (Input.GetKeyDown(p2Confirm))
            p2Ready = true;

        timer += Time.deltaTime;

        if (p1Ready && p2Ready)
            StartCoroutine(OnSceneLoad("Menu"));
        if (timer > maxTime)
            StartCoroutine(OnSceneLoad("Menu"));
    }

    IEnumerator OnSceneLoad(string scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }

}
