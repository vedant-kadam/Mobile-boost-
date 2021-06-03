using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Oscillations : MonoBehaviour
{
    //its the max amount of movememnt we want in any three AXIs
    [SerializeField] Vector3 Movements;
    //The Movemnt factor range is not necessary
    [Range(0f, 1f)]
    [SerializeField]
    float MoveMentFactor;
    //just is necessary to know the start position of the object
    Vector3 Startposi;
    //its the period by which we are divinding the input we are giving to the sin fiunction it will decide how fast the 
    //ossicilation will be
    [SerializeField] float period =2f;

    // Start is called before the first frame update
    void Start()
    {
        Startposi = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return;}
        //we get the cycles in a sin wave
        float cycles = Time.timeSinceLevelLoad / period;
        //just a constant tau we need for sin function  IE we have to give radian to the  sin Function
        const float Tau = Mathf.PI * 2;

        float RawSineWaveNmbers = Mathf.Sin(cycles * Tau);
        print(RawSineWaveNmbers);
        //as we get a valure between -1 amd 1 from the sin function
        //but we need a value between 1 and 0 so we divide by 2 and add 0.5
        MoveMentFactor = RawSineWaveNmbers / 2f+ 0.5f;

        Vector3 offset = MoveMentFactor * Movements;

        transform.position = Startposi + offset;

    }
}
