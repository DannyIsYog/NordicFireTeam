using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator WarningAnim;
    private PubManager _pubs;
    void Start()
    {
        _pubs = GameObject.FindGameObjectWithTag("PubManager").GetComponent<PubManager>();
        _pubs.TVOff += Warn;
        _pubs.TVOn += StopWarn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Warn()
    {
        WarningAnim.SetBool("TVFixed", false);
    }
    void StopWarn()
    {
        WarningAnim.SetBool("TVFixed", true);
    }
}
