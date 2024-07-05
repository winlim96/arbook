using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PlayPause : MonoBehaviour
{   
    private VideoPlayer player;
    public Button button;
  
    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<VideoPlayer>();
        button.onClick.AddListener(ClickPlay);
    }


    public void ClickPlay()
    {
        if (player != null)
        {
            if (player.isPlaying)
            {

                player.Pause();
            }
            else
            {
                player.Play();
            }
        }
    }
}
