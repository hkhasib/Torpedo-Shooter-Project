using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTorpedo : Projectile
{
    // Start is called before the first frame update
    private Transform playerTransform, playerActionPoint;
    private bool shoot = false;
    private bool targetMissed = false;
    
    void Start()
    {
        secondaryTarget = GameObject.FindWithTag("LeftOuterTarget").transform.position;
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerActionPoint = GameObject.FindWithTag("PlayerActionPoint").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(shoot)
        {
            transform.position = Vector3.MoveTowards(transform.position, primaryTarget, speed * Time.deltaTime);
            
        }

        if (transform.position.x == primaryTarget.x)
        {
            shoot= false;
            targetMissed= true;
            
            
        }
        if(targetMissed)
        {
            transform.position = Vector3.MoveTowards(transform.position, secondaryTarget, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerActionPoint")
        {
            primaryTarget = playerTransform.position;
            
            shoot = true;
        }
    }
}
