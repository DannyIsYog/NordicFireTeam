using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // angles relative to right being 0 degrees (pos goes up, neg goes down)
    float upDownAngle = 55;
    float stairsAngle = -45;
    Vector3 upDownDirection;
    Vector3 stairsDirection;
    float speed = 10;
    bool onStairs;

    void Start()
    {
        // turn angles into direction vectors
        upDownDirection = new Vector3(Mathf.Cos(upDownAngle * Mathf.Deg2Rad), Mathf.Sin(upDownAngle * Mathf.Deg2Rad));
        stairsDirection = new Vector3(Mathf.Cos(stairsAngle * Mathf.Deg2Rad), Mathf.Sin(stairsAngle * Mathf.Deg2Rad));
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


}
