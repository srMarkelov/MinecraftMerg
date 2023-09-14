using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileOptimization : MonoBehaviour
{
    [SerializeField] private List<Button> button = new List<Button>();
    [SerializeField] private GameObject referencePosition;
    [SerializeField] private GameObject referencePosition2;

    private void Start()
    {
        if (Application.isMobilePlatform)     ////////
        {
            button[0].transform.localScale = new Vector3(1.45f, 1.45f, 1.45f);
            /*
            button[0].transform.position = new Vector3(button[0].transform.position.x-0.4f, button[0].transform.position.y, button[0].transform.position.z);
            */
            button[1].transform.localScale = new Vector3(1.47f, 1.47f, 1.47f);
            button[2].transform.localScale = new Vector3(1.47f, 1.47f, 1.47f);
            button[3].transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
            //button[3].transform.localPosition = new Vector3(referencePosition.transform.localPosition.x-150f, referencePosition.transform.localPosition.y-0f, button[3].transform.localPosition.z);

            button[3].transform.SetParent(referencePosition.transform);
            button[3].transform.localPosition = new Vector3(-100f,0,1);



            button[0].transform.localPosition = new Vector3(button[0].transform.localPosition.x-65f, button[0].transform.localPosition.y, button[0].transform.localPosition.z);
            button[2].transform.localPosition = new Vector3(button[2].transform.localPosition.x+120f, button[2].transform.localPosition.y, button[2].transform.localPosition.z);
            
            button[4].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }
}
