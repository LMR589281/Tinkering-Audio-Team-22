using System.Collections;
using System.Collections.Generic;
//using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class buttonSound : MonoBehaviour
{
    //this function updates every frame 
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())//checks if the player is over the mouse button
        {
            Debug.Log("play sound");//prints play sound
            PlayOutAudio();//outputs the sound
        }
    }

    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public int sampleDurationSecs = 5;//length, 5
    public int tone = 1500;//tone, 1500


    //this function is called when the unity is put into play mode
    void Start()
    {
        audioSource = GetComponent<AudioSource>();//creates the variable for the audio source 
        outAudioClip = CreateToneAudioClipAscending(tone);//tone, 1500

        float[] samples = new float[outAudioClip.samples];//this set of code takes the float array out of the audio clip
        outAudioClip.GetData(samples, 0);//so that it can be passed into the echo function
        Debug.Log("SAMPLE SIZE: " + samples.Length);//as the echo function only takes in a array of floats
        float[] newEcho = echo_maker(samples);

        outAudioClip.SetData(newEcho, 0);   //adds the echo
        //PlayOutAudio(); audio source is played when the user press the button
    }


    // this function plays the audio clip
    public void PlayOutAudio()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    //this function stops playing the audio clip
    public void StopAudio()
    {
        audioSource.Stop();
    }


    //this function creates a audio clip
    private AudioClip CreateToneAudioClip(int frequency)
    {
        int sampleDurationSecs = 1;//length of the sound
        int sampleRate = 44100;//sample rate per second
        int sampleLength = sampleRate * sampleDurationSecs;//total samples 
        float maxValue = 1f / 4f;//max values of the samples

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);//the audio clip

        float[] samples = new float[sampleLength];//array of floats for the samples
        for (var i = 0; i < sampleLength; i++)//loop to get all the samples
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));//sample from the sine wave
            float v = s * maxValue;//lowering the max value of the sample
            samples[i] = v;//adding the sample to the array
        }

        audioClip.SetData(samples, 0);
        return audioClip;//returning the audio clip 
    }
    //this function creates a ascending audio clip
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
    //this function combines two waves together
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

