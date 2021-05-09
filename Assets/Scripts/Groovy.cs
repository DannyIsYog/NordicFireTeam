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
    [SerializeField] public AudioClip[] _clipsCrowd;
    [SerializeField] public AudioClip[] _clipsMusic;

    [SerializeField] public AudioSource _audioTV;
    [SerializeField] public AudioSource _audioPlayer;
    [SerializeField] public AudioSource _audioMusic;
    [SerializeField] public AudioSource _audioCrowd;

    [SerializeField] public Animator _animatorMusic;
    public PubManager Pubs;
    private PlayerController _player;

    void Start()
    {
        _audioTV.clip = _clipsTV[0];
        PlayNextSong();
        _audioTV.Play();

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        Pubs.TVOn += TurnTVOn;
        Pubs.TVOff += TurnTVOff;
        _player.Delivery += Score;
        _player.Pickup += PickupBeer;
        _player.Fall += Fall;
    }

    void Update()
    {

    }

    void TurnTVOn()
    {
        _animatorMusic.SetTrigger("Switch");
        _audioTV.clip = _clipsTV[0];
        _audioTV.Play();
        Cheering();
    }

    void TurnTVOff()
    {
        _animatorMusic.SetTrigger("Switch");
        _audioTV.clip = _clipsTV[1];
        _audioTV.Play();
        Booing();
    }

    void Score()
    {
        _audioPlayer.clip = _clipsPlayer[1];
        _audioPlayer.Play();
    }

    void PickupBeer()
    {
        _audioPlayer.clip = _clipsPlayer[0];
        _audioPlayer.Play();
    }
    void Cheering()
    {
        _audioCrowd.clip = _clipsCrowd[0];
        _audioCrowd.Play();
    }

    void Booing()
    {
        _audioCrowd.clip = _clipsCrowd[1];
        _audioCrowd.Play();
    }

    void Fall()
    {
        _audioPlayer.clip = _clipsPlayer[2];
        _audioPlayer.Play();
    }

    void PlayNextSong()
    {
        _audioMusic.clip = _clipsMusic[UnityEngine.Random.Range(0, _clipsMusic.Length)];
        _audioMusic.Play();
        Invoke("PlayNextSong", _audioMusic.clip.length);
    }
}