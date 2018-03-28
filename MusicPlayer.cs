using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    static MusicPlayer instance = null;
    public AudioClip menu;
    public AudioClip game;
    public AudioClip lose;

    private AudioSource music;
    void Awake()
    {
        music = GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music.clip = menu;
            music.Play();
            music.loop = true;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        music.Stop();

        if(level == 0)
        {
            music.clip = menu;
        }
        if (level == 1)
        {
            music.clip =game;
        }
        if (level == 2)
        {
            music.clip = lose ;
        }
        music.loop = true;

        music.Play();
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}

