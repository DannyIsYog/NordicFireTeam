using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

 
    public float speed = 10;
    TMPro.TextMeshPro playerText;
    SpriteRenderer playerSprite;
    Animator playerAnimator;
    public bool nearPuddle = false;
    public bool downed = false;
    public bool hasBeer = false;
    public event Action Delivery;
    public event Action FailedDelivery;
    public event Action Pickup;
    public event Action Fall;
    private GameObject goalUI;
    private float textTimer = 3.0f;


    List<string> fallLines = new List<string>()
    {
        "I got hit",
        "Call an ambulance",
        "By St.Patrick's Beard!",
        "Not again!"
    };

    List<string> serveLines = new List<string>()
    {
        "You got served",
        "There you go",
        "Drink it all",
        "Down the hatch"
    };

    List<string> beerLines = new List<string>()
    {
        "I need another one",
        "Save one for me",
        "Another round!",
        "Enjoy!!"
    };

    void Start()
    {
        playerText = this.GetComponentInChildren<TMPro.TextMeshPro>();
        playerSprite = this.GetComponent<SpriteRenderer>();
        playerAnimator = this.GetComponent<Animator>();
        goalUI = GameObject.Find("UIGoal");
        goalUI.SetActive(false);
    }

    void Update()
    {

        if (textTimer >= 0)
            textTimer -= Time.deltaTime;
        else UpdateText("", 1);
        // correct for diagonal movement with normalized
        if (!downed)
        {
            var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            var tempSpeed = speed;
            if (nearPuddle)
                tempSpeed *= 2;
            else tempSpeed = speed;

            if (moveInput.y != 0)
            {
                // apply with the new up/down direction vector instead of Vector3.up
                transform.position += moveInput.y * Vector3.up * tempSpeed * Time.deltaTime;
            }

            if (moveInput.x != 0)
            {


                // else move normally
                transform.position += moveInput.x * Vector3.right * tempSpeed * Time.deltaTime;

                if (moveInput.x < 0)
                {
                    playerSprite.flipX = true;
                }
                else playerSprite.flipX = false;
            }

            if (moveInput.x == 0 && moveInput.y == 0)
                playerAnimator.SetBool("isWalking", false);
            else playerAnimator.SetBool("isWalking", true);
        }

    }

    public void NearClient()
    {
        if (hasBeer)
            this.UpdateText("Press E to serve drink", 2);
        else this.UpdateText("You have nothing to serve", 1);
    }

    public void NearDrink()
    {
        if (!hasBeer)
            this.UpdateText("Press E to grab Beer", 3);
        else this.UpdateText("You already have a Beer", 1);

        goalUI.SetActive(true);
    }

    public void AwayFromClient()
    {

    }

    public void AwayFromBar()
    {
        goalUI.SetActive(false);
    }

    public void UpdateText(string s, int time)
    {
        textTimer = time;
        playerText.text = s;
    }

    public void GotHit()
    {

        var r = UnityEngine.Random.Range(0, fallLines.Count);
        this.UpdateText(fallLines[r], 4);
        playerAnimator.SetTrigger("Fall");
        playerAnimator.SetBool("HasPlate", false);
        playerAnimator.SetBool("HasBeer", false);
        hasBeer = false;

        Fall?.Invoke();
    }

    public void GotDown()
    {
      
        downed = true;
    }

    public void GotUp()
    {
        downed = false;
    }

    public void PickedUpDrink()
    {
        if (!hasBeer)
        {
            var r = UnityEngine.Random.Range(0, beerLines.Count);
            this.UpdateText(beerLines[r], 3);
            hasBeer = true;
            playerAnimator.SetBool("HasBeer", true);
            playerAnimator.SetBool("HasPlate", true);
            Pickup?.Invoke();
        }
    }

    public void ServedDrink()
    {
        if (hasBeer)
        {
            var r = UnityEngine.Random.Range(0, serveLines.Count);
            this.UpdateText(serveLines[r], 2);
            playerAnimator.SetBool("HasBeer", false);
            hasBeer = false;
            Delivery?.Invoke();
        }
    }

    public void FailedServedDrink()
    {
        if (hasBeer)
        {
            var r = UnityEngine.Random.Range(0, serveLines.Count);
            this.UpdateText(serveLines[r], 2);
            playerAnimator.SetBool("HasBeer", false);
            hasBeer = false;
            FailedDelivery?.Invoke();
        }
    }

    public void NearCircuit(bool fried)
    {
        if (fried)
            this.UpdateText("Press E to repair Circuit", 3);
        else this.UpdateText("This Circuit is fine", 2);
    }




}
