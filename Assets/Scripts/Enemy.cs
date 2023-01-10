using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Transform enemy;
    private bool attack = false;
    private bool playerHint = false;
    private bool getHit=false;
    private bool die=false;
    private bool shoot = false;
    private Animator creatureAnimator;
    public float speed = 0f;
    

    private Transform clonedTorpedo;

    public Transform enemyTorpedoPoint;
    //public Transform torpedo;
    private GameObject player;
    private Vector2 playerInitialPosition, initialTorpedoPos;
    private Animator enemyAnimator;
    public static bool torpedoHitted = false;
    // Start is called before the first frame update
    void Start()
    {
        //initialTorpedoPos=torpedo.transform.position;
        player = GameObject.FindWithTag("Player");
        enemyAnimator= GetComponent<Animator>();
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHint)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("LeftOuterTarget").transform.position, 0.5f * Time.deltaTime);
        }
        if (attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("LeftOuterTarget").transform.position, speed * Time.deltaTime);
        }

        if(life<=0)
        {
            die= true;
            shoot= false;
            
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "PlayerActionPoint")
        {
            if(!die)
            {
                AudioManager.Instance.PlaySound("enemyMissile");
                attack = true;
                playerInitialPosition = player.transform.position;
                clonedTorpedo = Instantiate(GameObject.FindWithTag("EnemyTorpedo").transform, enemyTorpedoPoint.position, transform.rotation);
            }
            //shoot = true;
            
        }
        else if (collision.gameObject.tag == "PlayerHintPoint")
        {
            playerHint = true;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            
            Player.bulletHitted = true;
            getHit= true;
            life = life - 1;
            if (life <= 0)
            {
                die = true;
                shoot = false;

            }
            StartCoroutine(hitAnimation(0.1f));
            //enemyAnimator.SetBool("gotHit",true);

        }
        if (collision.gameObject.tag == "Torpedo")
        {
            
            Player.torpedoHitted = true;
            getHit= true;
            life = life - 5;
            if (life <= 0)
            {
                die = true;
                shoot = false;

            }
            StartCoroutine(hitAnimation(0.1f));
            //enemyAnimator.SetBool("gotHit", true);
            
        }


    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
        
    //    if (collision.gameObject.tag == "Bullet")
    //    {

            
    //        enemyAnimator.SetBool("gotHit", false);
    //        if(die)
    //        {
    //            enemyAnimator.SetBool("die", true);
    //            StartCoroutine(waitForAction(1));

                
    //        }
            
    //    }
    //    if (collision.gameObject.tag == "Torpedo")
    //    {

            
    //        enemyAnimator.SetBool("gotHit", false);
    //        if (die)
    //        {
    //            enemyAnimator.SetBool("die", true);

    //            StartCoroutine(waitForAction(1));

                
    //        }
    //    }
    //}

    IEnumerator waitForAction(float second)
    {
        
        yield return new WaitForSeconds(second);
        Destroy(gameObject);

    }

    IEnumerator hitAnimation(float second)
    {
        if(die)
        {
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
