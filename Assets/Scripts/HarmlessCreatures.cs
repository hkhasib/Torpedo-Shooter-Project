using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmlessCreatures : Character
{
    public Transform target;
    public float speed = 0f;
    private bool playerHint=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerHint)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHintPoint")
        {
            Debug.Log("Hint");
            playerHint = true;
        }
    }
}
