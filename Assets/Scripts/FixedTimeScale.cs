using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTimeScale : MonoBehaviour
{
    public void TimeScale()
    {
        Invoke("TimeScaleFixed",0.3f);
    }
    
    public void TimeScaleFixed()
    {
        Time.timeScale = 2f;
    }
}
