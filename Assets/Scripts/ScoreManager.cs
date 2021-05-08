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
    private bool finnished = false;
    public PlayerController _player;
    public Animator _animator;

    void Start()
    {
        _score = 0;
        _player.Delivery += Score;
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Score: " + _score;
    }

    public void Score()
    {
        _animator.SetTrigger("Score");
        _score += 1;
        finnished = true;
    }
}
