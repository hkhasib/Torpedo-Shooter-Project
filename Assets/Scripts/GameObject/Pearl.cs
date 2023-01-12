using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Pearl : TorpedoShooter
{
    // Start is called before the first frame update
    public Light2D pearlLight;
    private float lightIntens = 0f, change = 0.001f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lightIntens = lightIntens + change;
        if (lightIntens > 1f)
        {
            change=-change;
        }
        else if (lightIntens < 0.4f)
        {
            change = 0.001f;
        }
        pearlLight.intensity = lightIntens;
    }
}
