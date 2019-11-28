﻿using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
//doubling pitch
public class Doubling : MonoBehaviour
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
function double (source):
    len = getLength(source) / 2 + 1;
    target = makeEmptySound(len);
    targetIndex = 0;
    for sourceIndex in range(0, getLength(source), 2) do:
        value = getSampleValueAt(source, sourceIndex);
        setSampleValueAt(target, targetIndex, value);
        targetIndex = targetIndex + 1;
    endfor
    return target;
endfunction
*/

