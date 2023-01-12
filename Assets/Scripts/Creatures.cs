using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creatures : Character
{
    private Transform creature;
    private bool attack = false;
    private bool playerHint = false;
    private bool hitPoint = false;
    private Animator creatureAnimator;
    public float speed = 0f;
    private Transform target;
    private GameObject player;
    private Transform playerTransform;
    private Vector2 playerInitialPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        creature = GetComponent<Transform>();
        creatureAnimator= GetComponent<Animator>();
        target = GameObject.FindWithTag("LeftOuterTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHint)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f * Time.deltaTime);
        }
        if (attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerInitialPosition, speed * Time.deltaTime);
        }
        if (transform.position.x == playerInitialPosition.x)
        {
            hitPoint= true;
            
        }
        if(hitPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "PlayerActionPoint")
        {
            attack= true;
            playerInitialPosition=playerTransform.position;
            creatureAnimator.SetBool("Attack", true);
            
        }
        else if(collision.gameObject.tag == "PlayerHintPoint")
        {
            playerHint = true;
        }


    }
}
