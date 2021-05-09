using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Circuit : MonoBehaviour
{

    public Animator CircuitAnim;
    private PubManager _pubs;
    public event Action Repairs;
    public bool fried;
    // Start is called before the first frame update
    void Start()
    {
        fried = false;
        _pubs = GameObject.FindGameObjectWithTag("PubManager").GetComponent<PubManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fry()
    {
        fried = true;
        CircuitAnim.SetBool("Fixed", false);
    }

    public void Repair()
    {
        fried = false;
        CircuitAnim.SetBool("Fixed", true);
        Repairs?.Invoke();
    }
}
