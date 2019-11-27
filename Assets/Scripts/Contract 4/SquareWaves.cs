using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SquareWaves : MonoBehaviour
{
    public Button yourButton;
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(1500);
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
            float x = 2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate);
            float s = (4 * Mathf.Sin(x)) / Mathf.PI + (4 * Mathf.Sin(3 * x)) / (3 * Mathf.PI) + (4 * Mathf.Sin(5 * x)) / (5 * Mathf.PI) + (7 * Mathf.Sin(3 * x)) / (7 * Mathf.PI);
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}