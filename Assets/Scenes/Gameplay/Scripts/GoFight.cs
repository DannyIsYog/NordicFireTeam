using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoFight : MonoBehaviour
{
    [SerializeField] Transform fightSpot;  //get this from pub manager
    [SerializeField] Transform sit;  //get this from pub manager
    
    private NavMeshAgent agent;

    private bool fight = true; //get this from pub manager
    
    // Start is called before the first frame update
    void Start()
    {
        fightSpot = GameObject.FindGameObjectWithTag("FightingPoint").transform;
        sit = GameObject.FindGameObjectWithTag("Sit").transform;

        agent = this.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fight == true)
            agent.SetDestination(fightSpot.position);
        else
            agent.SetDestination(sit.position);
    }
}
