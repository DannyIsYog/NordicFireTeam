using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

 
    float speed = 10;
    TMPro.TextMeshPro playerText;
    SpriteRenderer playerSprite;
    Animator playerAnimator;

    void Start()
    {
        playerText = this.GetComponentInChildren<TMPro.TextMeshPro>();
        playerSprite = this.GetComponent<SpriteRenderer>();
        playerAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        // correct for diagonal movement with normalized
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (moveInput.y != 0)
        {
            // apply with the new up/down direction vector instead of Vector3.up
            transform.position += moveInput.y * Vector3.up * speed * Time.deltaTime;
        }

        if (moveInput.x != 0)
        {
           
                // else move normally
                transform.position += moveInput.x * Vector3.right * speed * Time.deltaTime;

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


    public void NearClient()
    {
        this.UpdateText("Press E to serve drink");
    }

    public void NearDrink()
    {
        this.UpdateText("Press E to grab Beer");
    }

    public void AwayFromClient()
    {
        this.UpdateText("I'm the Player");
    }
    public void UpdateText(string s)
    {
        playerText.text = s;
    }

    public void GotHit()
    {
        this.UpdateText("I got hit");
    }

    public void PickedUpDrink()
    {
        this.UpdateText("Another round");
        playerAnimator.SetBool("HasBeer", true);
    }

    public void ServedDrink()
    {
        this.UpdateText("Enjooy");
        playerAnimator.SetBool("HasBeer", false);
    }


}
