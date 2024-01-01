using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxColorChanger : MonoBehaviour
{
    public Color colorStart;
    public Color colorEnd;
    public float duration = 1.0F;
    public float step = 0;

    private void Update()
    {
        RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(colorStart, colorEnd, step));
        step += Time.deltaTime / duration;
    }
}
