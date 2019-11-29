using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
//Base code provided from the first workshop
//Code for button from https://docs.unity3d.com/530/Documentation/ScriptReference/UI.Button-onClick.html
//Code by Luke Ryan
public class Descending : MonoBehaviour
{
    //declares all the variables needed
    public Button yourButton;
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    //this function is called once when the program is opened 
    //the function makes a button and then generates a sine wave with a descending tone
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClipAscending(1500);
    }

    //this function is activated when its button is click
    //the function just plays the created audio source
    void TaskOnClick()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    //this function is called during the start function
    //the function creates a audio clip with a descending tone
    private AudioClip CreateToneAudioClipAscending(int frequency)
    {
        int sampleDurationSecs = 5;
        int sampleRate = 44100;     //sample rate per second
        int sampleLength = sampleRate * sampleDurationSecs;     //total samples 
        float maxValue = 1f / 4f;       //max values of the samples

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);       //the audio clip

        float[] samples = new float[sampleLength];      //array of floats for the samples
        //loop to get all the samples
        for (var i = 0; i < (int)sampleLength / 3; i++)     //the audio clip is split into 3 parts to make a descending sound
        {//first third 
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));      //sample from the sine wave
            float v = s * maxValue;     //lowering the max value of the sample
            samples[i] = v;     //adding the sample to the array
        }

        for (var i = (int)sampleLength / 3; i < (int)(sampleLength / 3) * 2; i++)//second third
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 0.6f) * ((float)i / (float)sampleRate));     //sample from the sine wave frequency decreased by 40%
            float v = s * maxValue;     //lowering the max value of the sample
            samples[i] = v;     //adding the sample to the array
        }

        for (var i = (int)(sampleLength / 3) * 2; i < (int)sampleLength; i++)//last third
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 0.3f) * ((float)i / (float)sampleRate));     //sample from the sine wave frequency decreased by 30%
            float v = s * maxValue;     //lowering the max value of the sample
            samples[i] = v;     //adding the sample to the array
        }

        audioClip.SetData(samples, 0);
        return audioClip;       //returns the audio clip
    }
}
