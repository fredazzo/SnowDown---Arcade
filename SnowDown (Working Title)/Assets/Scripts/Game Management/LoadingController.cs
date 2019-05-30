using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public Image p1Fill;
    public Image p2Fill;

    public Animator transitionAnim;

    public string nextScene;

    private float timer = 0f;
    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        p1Fill.fillAmount = CalculateFill();
        p2Fill.fillAmount = CalculateFill();

        if (timer > maxTime)
            StartCoroutine(OnSceneLoad(nextScene));
    }

    private float CalculateFill()
    {
        return timer / maxTime;
    }

    IEnumerator OnSceneLoad(string scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }

}
