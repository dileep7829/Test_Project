using System;
using UnityEngine;

namespace Utils
{
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

        public EventHandler OnButtonClicked;
        public EventHandler OnItemRemoved;
    }
}
