using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClientController : MonoBehaviour
{
    // Start is called before the first frame update

    TextMeshPro clientText;
    private bool waitingForDrink;
    public string clientName;
    public GameObject throwable;
    public float throwForce = 5.0f;
    private Places _place;
    private int _id;

    void Start()
    {
        this.clientText = this.GetComponent<TMPro.TextMeshPro>();
        clientText.text = "Awaiting Drink";
        waitingForDrink = Random.Range(0, 10) > 5;
        UpdateText(clientName);
    }

    void FixedUpdate()
    {
        if (Random.Range(0, 100) > 98)
            Throw();    
    }

    public void ReceivedDrink()
    {
        if(waitingForDrink)
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

}
