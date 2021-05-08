using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class ClientController : MonoBehaviour
{
    // Start is called before the first frame update

    TextMeshPro clientText;
    private bool WaitingForDrink;
    public string clientName;
    public GameObject throwable;
    public float throwForce = 5.0f;
    private Places _place;
    public int _id;
    private bool TV;
    private int type;
    private NavMeshAgent client;
    
    [SerializeField] Transform target;
    [SerializeField] PubManager pubs;

    void Start()
    {
        TV = true;
        this.clientText = this.GetComponent<TMPro.TextMeshPro>();
        clientText.text = "Awaiting Drink";
        WaitingForDrink = Random.Range(0, 10) > 5;
        UpdateText(clientName);

        pubs.TVOn += WatchGame;
        pubs.TVOff += Fight;
        pubs.NewClient += CheckMyBeer;
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        client = this.GetComponentInParent<NavMeshAgent>();
        client.updateRotation = false;
        client.updateUpAxis = false;

        type = 2;
        Debug.Log("hello i'm type: " + type);
    }

    void FixedUpdate()
    {
        if (type == 0 && TV == false) //stay in place
        {
            
        }
        else if (type == 1 && TV == false) //fight with each other 
        {
            //need to determine fightspots
            //client.SetDestination(fightSpot.position);
        }
        else if (type == 2 && TV == false) //follow player
        {
            client.SetDestination(target.position);
        }
        else if (type == 3 && Random.Range(0, 100) > 98 && TV == false) //throw stuff at player
        {
            Throw();
        }

    }

    public void ReceivedDrink()
    {
        if(WaitingForDrink)
        UpdateText("Thank you");
        else UpdateText("That's not for me");
    }

    public void UpdateText(string s)
    {
        clientText.text = s;
    }

    public void Throw()
    {
        var bottle = Instantiate(throwable, this.transform);
        bottle.transform.position = this.transform.position;
        var targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        var bottlePosition = bottle.transform.position;

        bottle.transform.GetComponent<Rigidbody2D>().AddForce((targetPosition - bottlePosition) * throwForce, ForceMode2D.Impulse);
    }

    public void WatchGame()
    {
        TV = true;
        _place = pubs.places[_id];
        Debug.Log("o meu lugar é:" + pubs.places[_id]._id);
    }

    public void Fight()
    {
        TV = false;

        
    }

    public void CheckMyBeer()
    {
        if(pubs.CurrentClient._id == _id)
        {
            WaitingForDrink = true;
        }
    }

}
