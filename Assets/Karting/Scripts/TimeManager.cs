﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{ 
    public bool IsFinite { get; private set; }
    public float TotalTime { get; private set; }
    public float TimeRemaining { get; private set; }
    public bool IsOver { get; private set; }

    private bool raceStarted;

    public static Action<float> OnAdjustTime;
    public static Action<int, bool, GameMode> OnSetTime;

	public static float timeElapsed =0;
	public static float totalTimeElapsed;


    private void Awake()
    {
        IsFinite = false;
        TimeRemaining = TotalTime;
    }


    void OnEnable()
    {
        OnAdjustTime += AdjustTime;
        OnSetTime += SetTime;
    }

    private void OnDisable()
    {
        OnAdjustTime -= AdjustTime;
        OnSetTime -= SetTime;
    }

    private void AdjustTime(float delta)
    {
        TimeRemaining += delta;
    }

    private void SetTime(int time, bool isFinite, GameMode gameMode)
    {
        TotalTime = time;
        IsFinite = isFinite;
        TimeRemaining = TotalTime;
    }

    void Update()
    {
		if(!IsOver){
			
			timeElapsed += Time.deltaTime;

		} else {
			totalTimeElapsed = timeElapsed;
			//Debug.Log("ended");

		}
		
        if (!raceStarted) return;
        
        if (IsFinite && !IsOver)
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                IsOver = true;
            }
        }
    }

    public void StartRace()
    {
        raceStarted = true;
    }

    public void StopRace() {
        raceStarted = false;
    }
}

