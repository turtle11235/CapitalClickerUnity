using System.Collections;
using UnityEngine;

public class Clock
{
    public static readonly float IRLToInGameTimeRatio = 60 * 60;
    public static float ElapsedSecondsIRL
    {
        get { return Counter.ElapsedSeconds; }
    }

    public static float ElapsedMinutesIRL
    {
        get { return Counter.ElapsedSeconds / 60; }
    }

    public static float ElapsedHoursIRL
    {
        get { return ElapsedMinutesIRL / 60; }
    }

    public static float ElapsedSecondsInGame
    {
        get { return ElapsedSecondsIRL * IRLToInGameTimeRatio; }
    }

    public static float ElapsedMinutesInGame
    {
        get { return ElapsedMinutesIRL * IRLToInGameTimeRatio; }
    }

    public static float ElapsedHoursInGame
    {
        get { return ElapsedHoursIRL * IRLToInGameTimeRatio; }
    }

    public static float ElapsedDaysInGame
    {
        get { return ElapsedHoursInGame / 24; }
    }

    public static string InGameTimeFormatted
    {
        get 
        { 
            string hours = (ElapsedHoursInGame >= 1 ? Mathf.FloorToInt(ElapsedHoursInGame % 24) : 0).ToString("D2");
            string minutes = (ElapsedMinutesInGame >= 1 ? Mathf.FloorToInt(ElapsedMinutesInGame % 60) : 0).ToString("D2");
            return $"{hours}:{minutes}";
        }
    }

    public static string FormatTime(float days, float hours, float minutes, float seconds)
    {
        return "";
    }

}