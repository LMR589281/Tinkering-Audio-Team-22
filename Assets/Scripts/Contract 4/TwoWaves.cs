using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
//Base code provided from the first workshop
//Code for button from https://docs.unity3d.com/530/Documentation/ScriptReference/UI.Button-onClick.html
//Code by Luke Ryan
public class TwoWaves : MonoBehaviour
{
    //declares all the variables needed
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public Button yourButton;

    //this function is called once when the program is opened 
    //the function makes a button and then generates two waves and combines them
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClipCombine(1500);
    }

    //this function is played when the button is pressed
    //the funtion plays the audio the was created in the start function
    void TaskOnClick()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    //this function is called during the start function
    //the function generates two sign waves and then add the samples together
    private AudioClip CreateToneAudioClipCombine(int frequency)
    {
        int sampleDurationSecs = 1;     //length of the sound
        int sampleRate = 44100;     //sample rate per second
        int sampleLength = sampleRate * sampleDurationSecs;     //total samples 
        float maxValue = 1f / 4f;       //max values of the samples

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);       //the audio clip

        float[] samples = new float[sampleLength];      //array of floats for the samples
        for (var i = 0; i < sampleLength; i++)      //loop to get all the samples
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));//first wave
            float v = s * maxValue;     //lowering the max value of the sample
            samples[i] = v;     //adding sample to the array

            s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 0.75f) * ((float)i / (float)sampleRate));//second wave
            v = s * maxValue;       //lowering the max value of the sample
            samples[i] += v;        //adding the second sample to the samples array
        }

        audioClip.SetData(samples, 0);
        return audioClip;       //returns the audio clip
    }
}