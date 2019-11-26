using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
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

    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(1500);

        float[] samples = new float[outAudioClip.samples];//this set of code takes the float array out of the audio clip
        outAudioClip.GetData(samples, 0);//so that it can be passed into the echo function
        Debug.Log("SAMPLE SIZE: " + samples.Length);//as the echo function only takes in a array of floats
        float[] newEcho = echo_maker(samples);

        outAudioClip.SetData(newEcho, 0);   //adds the echo
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
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

    private float[] echo_maker(float[] music_array)
    {

        float[] copy = music_array;//copys the array of samples
        int offset = music_array.Length / 6;//sets the off set for the echo

        for (int i = 0; i < music_array.Length; i++)//loops to add the echo to the copy array
        {
            float echo = music_array[i] * 0.6f;//creates the echo
            if (i + offset < music_array.Length)//stops the echo being added to a point not in the list 
            {
                copy[i + offset] += echo;//adds the echo to the array
            }


        }

        return copy;//returns the array
    }


#if UNITY_EDITOR
    //[Button("Save Wav file")]
    private void SaveWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = CreateToneAudioClip(1500);
        SaveWavUtil.Save(path, audioClip);
    }
#endif
}