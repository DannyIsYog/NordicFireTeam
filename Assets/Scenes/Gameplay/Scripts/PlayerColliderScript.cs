using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerColliderScript : MonoBehaviour
{

    TextMeshPro clientText;
    PlayerController playerController;
    private bool isNearClient;
    private GameObject nearClient;

    void Start()
    {
        this.clientText = this.GetComponent<TMPro.TextMeshPro>();
        clientText.text = "I'm the Player";
        playerController = this.GetComponentInParent<PlayerController>();
        isNearClient = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isNearClient)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ServeDrink();
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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Client")
        {
            playerController.AwayFromClient();
            isNearClient = false;
            nearClient = collision.gameObject;
        }
    }


    public void ServeDrink()
    {
        playerController.UpdateText("Enjoy that bitch");
        nearClient.GetComponentInChildren<ClientController>().ReceivedDrink();
    }

}


