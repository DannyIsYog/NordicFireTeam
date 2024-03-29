using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    // Start is called before the first frame update

    public float lifeTime = 5.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        this.transform.Rotate(0, 0,1f);
        if (lifeTime <= 0)
            Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision) { 
    
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerController>().downed == false)
        {
            collision.gameObject.GetComponent<PlayerController>().GotHit();
        }
    }
}
