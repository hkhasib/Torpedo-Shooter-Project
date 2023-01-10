using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : TorpedoShooter
{
    public GameObject player;
    public float offset;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.position = new Vector3(playerPosition.x+offset, transform.position.y, transform.position.z);
    }
}
