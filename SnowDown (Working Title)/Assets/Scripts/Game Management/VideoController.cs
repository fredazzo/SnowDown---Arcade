using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public RawImage image;

    public VideoClip videoToPlay;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    private AudioSource audioSource;
    private bool finished;

    public Animator transitionAnim;

    public string altConfirmP1;
    public string altConfirmP2;


    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
        finished = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown(altConfirmP1) || Input.GetButtonDown(altConfirmP2))
            StartCoroutine(OnSceneLoad("Menu"));
        if (finished)
            StartCoroutine(OnSceneLoad("Gameplay"));
    }

    IEnumerator playVideo()

    {
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.VideoClip;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)

        {
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)

        {

            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));

            yield return null;

        }



        Debug.Log("Done Playing Video");
        finished = true;
    }

    IEnumerator OnSceneLoad(string scene)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }

}
