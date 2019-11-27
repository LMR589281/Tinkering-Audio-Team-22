using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Desending : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public int sampleDurationSecs = 5;//length, 5
    public int tone = 1500;//tone, 1500

    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClipAscending(1500);
    }

    void TaskOnClick()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    // Public APIs

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
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 0.6f) * ((float)i / (float)sampleRate));//sample from the sine wave
            float v = s * maxValue;//lowering the max value of the sample
            samples[i] = v;//adding the sample to the array
        }

        for (var i = (int)(sampleLength / 3) * 2; i < (int)sampleLength; i++)//last third
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 0.3f) * ((float)i / (float)sampleRate));//sample from the sine wave
            float v = s * maxValue;//lowering the max value of the sample
            samples[i] = v;//adding the sample to the array
        }

        audioClip.SetData(samples, 0);
        return audioClip;//returns the audio clip
    }
}