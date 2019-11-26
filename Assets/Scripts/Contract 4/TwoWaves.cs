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


public class TwoWaves : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClipCombine(1500);
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
    private AudioClip CreateToneAudioClipCombine(int frequency)
    {
        int sampleDurationSecs = 1;//length of the sound
        int sampleRate = 44100;//sample rate per second
        int sampleLength = sampleRate * sampleDurationSecs;//total samples 
        float maxValue = 1f / 4f;//max values of the samples

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);//the audio clip

        float[] samples = new float[sampleLength];//array of floats for the samples
        for (var i = 0; i < sampleLength; i++)//loop to get all the samples
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));//first wave
            float v = s * maxValue;//lowering the max value of the sample
            samples[i] = v;//adding sample to the array

            s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 0.75f) * ((float)i / (float)sampleRate));//second wave
            v = s * maxValue;//lowering the max value of the sample
            samples[i] += v;//adding the second sample to the samples array
        }

        audioClip.SetData(samples, 0);
        return audioClip;//returns the audio clip
    }


#if UNITY_EDITOR
    //[Button("Save Wav file")]
    private void SaveWavFile()
    {
        string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
        var audioClip = CreateToneAudioClipCombine(1500);
        SaveWavUtil.Save(path, audioClip);
    }
#endif
}