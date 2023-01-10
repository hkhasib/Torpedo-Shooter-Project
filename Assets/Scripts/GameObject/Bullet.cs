using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    private bool shoot = false;
    private Vector2 initialTargetPos;

    void Start()
    {
        secondaryTarget = GameObject.FindWithTag("PlayerHintPoint").transform.position;
        initialTargetPos = secondaryTarget;

    }

    // Update is called once per frame
    void Update()
    {

        secondaryTarget = GameObject.FindWithTag("PlayerHintPoint").transform.position;
        secondaryTarget.y = initialTargetPos.y;
        if (shoot)
        {
            transform.position = Vector3.MoveTowards(transform.position, secondaryTarget, speed * Time.deltaTime);

        }
        if (transform.position.x == GameObject.FindWithTag("PlayerHintPoint").transform.position.x)
        {

            shoot = false;
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBulletPoint")
        {
            shoot = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
