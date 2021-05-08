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
    
    void Start()
    {
        this.clientText = this.GetComponent<TMPro.TextMeshPro>();
        clientText.text = "Awaiting Drink";
        waitingForDrink = Random.Range(0, 10) > 5;
        UpdateText(clientName);

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

}
