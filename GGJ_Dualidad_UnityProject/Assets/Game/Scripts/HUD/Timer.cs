using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private static Timer _instance;
    public static Timer Instance
    {
        get
        {
            return _instance;
        }
    }

    public TMP_Text _timerText;
    public string timeFormat = @"hh\:mm\:ss";

    private bool _isActive;
    private TimeSpan _timer;
    private TimeSpan _maxTime = new TimeSpan(days: 0, hours: 23, minutes: 59, seconds: 59, milliseconds: 59);
    private float _elapsedSeconds;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }

    public void StartTimer()
    {
        _isActive = true;
    }

    public void PauseTimer()
    {
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            _elapsedSeconds += Time.deltaTime;
            _timer = TimeSpan.FromSeconds(_elapsedSeconds);
            Display();
        }
    }

    private void Display()
    {
        if(_timer.TotalHours >= 24d)
        {
            _timer = _maxTime;
        }
        _timerText.text = _timer.Hours > 0 ? String.Format("{0:00}:", Math.Floor(_timer.TotalHours)) + _timer.ToString(timeFormat) : _timer.ToString(timeFormat);
    }
}
