using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halving : MonoBehaviour
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
function half(source):
    target = makeEmptySound(getLength(source) * 2);
    sourceIndex = 0;
    for targetIndex in range(0, getLength( target)):
        value = getSampleValueAt( source, int(sourceIndex));
        setSampleValueAt( target, targetIndex, value);
        sourceIndex = sourceIndex + 0.5;
    endfor
    return target;
endfunction

*/
