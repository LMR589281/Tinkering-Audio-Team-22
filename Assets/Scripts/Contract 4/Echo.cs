﻿using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
//Base code provided from the first workshop
//Code for button from https://docs.unity3d.com/530/Documentation/ScriptReference/UI.Button-onClick.html
//Code by Luke Ryan
public class Echo : MonoBehaviour
{
    //declares all the variables needed
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public Button yourButton;

    //this function is loaded when the program is first loaded
    //the function creates the button and also generates the audio clip as well as adding the echo
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(1500);
        outAudioClip = echo_maker(outAudioClip);
    }

    //this function is activated when its button is click
    //the function just plays the created audio source
    void TaskOnClick()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    //this function is loaded when the tone is being created
    //the function takes in a tone and then creates an array of samples baised on a sine wave
    private AudioClip CreateToneAudioClip(int frequency)
    {
        int sampleDurationSecs = 5;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

    //this function is loaded when the tone is being created
    //the function adds echo by adding a lowerer frequencey at a delayed time 
    private AudioClip echo_maker(AudioClip audioClip)
    {
        float[] samples = new float[outAudioClip.samples];      //takes the samples out of the audio clip
        outAudioClip.GetData(samples, 0);  

        float[] copy = samples;    //copys the array of samples
        int offset = samples.Length / 6;    //sets the off set for the echo

        for (int i = 0; i < samples.Length; i++)    //loops to add the echo to the copy array
        {
            float echo = samples[i] * 0.6f;     //creates the echo
            if (i + offset < samples.Length)        //stops the echo being added to a point not in the list 
            {
                copy[i + offset] += echo;       //adds the echo to the array
            }
        }

        float[] newEcho = copy;
        outAudioClip.SetData(newEcho, 0);   
        return audioClip;       //returns the music clip
    }

}