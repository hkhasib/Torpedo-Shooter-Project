using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FastEnemy : Enemy
{

    void Start()
    {
        //initialTorpedoPos=torpedo.transform.position;
        if (!hasLight)
        {
            bodyLight.intensity = 0f;
            torchLight.intensity = 0f;
        }
        player = GameObject.FindWithTag("Player");
        enemyAnimator = GetComponent<Animator>();
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHint||attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("LeftOuterTarget").transform.position, speed * Time.deltaTime);
        }

        if (life <= 0)
        {
            die = true;
            shoot = false;

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "PlayerActionPoint")
        {
            if (!die)
            {
                AudioManager.Instance.PlaySound("enemyMissile");
                clonedTorpedo = Instantiate(GameObject.FindWithTag("EnemyFastTorpedo").transform, enemyTorpedoPoint.position, transform.rotation);
                attack = true;
                playerInitialPosition = player.transform.position;
                
            }

        }
        else if (collision.gameObject.tag == "PlayerHintPoint")
        {
            playerHint = true;
        }

        if (collision.gameObject.tag == "Bullet")
        {

            Player.bulletHitted = true;
            getHit = true;
            life = life - 1;
            if (life <= 0)
            {
                LevelManager.score = LevelManager.score + 20;
                die = true;
                shoot = false;
                //Destroy(bubbleParticle);

            }
            StartCoroutine(hitAnimation(0.1f));

        }
        if (collision.gameObject.tag == "Torpedo")
        {

            Player.torpedoHitted = true;
            getHit = true;
            life = life - 5;
            if (life <= 0)
            {
                LevelManager.score = LevelManager.score + 20;
                die = true;
                shoot = false;

            }
            StartCoroutine(hitAnimation(0.1f));

        }


    }


    IEnumerator waitForAction(float second)
    {

        yield return new WaitForSeconds(second);
        Destroy(gameObject);

    }

    IEnumerator hitAnimation(float second)
    {
        if (die)
        {
            bodyLight.intensity = 0f;
            torchLight.intensity = 0f;
            AudioManager.Instance.PlaySound("explosion");
            enemyAnimator.SetBool("die", true);
            yield return new WaitForSeconds(1);
            enemyAnimator.SetBool("die", false);
            Destroy(gameObject);

        }
        else
        {
            enemyAnimator.SetBool("gotHit", true);
            yield return new WaitForSeconds(second);
            enemyAnimator.SetBool("gotHit", false);
        }


    }
}
