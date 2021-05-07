using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groovy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public float RandomMax;
    [SerializeField] public float RandomMin;
    [SerializeField] public SpriteRenderer Image;
    [SerializeField] public AudioSource _audioTV;
    [SerializeField] public AudioSource _audioMusic;
    private float RandomNum;
    public bool TVOn;

    void Start()
    {
       TVOn = true;
       RandomNum =  Random.Range(RandomMin, RandomMax);
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
        if (TVOn)
        {
            TVOn = false;
            RandomNum = Random.Range(RandomMin, RandomMax);
            RandomNum += Time.unscaledTime;
            Image.color = Color.black;
        }
        else
        {
            TVOn = true;
            RandomNum = Random.Range(RandomMin, RandomMax);
            RandomNum += Time.unscaledTime;
            Image.color = Color.blue;
        }
    }


}
