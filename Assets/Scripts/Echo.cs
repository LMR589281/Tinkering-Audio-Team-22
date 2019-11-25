using System.Collections;
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
        outAudioClip = CreateToneAudioClipAscending(tone);//tone, 1500
        float[] samples = new float[outAudioClip.samples];
        outAudioClip.GetData(samples,0);
        
        Debug.Log("SAMPLE SIZE: "+samples.Length);

        float[] newEcho = echo_maker(samples);
        outAudioClip.SetData(newEcho, 0);
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

    private AudioClip CreateToneAudioClipAscending(int frequency)
    {
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;
        maxValue = maxValue / 2.0f; // testing stuff with echoes

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];

        for (var i = 0; i < (int)sampleLength/3; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
            //print(samples);
        }

        for (var i = (int)sampleLength / 3; i < (int)(sampleLength / 3)*2; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency*1.5f) * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
            //print(samples);
        }

        for (var i = (int)(sampleLength / 3) * 2; i < (int)sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 2.0f) * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
            //print(samples);
        }

        audioClip.SetData(samples, 0);
        return audioClip;
        //return samples;
    }


    private float[] echo_maker(float[] music_array) {

        float[] copy = music_array;
        int offset = music_array.Length / 6;

        for (int i=0; i<music_array.Length; i++)
        {
            float echo = music_array[i] * 1.5f;//0.6f;
            //float combo = getSampleValueAt(music_array[i], i)+echo;
            if(i+ offset < music_array.Length)
            {
                copy[i + offset] += echo;
            }
            
            //setSampleValueAt(music_array[i], i, combo);
        
        }

        return copy;
    }


#if UNITY_EDITOR
    //[Button("Save Wav file")]
    private void SaveWavFile() {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = CreateToneAudioClip(1500);
        SaveWavUtil.Save(path, audioClip);
    }
#endif
}