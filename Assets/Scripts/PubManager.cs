using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PubManager : MonoBehaviour
{
    [SerializeField] public float RandomMax;
    [SerializeField] public float RandomMin;
    [SerializeField] public GameObject[] Customers;
    public int[] places;
    private int _total;
    public bool TV;
    private float _randomNum;

    #region Events
    public event Action TVOn;
    public event Action TVOff;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        TV = true;
        _randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);

        _total = Customers.Length;
        for(int i = 0; i < _total; i++)
        {
            places[i] = i;
        }
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
        for (int i = 0; i < _total; i++)
        {
            int temp = places[i];
            int randomIndex = UnityEngine.Random.Range(i, _total);
            places[i] = places[randomIndex];
            places[randomIndex] = temp;
            Debug.Log(places[i]);
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
            TVOn?.Invoke();
            TV = true;

            SortCustomers();
            _randomNum = UnityEngine.Random.Range(RandomMin, RandomMax);
            _randomNum += Time.unscaledTime;
        }
    }
}
