using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public Transform Target;

    private bool activated;
    public float HideDistance = 3.0f;
    private Color originalColor;
    private Color offColor;
    private SpriteRenderer sprite;
    private Quaternion originalRotation;

    void Start()
    {
        sprite = this.transform.GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        offColor = new Color(originalColor.r, originalColor.g, originalColor.b);
        offColor.a = 0;
        sprite.color = offColor;
        originalRotation = this.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            var dir = Target.transform.position - this.transform.position;

            if (dir.magnitude < HideDistance)
                sprite.color = offColor;
           else 
                sprite.color = originalColor;

            var angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void Activate(Transform target)
    {
        this.Target = target;
        activated = true;
        sprite.color = originalColor;


    }

    public void DeActivate()
    {
        activated = false;
        sprite.color = offColor;
    }

    
}
