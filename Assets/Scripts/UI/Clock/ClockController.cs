using System;
using TMPro;
using UnityEngine;

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

    public void Tick(TimeSpan period) => timeText.text = clock.Now.ToString("HH:mm");
}
