using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DethBee : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<BeeMove>())
        {
            Destroy(col.gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<BeeMove>())
        {
            Destroy(col.gameObject);
        }
    }
}
