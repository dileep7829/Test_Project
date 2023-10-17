using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Singleton Event Manager Class for ease of Use
/// </summary>
public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public EventHandler<string> OnLetterClicked;
    public EventHandler<int> OnNewHighScore;
    public EventHandler<int> OnNewScore;

}
