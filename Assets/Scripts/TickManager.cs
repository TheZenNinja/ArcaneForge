using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    private static float tickSpeed = .2f;
    public static Action tick;

    void Start() => StartCoroutine(Tick());

    private IEnumerator Tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickSpeed);
            tick?.Invoke();
        }
    }
    public static void ForceTick(int times = 1)
    {
        for (int i = 0; i < times; i++)
        {
            tick?.Invoke();
        }
    }
}
