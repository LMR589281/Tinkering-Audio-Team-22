using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
//Base code provided from the first workshop
//Code for button from https://docs.unity3d.com/530/Documentation/ScriptReference/UI.Button-onClick.html
public class Normal : MonoBehaviour
{
    //creates all the variables
    public Button yourButton;
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    //this function is loaded when the program is first loaded
    //the function creates the button and also generates the audio clip
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(1500);
    }

    //this function is activated when its button is click
    //the function just plays the created audio source
    void TaskOnClick()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    //this function is loaded when the tone is being created
    //the function takes in a tone and then creates an array of samples baised on a sine wave
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
}

