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
    private GameObject nearClient;
    private GameObject nearDrink;

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
                if (nearClient.GetComponentInChildren<ClientController>().WaitingForDrink)
                
                    playerController.ServedDrink();
                   
                
                else  playerController.FailedServedDrink();


                if(playerController.hasBeer)
                    nearClient.GetComponentInChildren<ClientController>().ReceivedDrink();
             }
        
        } else if(isNearDrink)
               
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerController.PickedUpDrink();
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


