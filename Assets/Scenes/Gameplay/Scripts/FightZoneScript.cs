using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightZoneScript : MonoBehaviour
{

    float timer = 3.0f;
    public GameObject bottlePrefab;
    public float throwForce = 2.0f;

    private float moveX;
    private float moveY;
    private bool waitingForClients = false;
    private bool started = false;

    private SpriteRenderer spriteRenderer;
    private GameObject customer1;
    private GameObject customer2;

    private PubManager pubManager;
    // Start is called before the first frame update
    void Start()
    {
       
        pubManager = GameObject.Find("PubManager").GetComponent<PubManager>();
        pubManager.TVOn += StopFight;
        pubManager.TVOff += Activate;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        StopFight();

    }

    // Update is called once per frame
    void Update()
    {

        if (started)
        {
            if (timer >= 0)
            {
                this.transform.Translate(0.0025f * moveX, 0.0025f * moveY, 0.0f);
            }
            else NextAction();

            this.transform.Rotate(0f, 0f, 0.05f);


        }
    }

    void NextAction()
    {
        moveX = Random.Range(-1, 2);
        moveY = Random.Range(-1, 2);
        Throw();
        timer = 4.0f;
    }


    void Throw()
    {
        var bottle = Instantiate(bottlePrefab, this.transform);
        bottle.transform.position = this.transform.position;
        var targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        var bottlePosition = bottle.transform.position;
        bottle.transform.GetComponent<Rigidbody2D>().AddForce((targetPosition - bottlePosition) * throwForce, ForceMode2D.Impulse);

    }


    public void Activate()
    {
        waitingForClients = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(waitingForClients)
        if (collision.gameObject.tag == "Client")
            if (customer1 == null)
            {
                customer1 = collision.gameObject;
                customer1.gameObject.GetComponentInChildren<ClientController>().Fighting();
            }
            else if (customer2 == null && collision.gameObject != customer1)
            {
                customer2 = collision.gameObject;
                customer2.gameObject.GetComponentInChildren<ClientController>().Fighting();
                StartFight();
            } else
                {
                    collision.gameObject.GetComponentInChildren<ClientController>().Fighting();
                }
    }

    void StartFight()
    {
        started = true;
        spriteRenderer.color = new Color(1, 1, 1,1);
    }

    public void StopFight()
    {
        started = false;
        spriteRenderer.color = new Color(0, 0, 0, 0);
    }



}
