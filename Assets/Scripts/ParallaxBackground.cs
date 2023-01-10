using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : TorpedoShooter
{
    public Transform cameraTransform;

    public Transform background1;
    public Transform background2;
    public Transform background3;
    public Transform background4;
    public Transform background5;
    public Transform background6;


    
    public float scroll2 = 1.0f;
    public float scroll3 = 1.0f;
    public float scroll4 = 1.0f;
    public float scroll5 = 1.0f;
    public float scroll6 = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        background1.transform.position = new Vector3(cameraTransform.position.x, background1.position.y, background1.position.z);
        background2.transform.position = new Vector3(cameraTransform.position.x * scroll2, background2.position.y, background2.position.z);
        background3.transform.position = new Vector3(cameraTransform.position.x * scroll3 ,background3.position.y, background3.position.z);
        background4.transform.position = new Vector3(cameraTransform.position.x * scroll4, background4.position.y, background4.position.z);
        background5.transform.position = new Vector3(cameraTransform.position.x * scroll5, background5.position.y, background5.position.z);
        background6.transform.position = new Vector3(cameraTransform.position.x * scroll6, background6.position.y, background6.position.z);

    }
}
