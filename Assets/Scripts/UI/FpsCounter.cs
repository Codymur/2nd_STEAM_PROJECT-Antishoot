using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsCounter : MonoBehaviour
{

    private float FPS;
    public TMPro.TextMeshProUGUI FpsCounterText;

    void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    void GetFPS()
    {
        FPS = (int)(1f / Time.unscaledDeltaTime);
        FpsCounterText.text = FPS.ToString();
    }
}
