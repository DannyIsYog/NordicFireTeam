using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Groovy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public float RandomMax;
    [SerializeField] public float RandomMin;
    [SerializeField] public SpriteRenderer Image;
    [SerializeField] public AudioClip[] _clipsTV;
    [SerializeField] public AudioSource _audioTV;
    [SerializeField] public AudioSource _audioMusic;
    private float RandomNum;
    public bool TV;

    public event Action TVOn;
    public event Action TVOff;

    void Start()
    {
        TV = true;
       RandomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
        _audioTV.clip = _clipsTV[0];
        _audioTV.Play();
        _audioMusic.mute = true;
    }

    void Update()
    {
        if(Time.unscaledTime >= RandomNum)
        {
            SwapMusicState();
        }
    }

    void SwapMusicState()
    {
        if (TV)
        {
            TVOn?.Invoke();
            TV = false;

            RandomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
            RandomNum += Time.unscaledTime;
            _audioMusic.mute = false;
            _audioTV.clip = _clipsTV[1];
            _audioTV.Play();

        }
        else
        {
            TVOff?.Invoke();
            TV = true;

            RandomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
            RandomNum += Time.unscaledTime;
            _audioMusic.mute = true;
            _audioTV.clip = _clipsTV[0];
            _audioTV.Play();
        }
    }


}
