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
    public bool WaitingForDrink;
   
    public GameObject throwable;
    public float throwForce = 5.0f;
    public int _id = 0;
    private int type;
    private NavMeshAgent client;
    private bool followingPlayer;
    private Animator clientAnimator;
    private SpriteRenderer clientRenderer;
    [SerializeField] Vector3 target;
    [SerializeField] public PubManager pubs;

    void Start()
    {
      
        this.clientText = this.GetComponent<TMPro.TextMeshPro>();
        
       
        
        clientAnimator = this.GetComponentInParent<Animator>();
        clientRenderer = this.GetComponentInParent<SpriteRenderer>();
        pubs.TVOn += TVOn;
        pubs.TVOff += TVoff;
        pubs.NewClient += CheckMyBeer;

        client = this.GetComponentInParent<NavMeshAgent>();
        client.updateRotation = false;
        client.updateUpAxis = false;
        type = Random.Range(1, 5);
        UpdateText(this.gameObject.name);
    }

    void FixedUpdate()
    {

        if (followingPlayer)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position;
            client.SetDestination(target);

            if (this.transform.position.x - target.x > 0)
                clientRenderer.flipX = true;
            else clientRenderer.flipX = false;

        }
    }

    public void ReceivedDrink()
    {
        if (followingPlayer)
        {
            ReturnToSeat();
            UpdateText("Just what I needed");
        }
        else
        {


            if (WaitingForDrink)
                UpdateText("Thank you");
            else UpdateText("That's not for me");
        }
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

    public void CheckMyBeer()
    {
        if(pubs.CurrentClient._id == _id)
        {
            WaitingForDrink = true;
        }
    }

    public void DrinkBeer()
    {
        //Activate Animator Booolean
    }

    public void Fight()
    {
        //Activate Fight Routine Booolean
    }


    public void FollowPlayer()
    {
        // Update Target
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        client.SetDestination(target);
        followingPlayer = true;
        clientAnimator.SetBool("Walking", true);
        UpdateText("I need BEEER!!");

    }

    public void TVoff()
    {
        SelectAction();
    }

    public void ReturnToSeat()
    {
        if (type == 2)
            followingPlayer = false;

        target = pubs.getPlace(this._id).transform.position;
        client.SetDestination(target);

         if (this.transform.position.x - target.x > 0)
                clientRenderer.flipX = true;
            else clientRenderer.flipX = false;
    }
    public void TVOn()
    {
        ReturnToSeat();
    }
    private void SelectAction()
    {
        switch (type)
        {

            case 1:
                DrinkBeer();
                break;

            case 2:
                FollowPlayer();
                break;

            case 3:
                Fight();
                break;

            case 4:
                Throw();
                break;
        }
    }



}
