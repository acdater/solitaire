using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    private TimeSpan _time;
    [SerializeField] private bool _isLaunched = false;
    [SerializeField] private float _seconds = 0;
    [SerializeField] private TextMeshProUGUI m_TextMeshPro;
    [SerializeField] private string _prefix = "Time";

    private void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isLaunched == false)
        {
            return;
        }

        _seconds += Time.deltaTime;

        if (_seconds >= 1)
        {
            UpdateTimeWithSeconds(_seconds);
            _seconds = 0;
        }
    }

    private void UpdateTimeWithSeconds(float seconds)
    {
        var time = TimeSpan.FromSeconds(seconds);
        _time += time;
        m_TextMeshPro.text = $"{_prefix}: {_time.Hours}:{_time.Minutes}:{_time.Seconds}";
    }

    public void Trigger(MoveCommand moveCommand)
    {
        _isLaunched = true;
    }
}
