using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocentCreature : SeaCreeatures
{
    
    // Start is called before the first frame update
    void Start()
    {
        if(topMove)
        {
            target = GameObject.FindWithTag("TopOuterTarget").transform;
        }
        else
        {
            target = GameObject.FindWithTag("LeftOuterTarget").transform;
        }
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
            Debug.Log("NimoHint");
            playerHint = true;
        }
    }
}
