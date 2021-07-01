using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class TogglePauseBehaviour : MonoBehaviour
{
    private double lastPauseBegin        = -1.0;
    private double totalPauseTimeSeconds = 0.0;

    private const string REPORT_TOTAL_TIME_PLAYED_SECONDS_EVENT_NAME = "TotalTimePlayedSeconds";
    private const string REPORT_TOTAL_TIME_PAUSED_SECONDS_EVENT_NAME = "TotalTimePausedSeconds";

    public void OnGameOver()
    {
        Debug.Log("TogglePauseBehaviour.OnGameOver()");
        ReportTime(REPORT_TOTAL_TIME_PLAYED_SECONDS_EVENT_NAME, Time.realtimeSinceStartup);
        ReportTime(REPORT_TOTAL_TIME_PAUSED_SECONDS_EVENT_NAME, totalPauseTimeSeconds);
    }

    private static void ReportTime(string eventName, double timeSeconds)
    {
        Debug.Log("TogglePauseBehaviour.ReportTime(" + eventName + ", " + timeSeconds + ")");
        Analytics.CustomEvent(
            eventName,
            new Dictionary<string, object> {
                {  "Time (seconds)", (object) timeSeconds }
            }
        );
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        lastPauseBegin = Time.realtimeSinceStartup;
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
        if (lastPauseBegin >= 0.0)
        {
            totalPauseTimeSeconds += (Time.realtimeSinceStartup - lastPauseBegin);
            lastPauseBegin         = -1.0;
        }
    }

}
