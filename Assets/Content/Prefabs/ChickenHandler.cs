using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHandler : MonoBehaviour
{
    private void Start()
    {
        Invoke("DeathChicken",3f);

    }

    private void DeathChicken()
    {
        Destroy(gameObject);
    }
}
