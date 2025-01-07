using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    // The video player that plays in the intro animation
    [SerializeField] VideoPlayer videoPlayer;

    [SerializeField] GameObject videoUI;

    [SerializeField] EventSystem eventSystem;

    [SerializeField] GameObject startButton;

    private void Start()
    {
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.frame >= (long)videoPlayer.frameCount - 1)
        {
            videoPlayer.Stop();

            videoUI.SetActive(false);

            eventSystem.SetSelectedGameObject(startButton);
        }
    }

    public void SkipVideo()
    {
        videoPlayer.Stop();

        videoUI.SetActive(false);

        eventSystem.SetSelectedGameObject(startButton);
    }
}
