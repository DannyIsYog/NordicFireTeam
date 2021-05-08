using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Groovy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public SpriteRenderer Image;
    [SerializeField] public AudioClip[] _clipsTV;
    [SerializeField] public AudioClip[] _clipsPlayer;

    [SerializeField] public AudioSource _audioTV;
    [SerializeField] public AudioSource _audioPlayer;
    [SerializeField] public AudioSource _audioMusic;

    [SerializeField] public Animator _animatorMusic;
    public PubManager Pubs;
    private PlayerController _player;

    void Start()
    {
        _audioTV.clip = _clipsTV[0];
        _audioTV.Play();

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        Pubs.TVOn += TurnTVOn;
        Pubs.TVOff += TurnTVOff;
        _player.Delivery += Score;
        _player.Pickup += PickupBeer;
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

    void Score()
    {
        _audioPlayer.clip = _clipsPlayer[1];
        _audioPlayer.Play();
        Debug.Log("score");
    }

    void PickupBeer()
    {
        _audioPlayer.clip = _clipsPlayer[0];
        _audioPlayer.Play();
        Debug.Log("score");
    }


}