using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHandler : MonoBehaviour
{
    private void Start()
    {
        Invoke("DeathChicken",1.5f);

    }

    private void DeathChicken()
    {
        Destroy(gameObject);
    }
}
