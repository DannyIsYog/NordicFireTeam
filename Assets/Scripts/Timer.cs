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

    public GameObject EndingScreen;

    public GameObject scoreText;

    public GameObject ScoreManager;
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
            EndingScreen.SetActive(true);
          scoreText.GetComponent<TextMeshProUGUI>().text = "" + ScoreManager.GetComponent<ScoreManager>().getScore();
            
        }
        float tt = 5400 - (Time.time - _startTime) * 22.5f;
        string minutes = ((int)tt / 60).ToString();
        string seconds = ((int)tt % 60).ToString();

        timerText.text = minutes + ":" + seconds;

        if (tt <= 0f) {
            finnished = true;
        }
    }

    public void End()
    {
        timerText.color = Color.red;
        finnished = true;
    }
}
