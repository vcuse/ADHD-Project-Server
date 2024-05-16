using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using MixedReality.Toolkit.Examples;

public class Timer : MonoBehaviour
{
    public TestMouse mouseMovementTrack;

    public LogRawInput loggerFile;

    // These timers are only active when their specific actions occur
    private bool posTimerActive;
    private bool gazeTimerActive;

    // These timers are used to track the TOTAL time
    private float totalKBTime;
    private float totalMouseTime;
    private float totalGazeTime;
    private float totalPosTime;

    // Separate timers used to synchronize text to speech
    private float currentKBTime;
    private float currentMouseTime;
    private float currentGazeTime;
    private float currentPosTime;

    private Vector2Int mousePosition;

    [SerializeField] private TMP_Text KBTimer;
    [SerializeField] private TMP_Text MouseTimer;
    [SerializeField] private TMP_Text GazeTimer;
    [SerializeField] private TMP_Text PosTimer;
    [SerializeField] private TextToSpeechHandler handler;

    // Start is called before the first frame update
    void Start()
    {
        totalKBTime = 0;
        totalMouseTime = 0;
        totalGazeTime = 0;
        totalPosTime = 0;

        currentMouseTime = 0;
        currentKBTime = 0;
        currentGazeTime = 0;

        mousePosition = new Vector2Int(0,0);

        posTimerActive = false;
        gazeTimerActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentKBTime > 120)
        {
            handler.Speak();
            currentKBTime = 0;
            currentMouseTime = 0;
        }

        if(currentMouseTime > 120)
        {
            handler.Speak();
            currentMouseTime = 0;
            currentKBTime = 0;
        }

        if(gazeTimerActive)
        {
            currentGazeTime += Time.deltaTime;
            totalGazeTime += Time.deltaTime;
        }

        if(posTimerActive)
        {
            currentPosTime += Time.deltaTime;
            totalPosTime += Time.deltaTime;
        }

        Vector2Int currPos = TestMouse.mousePosition;

        // Checking if key is pressed or if current mouse position does not align with previous
        if(loggerFile.isMouseKeyPressed || currPos != mousePosition)
        {
            totalMouseTime = 0;
            currentMouseTime = 0;
            currentKBTime = 0;
        }
        else
        {
            currentMouseTime = currentMouseTime + Time.deltaTime;
            totalMouseTime = totalMouseTime + Time.deltaTime;
        }

        if(loggerFile.isKeyboardKeyPressed)
        {
            totalKBTime = 0;
            currentKBTime = 0;
            currentMouseTime = 0;
        }
        else
        {
            currentKBTime = currentKBTime + Time.deltaTime;
            totalKBTime = totalKBTime + Time.deltaTime;
        }

        // Display the time with two decimal places
        KBTimer.text = totalKBTime.ToString("F2");
        MouseTimer.text = totalMouseTime.ToString("F2");
        GazeTimer.SetText(totalGazeTime.ToString("F2"));
        PosTimer.SetText(totalPosTime.ToString("F2"));

        loggerFile.setKeyUnpressed();
        //setting new mouse position
        mousePosition = TestMouse.mousePosition;
    }

    //call this from another script to get the time since the last mouse movement
    public float GetMouseTime(){
        return totalMouseTime;
    }

    public float GetKBTime() {
        return totalKBTime;
    }

    // Activate the gaze timer
    public void activateGazeTimer()
    {
        gazeTimerActive = true;
    }

    public void deactivateGazeTimer()
    {
        gazeTimerActive = false;
    }
    
    public Boolean testGaze()
    {
        if (currentGazeTime > 5)
        {
            currentGazeTime = 0;
            return true;
        }
        return false;
    }

    public void resetGazeTimers()
    {
        gazeTimerActive = false;
        currentGazeTime = 0;
        totalGazeTime = 0;
    }

    public void activatePosTimer()
    {
        posTimerActive = true;
    }

    public void deactivatPosTimer()
    {
        posTimerActive = false;
    }

    public Boolean testPos()
    {
        if (currentPosTime > 5)
        {
            currentPosTime = 0;
            return true;
        }
        return false;
    }

    public void resetPosTimers()
    {
        posTimerActive = false;
        currentPosTime = 0;
        totalPosTime = 0;
    }
}
