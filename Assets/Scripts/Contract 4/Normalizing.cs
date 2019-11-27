using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Normalizing : MonoBehaviour
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

        float[] samples = new float[outAudioClip.samples];
        outAudioClip.GetData(samples, 0);
        float[] Normalized = normalizer(samples);

        outAudioClip.SetData(Normalized, 0); 
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
    private float[] normalizer(float[] sample_array)
    {

        float largest_sample = 0;
        //float[] copy = sample_array;

        for (int i = 0; i < sample_array.Length; i++)
        {
            if (largest_sample < sample_array[i])
            {
                largest_sample = sample_array[i];
            }
        }
        float amplification = 32767.0f / largest_sample;

        for (int i = 0; i < sample_array.Length; i++)
        {
            float new_sample = amplification * sample_array[i];
            sample_array[i] = new_sample;
        }
        return sample_array;
    }
}
/*
function normalize(sound):
    largest = 0;
    for s in getSamples(sound) do:
        largest = max(largest, getSampleValue(s));
    endfor

    amplification = 32767.0 / largest;

    for s in getSamples(sound) do:
        louder = amplification * getSampleValue(s);
        setSampleValue(s, louder);
endfor
*/
