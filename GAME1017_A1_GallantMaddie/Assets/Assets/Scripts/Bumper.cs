using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumper : MonoBehaviour
{

    public float bumpForce;
    public float fieldOfImpact;
    public LayerMask layerToBump;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bump();
    }

    void bump()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToBump);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * bumpForce);
        }
    }
}