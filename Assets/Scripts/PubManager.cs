using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PubManager : MonoBehaviour
{
    [SerializeField] public float RandomMax;
    [SerializeField] public float RandomMin;
    [SerializeField] public ClientController[] Clients;
    [SerializeField] public Places[] places;

    public ClientController CurrentClient;
    private int _total;
    public bool TV;
    private float _randomNum;
    private int _randomNumClient;

    #region Events
    public event Action TVOn;
    public event Action TVOff;

    public event Action NewClient;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        TV = true;
        _randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);

        _total = Clients.Length;
        SortCustomers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime >= _randomNum)
        {
            SwapMusicState();
        }
    }

    private void SortCustomers()
    {
        Debug.Log(_total);
        for (int i = 0; i < _total; i++)
        {
            Places temp = places[i];
            int randomIndex = UnityEngine.Random.Range(i, _total);
            places[i] = places[randomIndex];
            places[randomIndex] = temp;
            Debug.Log(places[i]._id);
        }
    }

    void SwapMusicState()
    {
        if (TV)
        {
            TVOff?.Invoke();
            TV = false;
            _randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
            _randomNum += Time.unscaledTime;

        }
        else
        {
            SortCustomers();
            TVOn?.Invoke();
            TV = true;
            _randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
            _randomNum += Time.unscaledTime;
        }
    }

    void ChooseNextClient()
    {
        _randomNumClient = UnityEngine.Random.Range(0, _total);
        CurrentClient = Clients[_randomNumClient];
        NewClient?.Invoke();
    }
}
