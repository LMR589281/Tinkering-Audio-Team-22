using System.Collections;
using UnityEngine;
public class PlaySoundOnKeypress : MonoBehaviour
{
    public AudioSource Jump;
    public AudioSource FlyWalk;
    public AudioSource Quack;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            FlyWalk.Play();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump.Play();
        if (Input.GetKeyDown(KeyCode.Q))
            Quack.Play();

    }
}
