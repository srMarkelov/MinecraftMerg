using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTimeScale : MonoBehaviour
{
    public void TimeScale()
    {
        Invoke("TimeScaleFixed",1f);
    }
    
    public void TimeScaleFixed()
    {
        Time.timeScale = 2f;
    }
}
