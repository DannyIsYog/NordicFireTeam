using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI timerText;
    private float _startTime;
    private bool finnished = false;
    void Start()
    {
        _startTime = Time.time;
        Debug.Log(_startTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (finnished)
        {
            return;
        }
        float tt = 5400 - (Time.time - _startTime) * 22.5f;
        string minutes = ((int)tt / 60).ToString();
        string seconds = ((int)tt % 60).ToString();

        timerText.text = minutes + ":" + seconds;
    }

    public void End()
    {
        timerText.color = Color.red;
        finnished = true;
    }
}
