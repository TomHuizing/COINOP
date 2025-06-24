using System;
using Gameplay.Time;
using TMPro;
using UnityEngine;

namespace UI.Time
{
    public class ClockController : MonoBehaviour
    {
        [SerializeField] private GameClock clock;
        [SerializeField] private TextMeshProUGUI timeText;

        void Start()
        {
            clock.Start();
        }

        void OnEnable()
        {
            clock.OnTick += Tick;
            timeText.text = clock.Now.ToString("HH:mm");
        }

        void OnDisable()
        {
            clock.OnTick -= Tick;
        }

        public void Tick(DateTime now, TimeSpan period) => timeText.text = now.ToString("HH:mm");
    }
}
