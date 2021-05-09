using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator WarningAnim;
    private PubManager _pubs;
    private int phase;
    void Start()
    {
        phase = 0;
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
        if(phase > 1)
        {
            return;
        }
        WarningAnim.SetBool("TVFixed", false);
        phase += 1;
    }
    void StopWarn()
    {
        WarningAnim.SetBool("TVFixed", true);
    }
}
