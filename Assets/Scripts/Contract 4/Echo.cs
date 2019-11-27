using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Echo : MonoBehaviour
{
    //declares all the varibles needed
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(1500);
        outAudioClip = echo_maker(outAudioClip);
    }

    void TaskOnClick()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    // Public APIs

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
 
    private AudioClip echo_maker(AudioClip audioClip)
    {
        float[] samples = new float[outAudioClip.samples];  
        outAudioClip.GetData(samples, 0);  

        float[] copy = samples;//copys the array of samples
        int offset = samples.Length / 6;//sets the off set for the echo

        for (int i = 0; i < samples.Length; i++)//loops to add the echo to the copy array
        {
            float echo = samples[i] * 0.6f;//creates the echo
            if (i + offset < samples.Length)//stops the echo being added to a point not in the list 
            {
                copy[i + offset] += echo;//adds the echo to the array
            }
        }

        float[] newEcho = copy;
        outAudioClip.SetData(newEcho, 0);   
        return audioClip;//returns the music clip
    }

}