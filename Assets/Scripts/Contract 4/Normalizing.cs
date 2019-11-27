using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Normalizing : MonoBehaviour
{
    //declares all the variables needed
    public Button yourButton;
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    //this function is loaded when the program is first loaded
    //the function creates the button and also generates the audio clip as well as normalizing the sound
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(1500);
        outAudioClip = normalizer(outAudioClip);
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
    //the function normalizes the sound by making the sound of the audio baised on the loudest sound in the audio clip
    private AudioClip normalizer(AudioClip audioClip)
    {
        float[] samples = new float[outAudioClip.samples];
        outAudioClip.GetData(samples, 0);

        float largest_sample = 0;

        for (int i = 0; i < samples.Length; i++)
        {
            if (largest_sample < samples[i])
            {
                largest_sample = samples[i];
            }
        }
        float amplification = 32767.0f / largest_sample;

        for (int i = 0; i < samples.Length; i++)
        {
            float new_sample = amplification * samples[i];
            samples[i] = new_sample;
        }
        outAudioClip.SetData(samples, 0);
        return audioClip;
    }
}
