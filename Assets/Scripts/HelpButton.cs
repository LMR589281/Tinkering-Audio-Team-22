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

public class HelpButton : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClipAscending(1500);
        //PlayOutAudio();
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        PlayOutAudio();
        SceneManager.LoadScene(1);
    }



        private AudioSource audioSource;
        private AudioClip outAudioClip;
        public int sampleDurationSecs = 5;//length, 5
        public int tone = 1500;//tone, 1500

    // Public APIs
    public void PlayOutAudio()
        {
            audioSource.PlayOneShot(outAudioClip);
        }


        public void StopAudio()
        {
            audioSource.Stop();
        }


    private AudioClip CreateToneAudioClipAscending(int frequency)
    {
        int sampleRate = 44100;//sample rate per second
        int sampleLength = sampleRate * sampleDurationSecs;//total samples 
        float maxValue = 1f / 4f;//max values of the samples

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);//the audio clip

        float[] samples = new float[sampleLength];//array of floats for the samples
        //loop to get all the samples
        for (var i = 0; i < (int)sampleLength / 3; i++)//the audio clip is split into 3 parts to make a ascending sound
        {//first third 
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));//sample from the sine wave
            float v = s * maxValue;//lowering the max value of the sample
            samples[i] = v;//adding the sample to the array
        }

        for (var i = (int)sampleLength / 3; i < (int)(sampleLength / 3) * 2; i++)//second third
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 1.5f) * ((float)i / (float)sampleRate));//sample from the sine wave
            float v = s * maxValue;//lowering the max value of the sample
            samples[i] = v;//adding the sample to the array
        }

        for (var i = (int)(sampleLength / 3) * 2; i < (int)sampleLength; i++)//last third
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 2.0f) * ((float)i / (float)sampleRate));//sample from the sine wave
            float v = s * maxValue;//lowering the max value of the sample
            samples[i] = v;//adding the sample to the array
        }

        audioClip.SetData(samples, 0);
        return audioClip;//returns the audio clip
    }


#if UNITY_EDITOR
    //[Button("Save Wav file")]
    private void SaveWavFile()
        {
            string path = EditorUtility.SaveFilePanel("Where do you want the wav file to go?", "", "", "wav");
            var audioClip = CreateToneAudioClipAscending(1500);
            SaveWavUtil.Save(path, audioClip);
        }
#endif
    }

