using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenEffects : MonoBehaviour
{
    public bool breathing = false;
    public bool rotation = false;
    void Start()
    {
        if (breathing)
        {
            LeanTween.scale(gameObject, new Vector3(1f, 1.025f, 1f), Random.Range(0.6f, 0.8f)).setLoopType(LeanTweenType.pingPong).setDelay(Random.Range(0.05f, 0.1f));
        }

        if(rotation)
        {
            LeanTween.rotateAround(gameObject, Vector3.forward, 3, 1f).setLoopType(LeanTweenType.pingPong).setDelay(Random.Range(0.1f, 0.3f));
        }
    }
}
