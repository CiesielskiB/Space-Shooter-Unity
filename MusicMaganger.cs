using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMaganger : MonoBehaviour {

    static MusicMaganger instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
	void Start () {
        
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
