﻿using System.Collections;
using System.Collections.Generic;
//using NaughtyAttributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class Echo : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    public int sampleDurationSecs = 5;//length, 5
    public int tone = 1500;//tone, 1500

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //outAudioClip = echo_maker<samples>();
        outAudioClip = CreateToneAudioClip(tone);//tone, 1500
        PlayOutAudio();
    }


    // Public APIs
    public void PlayOutAudio()
    {
        audioSource.PlayOneShot(outAudioClip);
    }


    public void StopAudio()
    {
        audioSource.Stop();
    }


    // Private 
    private AudioClip CreateToneAudioClip(int frequency)
    {
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float) i  / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
            //print(samples);
        }

        audioClip.SetData(samples, 0);
        return audioClip;
        //return samples;
    }

/*
    private void echo_maker(music_array) {

        copy = music_array;

        for (music_array.Length; music_array.Length<i ; i++)
        {
            float echo = 0.6 * getSampleValueAt(copy, i-3);
            float combo = getSampleValueAt(music_array[i], i)+echo;
            setSampleValueAt(music_array[i], i, combo);
        
        }
    }
*/

#if UNITY_EDITOR
    //[Button("Save Wav file")]
    private void SaveWavFile() {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = CreateToneAudioClip(1500);
        SaveWavUtil.Save(path, audioClip);
    }
#endif
}