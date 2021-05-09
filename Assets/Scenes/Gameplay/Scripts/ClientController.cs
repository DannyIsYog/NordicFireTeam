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
    private bool fighting = false;
    private bool throwing = false;
    private Animator clientAnimator;
    private SpriteRenderer clientRenderer;
    [SerializeField] Vector3 target;
    [SerializeField] public PubManager pubs;

    private bool walked = false;
    private BoxCollider2D _collider;
    private float timerAux;
    private float textTime = 4.0f;

    private float[][] _probabilities;
    

    void Start()
    {
        PopulateProbabilities();
        _collider = this.GetComponent<BoxCollider2D>();
        this.clientText = this.GetComponent<TMPro.TextMeshPro>();


        timerAux = textTime;
        clientAnimator = this.GetComponentInParent<Animator>();
        clientRenderer = this.GetComponentInParent<SpriteRenderer>();
        pubs.TVOn += TVOn;
        pubs.TVOff += TVoff;
        pubs.NewClient += CheckMyBeer;

        client = this.GetComponentInParent<NavMeshAgent>();
        client.updateRotation = false;
        client.updateUpAxis = false;
        //type = Random.Range(1, 5);
        ChooseAction();
        UpdateText(this.gameObject.name);
    }

    void FixedUpdate()
    {
        if (walked && client.velocity.magnitude < 0.05f)
        {
            _collider.enabled = true;
            clientAnimator.SetBool("Walking", false);
        }
        if (timerAux >= 0)
            timerAux -= Time.deltaTime;
        else
            UpdateText(this.gameObject.name);


        if (fighting)
        {
            target = GameObject.FindGameObjectWithTag("FightZone").transform.position;
            client.SetDestination(target);
        }
         else if (followingPlayer)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform.position;
                client.SetDestination(target);

                if (this.transform.position.x - target.x > 0)
                    clientRenderer.flipX = true;
                else clientRenderer.flipX = false;

            }

        else if (throwing)
        {
            var r = Random.Range(0, 100);
            var r2 = Random.Range(0, 100);
            if (r > 98 && r2 > 95)
                Throw();
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

        clientAnimator.SetBool("HasBeer", true);

    }

    public void UpdateText(string s)
    {
        timerAux = textTime;
        clientText.text = s;
    }

    public void Throw()
    {
        var r = Random.Range(0, pubs.throwablePrefabs.Count);
        var prefab = pubs.throwablePrefabs[r];
        var bottle = Instantiate(prefab, this.transform);
        bottle.transform.position = this.transform.position;
        var targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        var bottlePosition = bottle.transform.position;
        clientAnimator.SetBool("HasBeer", false);
        bottle.transform.GetComponent<Rigidbody2D>().AddForce((targetPosition - bottlePosition) * throwForce, ForceMode2D.Impulse);
    }

    public void CheckMyBeer()
    {
        if(pubs.CurrentClient._id == _id)
        {
            WaitingForDrink = true;
            clientAnimator.SetBool("HasBeer", false);
        }
    }

    public void DrinkBeer()
    {
        //Activate Animator Booolean
        clientAnimator.SetBool("HasBeer", true);
    }

    public void Fight()
    {
        //Activate Fight Routine Booolean
        target = GameObject.FindGameObjectWithTag("FightZone").transform.position;
        client.SetDestination(target);
        clientAnimator.SetBool("Walking", true);
        clientAnimator.SetBool("HasBeer", false);
        UpdateText("I want to FIGHT!");
    }


    public void FollowPlayer()
    {
        // Update Target
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        client.SetDestination(target);
        followingPlayer = true;
        clientAnimator.SetBool("Walking", true);
        clientAnimator.SetBool("HasBeer", false);
        UpdateText("I need BEEER!!");

    }

    public void TVoff()
    {
        ExecuteAction();
    }

    public void ReturnToSeat()
    {
      fighting = false;
      this.clientRenderer.color = new Color(1, 1, 1, 1);
      followingPlayer = false;
      throwing = false;
        target = pubs.getPlace(this._id).transform.position;
        client.SetDestination(target);
        

         if (this.transform.position.x - target.x > 0)
                clientRenderer.flipX = true;
            else clientRenderer.flipX = false;
        _collider.enabled = false;
       walked = true;
    }
    public void TVOn()
    {
        ChooseAction();
        ReturnToSeat();
    }

    public void Fighting()
    {
        fighting = true;
        this.clientRenderer.color = new Color(0, 0, 0, 0);
        UpdateText(" ");
    }

    private void ExecuteAction()
    {
        switch (type)
        {

            case 1:
                DrinkBeer();
                break;

            case 2:
                throwing = true;
                break;

            case 3:
                Fight();
                break;

            case 4:
                FollowPlayer();
                break;
        }
    }

    void PopulateProbabilities()
    {
        _probabilities[0] = new float[4] { .9f, .1f, 0, 0 };
        _probabilities[1] = new float[4] { .6f, .2f, .2f, 0 };
        _probabilities[2] = new float[4] { .4f, .2f, .2f, .2f };
    }

    void ChooseAction()
    {
        int phase = pubs.Phase;
        float rand = Random.Range(0f, 1f);
        if (rand <= _probabilities[phase][0])
        {
            type = 1;
        }
        else if (rand <= _probabilities[phase][1])
        {
            type = 2;
        }
        else if (rand <= _probabilities[phase][2])
        {
            type = 3;
        }
        else
        {
            type = 4;
        }
    }



}
