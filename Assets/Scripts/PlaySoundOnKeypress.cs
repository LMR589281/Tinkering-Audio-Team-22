﻿using System.Collections;
using UnityEngine;
public class PlaySoundOnKeypress : MonoBehaviour
{
    public AudioSource Jump;
    public AudioSource FlyWalk;
    public AudioSource Quack;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Plays an audio clip after button is pressed.
    void Update()
    {if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            FlyWalk.Play();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump.Play();
        if (Input.GetKeyDown(KeyCode.Q))
            Quack.Play();

    }
}
