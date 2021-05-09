using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerColliderScript : MonoBehaviour
{

    TextMeshPro clientText;
    PlayerController playerController;
    private bool isNearClient;
    private bool isNearDrink;
    private bool isNearCircuit;
    private bool isFried;
    private GameObject nearClient;
    private GameObject nearDrink;
    private Circuit nearCircuit;

    void Start()
    {
        this.clientText = this.GetComponent<TMPro.TextMeshPro>();
        clientText.text = " ";
        playerController = this.GetComponentInParent<PlayerController>();
        isNearClient = false;
        isNearDrink = false;
}

    // Update is called once per frame
    void Update()
    {

        if (isNearClient)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (playerController.hasBeer)
                {
                    if (nearClient.GetComponentInChildren<ClientController>().WaitingForDrink)

                        playerController.ServedDrink();


                    else playerController.FailedServedDrink();


                        nearClient.GetComponentInChildren<ClientController>().ReceivedDrink();
                }
            }
        
        } 

        else if (isNearDrink)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerController.PickedUpDrink();
            }
        }

        else if (isNearCircuit && nearCircuit.fried)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    nearCircuit.Repair();
                }
            }
            

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Client")
        {
            playerController.NearClient();
            isNearClient = true;
            nearClient = collision.gameObject;
        }


        else if(collision.gameObject.tag == "Drink")
        {
            playerController.NearDrink();
            isNearDrink = true;
            nearClient = collision.gameObject;
        }

        else if (collision.gameObject.tag == "Puddle")
        {
           
            playerController.nearPuddle = true;
        }

      
        else if (collision.gameObject.tag == "Circuit")
        {
            isFried = collision.gameObject.GetComponent<Circuit>().fried;
            playerController.NearCircuit(isFried);
            isNearCircuit = true;
            nearCircuit = collision.gameObject.GetComponent<Circuit>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Client")
        {
            playerController.AwayFromClient();
            isNearClient = false;
            nearClient = null;
        }

        else if (collision.gameObject.tag == "Drink")
        {
            playerController.AwayFromBar();
            isNearDrink = false;
            nearClient = null;
        }

        else if (collision.gameObject.tag == "Puddle")
        {
            playerController.nearPuddle = false;
        }
    }



}


