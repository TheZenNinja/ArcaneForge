using System;
using System.Collections.Generic;

[Serializable]
public class Timer
{
    public float timerLength;
    public float currentTime;
    public float percent => UnityEngine.Mathf.Clamp01(1 - currentTime / timerLength);
    public bool finished;
    public Action onEnd;
    public void Restart() => Start();
    public void Start()
    {
        currentTime = timerLength;
        finished = false;
    }
    public void Tick() => Tick(UnityEngine.Time.fixedDeltaTime);
    public void Tick(float deltaTime)
    {
        UnityEngine.Debug.Log("Tick");
        if (finished)
            return;

        currentTime -= deltaTime;
        if (currentTime <= 0)
        {
            UnityEngine.Debug.Log("Done");
            finished = true;
            currentTime = 0;
            onEnd?.Invoke();
        }
    }
}
