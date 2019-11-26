using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normalizing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/*
function normalize(sound):
    largest = 0;
    for s in getSamples(sound) do:
        largest = max(largest, getSampleValue(s));
    endfor

    amplification = 32767.0 / largest;

    for s in getSamples(sound) do:
        louder = amplification * getSampleValue(s);
        setSampleValue(s, louder);
endfor
*/
