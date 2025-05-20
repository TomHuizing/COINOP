using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float TimeCompression = 60f;
    public DateTime StartTime;

    public DateTime CurrentTime { get; private set;}
    public TimeSpan DeltaTime { get; private set; } = TimeSpan.Zero;

    public bool IsPaused { get; set; } = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsPaused)
        {
            DeltaTime = TimeSpan.FromSeconds(Time.deltaTime * TimeCompression);
            CurrentTime += DeltaTime;
        }
    }
}
