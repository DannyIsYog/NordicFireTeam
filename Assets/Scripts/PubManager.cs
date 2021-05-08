using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubManager : MonoBehaviour
{

    [SerializeField] public GameObject[] Customers;
    public int[] places;
    public Groovy grooves;
    private int _total;

    // Start is called before the first frame update
    void Start()
    {
        _total = Customers.Length;
        for(int i = 0; i < _total; i++)
        {
            places[i] = i;
        }
        SortCustomers();
        grooves.TVOn += SortCustomers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SortCustomers()
    {
        for (int i = 0; i < _total; i++)
        {
            int temp = places[i];
            int randomIndex = Random.Range(i, _total);
            places[i] = places[randomIndex];
            places[randomIndex] = temp;
            Debug.Log(places[i]);
        }
    }
}
