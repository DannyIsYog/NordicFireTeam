using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float _score;
    public PlayerController _player;
    public Animator _animatorPlus1;
    public Animator _animatorLess1;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _score = 0;
        _player.Delivery += IncreaseScore;
        _player.FailedDelivery += DecreaseScore;
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Score: " + _score;
    }

    public void IncreaseScore()
    {
        _animatorPlus1.SetTrigger("Score");
        _score += 1;
    }

    public void DecreaseScore()
    {
        _animatorLess1.SetTrigger("Score");
        _score -= 1;
    }
}
