using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

 
    float speed = 10;
    TMPro.TextMeshPro playerText;

    void Start()
    {
        playerText = this.GetComponentInChildren<TMPro.TextMeshPro>();
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
           
        }
    }


    public void NearClient()
    {
        this.UpdateText("Press E to serve drink");
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


}
