using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private Vector3 initialPosition, smartAttackPosition, nextPos;
    private int randomNum= 0;
    private float x = 0.01f, y=0.01f;
    private bool changeXPos=false, returnPos=false;
    public static bool bossReady=false;
    public Transform topPos, bottomPos;
    
    void Start()
    {
        bossReady = false;
        if (!hasLight)
        {
            bodyLight.intensity = 0f;
            torchLight.intensity = 0f;
        }
        player = GameObject.FindWithTag("Player");
        enemyAnimator = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        enemyTorpedoPoint.position = new Vector3(transform.position.x-2.54218f, transform.position.y- 0.2305602f, enemyTorpedoPoint.position.z);
        
        float posY = Mathf.PingPong(Time.time * 0.5f, 1) * 6 - 3;
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        //if (boosReady&&!LevelManager.levelComplete)
        //{
            

        //    x = x + y;
        //    if (x > 5f)
        //    {
        //        y = -0.01f;

        //        ShootTorpedo();

        //    }
        //    if (x < 0)
        //    {
        //        y = 0.01f;
        //    }


        //}
    }

    private IEnumerator shootDelay()
    {
        if (attack)
        {
            
            yield return new WaitForSeconds(3);
            clonedTorpedo = Instantiate(GameObject.FindWithTag("BossTorpedo").transform, enemyTorpedoPoint.position, transform.rotation);
            attack = false;
        }
    }

    private void ShootTorpedo()
    {
        AudioManager.Instance.PlaySound("enemyMissile");
        clonedTorpedo = Instantiate(GameObject.FindWithTag("BossTorpedo").transform, enemyTorpedoPoint.position, transform.rotation);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bossPoint")
        {
            if(bossReady && !LevelManager.levelComplete)
            {
                ShootTorpedo();
            }
            
        }
            if (collision.gameObject.tag == "Bullet")
        {

            Player.bulletHitted = true;
            getHit = true;
            life = life - 1;
            if (life <= 0)
            {
                LevelManager.score = LevelManager.score + 200;
                StartCoroutine(levelFinishDelay());
                
                die = true;
                shoot = false;
                //Destroy(bubbleParticle);

            }
            {
                StartCoroutine(hitAnimation(0.1f));
            }

        }
        if (collision.gameObject.tag == "Torpedo")
        {

            Player.torpedoHitted = true;
            getHit = true;
            life = life - 5;
            if (life <= 0)
            {
                LevelManager.score = LevelManager.score + 200;
                StartCoroutine(levelFinishDelay());

                die = true;
                shoot = false;
            }
            else
            {
                AudioManager.Instance.PlaySound("playerMissileHit");
                StartCoroutine(hitAnimation(0.2f));
            }
            
            

        }
    }
    IEnumerator hitAnimation(float second)
    {
      
            enemyAnimator.SetBool("gotHit", true);
            yield return new WaitForSeconds(second);
            enemyAnimator.SetBool("gotHit", false);
        

    }

    private IEnumerator levelFinishDelay()
    {
        bossReady = false;
        bodyLight.intensity = 0f;
        torchLight.intensity = 0f;
        enemyAnimator.SetBool("die", true);
        AudioManager.Instance.PlaySound("explosion");
        yield return new WaitForSeconds(1f);
        enemyAnimator.enabled = false;
        yield return new WaitForSeconds(1f);
        LevelManager.levelComplete = true;
        AudioManager.Instance.PlaySound("levelComplete");
        
        
    }

}
