﻿using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
//halving pitch
public class Halving : MonoBehaviour
{
    public Button yourButton;
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(1500);
    }

    void TaskOnClick()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    // Public APIs

    // Private 
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
}

/*
function half(source):
    target = makeEmptySound(getLength(source) * 2);
    sourceIndex = 0;
    for targetIndex in range(0, getLength( target)):
        value = getSampleValueAt( source, int(sourceIndex));
        setSampleValueAt( target, targetIndex, value);
        sourceIndex = sourceIndex + 0.5;
    endfor
    return target;
endfunction

*/