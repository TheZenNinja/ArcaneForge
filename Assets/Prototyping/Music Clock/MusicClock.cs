using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class MusicClock
{
    public static Action whole;
    public static Action half;
    public static Action quarter;

    public static SongData song;
    public static bool songDone;

    public static IEnumerator Tick()
    {
        int beat = 0;
        while (!songDone)
        {
            beat++;
            if (beat > 4)
                beat = 1;

            if (beat == 2)
                half?.Invoke();
            else if (beat == 4)
            { 
                half?.Invoke();
                whole?.Invoke();
            }

            quarter?.Invoke();

            yield return new WaitForSeconds(song.meaureLengthInSeconds / 4);
        }

    }
}
