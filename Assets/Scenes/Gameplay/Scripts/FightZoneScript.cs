using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightZoneScript : MonoBehaviour
{

    float timer = 3.0f;
    public GameObject bottlePrefab;
    public float throwForce = 2.0f;

    private float moveX;
    private float moveY;
    // Start is called before the first frame update
    void Start()
    {
        NextAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            this.transform.Translate(0.0025f * moveX, 0.0025f * moveY, 0.0f);
        }
        else NextAction();

        this.transform.Rotate(0f, 0f, 0.05f);   
        

    
    }

    void NextAction()
    {
        moveX = Random.Range(-1, 2);
        moveY = Random.Range(-1, 2);
        Throw();
        timer = 4.0f;
    }


    void Throw()
    {
        var bottle = Instantiate(bottlePrefab, this.transform);
        bottle.transform.position = this.transform.position;
        var targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        var bottlePosition = bottle.transform.position;
        bottle.transform.GetComponent<Rigidbody2D>().AddForce((targetPosition - bottlePosition) * throwForce, ForceMode2D.Impulse);

    }




}
