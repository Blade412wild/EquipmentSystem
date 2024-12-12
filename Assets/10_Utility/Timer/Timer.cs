using System;
using UnityEngine;

[Serializable]
public class Timer
{
    public event Action OnTimerIsDone;
    public event Action<Timer> OnRemoveTimer;

    // timer 
    private float startTime = 0;
    public float currentTime;
    private float endTime;
    private bool repeat = false;
    private int repeatAmount = 0;
    private int currentAmount = 1;

    public Timer(float _seconds)
    {
        endTime = _seconds;
        TimerManager.Instance.AddTimerToList(this);
    }

    public Timer(float _seconds, bool _repeat)
    {
        endTime = _seconds;
        repeat = _repeat;
    }

    public Timer(float _seconds, bool _repeat, int _amount)
    {
        endTime = _seconds;
        repeat = _repeat;
        repeatAmount = _amount;
    }



    // Update is called once per frame
    public void OnUpdate()
    {
        currentTime += Time.deltaTime;
        //Debug.Log(currentTime);
        RunTimer();
    }

    private void RunTimer()
    {
        if (currentTime >= endTime)
        {
            //Debug.Log(" Timer is finished, [" + endTime + "] have past");
            if (repeat == true && currentAmount < repeatAmount)
            {
                var t = Time.time;
                currentAmount++;
                ResetTimer();
                OnTimerIsDone?.Invoke();
            }
            else
            {
                OnTimerIsDone?.Invoke();
                ResetTimer();
                OnRemoveTimer?.Invoke(this);
            }
        }
    }

    public void ResetTimer()
    {
        currentTime = startTime;
    }
}
