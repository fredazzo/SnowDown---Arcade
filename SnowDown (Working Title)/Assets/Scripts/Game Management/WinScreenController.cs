using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public Text p1Win;
    public Text p2Win;
    private int winner;

    public Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        winner = PlayerPrefs.GetInt("Winner");
        SoundManager.instance.musicSource.Stop();
       
        if(winner == 1)
        {
            print("got here");
            p1Win.text = "Player 1 Won!";
            p2Win.text = "Player 2 Lose";
        }
        else if(winner == 2)
        {
            p1Win.text = "Player 1 Lose";
            p2Win.text = "Player 2 Won!";
        }

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
