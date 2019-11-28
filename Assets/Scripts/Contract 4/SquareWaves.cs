using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SquareWaves : MonoBehaviour
{
    //declares all the variables needed
    public Button yourButton;
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    //this function is called once when the program is opened 
    //the function makes a button and then generates a square wave
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(1500);
    }

    //this function is played when the button is pressed
    //the funtion plays the audio the was created in the start function
    void TaskOnClick()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    //this function is called during the start function
    //the function generates many sine waves and combines then to make a square like wave
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
            float x = 2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate);     //used a formula from https://www.desmos.com/calculator/6ihiapwpgw for the square wave
            float s = (4 * Mathf.Sin(x)) / Mathf.PI + (4 * Mathf.Sin(3 * x)) / (3 * Mathf.PI) + (4 * Mathf.Sin(5 * x)) / (5 * Mathf.PI) + (7 * Mathf.Sin(3 * x)) / (7 * Mathf.PI);
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}