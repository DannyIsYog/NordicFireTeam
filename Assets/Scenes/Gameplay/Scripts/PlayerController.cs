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
    private bool downed = false;
    private bool hasBeer = false;
    public event Action Delivery;
    public event Action FailedDelivery;
    public event Action Pickup;
    private GameObject goalUI;
    private float textTimer = 3.0f;


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
        else UpdateText("");
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
            this.UpdateText("Press E to serve drink");
        else this.UpdateText("You have nothing to serve");
    }

    public void NearDrink()
    {
        if (!hasBeer)
            this.UpdateText("Press E to grab Beer");
        else this.UpdateText("You already have a Beer");

        goalUI.SetActive(true);
    }

    public void AwayFromClient()
    {
        this.UpdateText("I'm the Player");
    }

    public void AwayFromBar()
    {
        goalUI.SetActive(false);
    }

    public void UpdateText(string s)
    {
        textTimer = 3.0f;
        playerText.text = s;
    }

    public void GotHit()
    {
        this.UpdateText("I got hit");
        playerAnimator.SetTrigger("Fall");
        playerAnimator.SetBool("HasPlate", false);
        playerAnimator.SetBool("HasBeer", false);
        hasBeer = false;
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
            this.UpdateText("Another round");
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
            this.UpdateText("Enjooy");
            playerAnimator.SetBool("HasBeer", false);
            hasBeer = false;
            Delivery?.Invoke();
        }
    }

    public void FailedServedDrink()
    {
        if (hasBeer)
        {
            this.UpdateText("Enjooy");
            playerAnimator.SetBool("HasBeer", false);
            hasBeer = false;
            FailedDelivery?.Invoke();
        }
    }





}
