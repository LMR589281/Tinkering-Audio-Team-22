using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doubling : MonoBehaviour
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
function double (source):
    len = getLength(source) / 2 + 1;
    target = makeEmptySound(len);
    targetIndex = 0;
    for sourceIndex in range(0, getLength(source), 2) do:
        value = getSampleValueAt(source, sourceIndex);
        setSampleValueAt(target, targetIndex, value);
        targetIndex = targetIndex + 1;
    endfor
    return target;
endfunction
*/


