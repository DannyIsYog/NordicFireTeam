using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Groovy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public SpriteRenderer Image;
    [SerializeField] public AudioClip[] _clipsTV;
    [SerializeField] public AudioSource _audioTV;
    [SerializeField] public AudioSource _audioMusic;
    [SerializeField] public Animator _animatorMusic;
    public PubManager Pubs;

    void Start()
    {
        _audioTV.clip = _clipsTV[0];
        _audioTV.Play();

        Pubs.TVOn += TurnTVOn;
        Pubs.TVOff += TurnTVOff;
    }

    void Update()
    {

    }

    void TurnTVOn()
    {
        _animatorMusic.SetTrigger("Switch");
        _audioTV.clip = _clipsTV[0];
        _audioTV.Play();
    }

    void TurnTVOff()
    {
        _animatorMusic.SetTrigger("Switch");
        _audioTV.clip = _clipsTV[1];
        _audioTV.Play();
    }


}
