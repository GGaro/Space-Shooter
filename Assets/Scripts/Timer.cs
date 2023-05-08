using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Timer : MonoBehaviour
{
    [SerializeField] Text TimerText;
    [SerializeField] float timePassed;
    bool keepTime = false;

    void OnEnable()
    {
        EventManager.onStartGame += startTimer;
        EventManager.onPlayerDeath += stopTimer;
    }

    void OnDisable()
    {
        EventManager.onStartGame -= startTimer;
        EventManager.onPlayerDeath -= stopTimer;
    }

    void Update()
    {
        if (keepTime)
        {
            timePassed += Time.deltaTime;
            updateTimerDisplay();
        }
    }
    void startTimer()
    {
        timePassed = 0;
        keepTime = true;
    }

    void stopTimer()
    {
        keepTime = false;
    }

    void updateTimerDisplay()
    {
        int minutes;
        float seconds;

        minutes = Mathf.FloorToInt(timePassed / 60);
        seconds = timePassed%60;

        TimerText.text = string.Format("{0}:{1:00.00}", minutes, seconds);
    }
}
