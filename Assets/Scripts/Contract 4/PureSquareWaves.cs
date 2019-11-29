using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
//Base code provided from the first workshop
//Code for button from https://docs.unity3d.com/530/Documentation/ScriptReference/UI.Button-onClick.html
//Code by Luke Ryan
public class PureSquareWaves : MonoBehaviour
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

    //this function is activated when its button is click
    //the function just plays the created audio source
    void TaskOnClick()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    //this function is called during the start function
    //the function generates a sine and turns the value into 1 or -1 to make a square wave
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
            if (s > 0) {        //checks if the hight of the wave is over 0  
                s = 1;      //if the hight is over 0 then the sample is set to 1
            }
            else {      //checks if the hight of the wave is not over 0 
                s = -1;     //if the hight is ont over 0 then the sample is set to -1
            }
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}