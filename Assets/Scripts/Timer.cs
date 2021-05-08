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
    }

    // Update is called once per frame
    void Update()
    {
        if (finnished)
        {
            return;
        }
        float tt = 5400 - Time.time * 30f - _startTime;
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
