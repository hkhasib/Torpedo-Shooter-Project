using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Player : Character
{
    
    public float speed = 5f;
    public float movement = 0f;
    public float bulletSpeed = 20f;
    public float torpedoSpeed = 5f;
    public Transform bullet, torpedo, target, torpedoPoint, bulletPoint, bubbleParticle;
    public float vertical_movement = 0f;
    private bool redStarCollision = false;
    private Transform clonedTorpedo;
    
    private Rigidbody2D player;
    private Animator playerAnimation;

    private float playerX = 0;
    

    public Transform playerActionPoint;
    public Transform playerHintPoint;

    private Vector2 targetPosition, torpedTarget;

    private Vector2 initialBulletPos, initialTorpedoPos;

    public static bool bulletHitted=false;
    public static bool torpedoHitted = false;
    private bool torpedoShoot = false;
    private bool bulletShoot = false;
    private bool playerLock = false;
    private bool gotHit = false;
    private Vector3 relativePos;
    public float upperAxisLimit, lowerAxisLimit;
    private Renderer playerRenderer;
    public Light2D bodyLight, torchLight;
    // Start is called before the first frame update
    void Start()
    {
        
        initialBulletPos=bullet.position;
        initialTorpedoPos = torpedo.position;
        targetPosition = target.position;
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        playerRenderer = player.GetComponent<Renderer>();
        playerRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 0)
        {
            life = 0;
        }
        LevelManager.playerLife = life;
        relativePos = bubbleParticle.position;
        playerX =player.transform.position.x;
        if (!playerLock)
        {
            movement = Input.GetAxis("Horizontal");
        }
        else
        {
            player.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        

        targetPosition.x = target.position.x;

        

        if (movement > 0f)
        {
            player.velocity = new Vector2(movement * speed, player.velocity.y);
            torpedTarget.x = target.position.x;

        }

        vertical_movement = Input.GetAxis("Vertical");

        if(player.transform.position.y < lowerAxisLimit) {
            player.transform.position = new Vector3(player.transform.position.x, lowerAxisLimit,player.transform.position.z);
        }
        if (player.transform.position.y > upperAxisLimit)
        {
            player.transform.position = new Vector3(player.transform.position.x, upperAxisLimit, player.transform.position.z);
        }

        if (vertical_movement != 0)
        {
            player.velocity = new Vector2(player.velocity.x, vertical_movement * speed);
            
        }

        if (vertical_movement<0f)
        {
            bubbleParticle.rotation = Quaternion.Euler(new Vector3(0f, 0f, -47f));
            
        }
        else if (vertical_movement > 0f)
        {
            
            bubbleParticle.rotation = Quaternion.Euler(new Vector3(0f, 0f, 43f));
        }
        else if(movement!=0)
        {
            
            bubbleParticle.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else
        {

            bubbleParticle.rotation = Quaternion.Euler(new Vector3(0f, 0f, -15f));
        }

        playerActionPoint.position = new Vector2(player.transform.position.x+10f, playerActionPoint.position.y);
        playerHintPoint.position = new Vector2(player.transform.position.x + 17f, player.transform.position.y);

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!LevelManager.gamePause)
            {
                LevelManager.gamePause = true;
            }
            else
            {
                LevelManager.gamePause = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            
            if(bulletShoot==false)
            {

                //targetPosition= target.position;
                //bullet.position = bulletPoint.position;
                //bulletShoot = true;
                clonedTorpedo = Instantiate(bullet, bulletPoint.position, transform.rotation);
                AudioManager.Instance.PlaySound("bullet");

            }
        }
        if ((Input.GetKeyUp(KeyCode.LeftControl)||Input.GetKeyUp(KeyCode.RightControl))
            || (Input.GetKeyUp(KeyCode.LeftCommand) || Input.GetKeyUp(KeyCode.RightCommand)))
        {
            
            if (torpedoShoot == false)
            {
                if (clonedTorpedo == null)
                {
                    AudioManager.Instance.PlaySound("missile");
                    clonedTorpedo = Instantiate(torpedo, torpedoPoint.position, transform.rotation);
                }

            }
        }

        if (bulletShoot)
        {
            bullet.position = Vector3.MoveTowards(bullet.position,targetPosition,bulletSpeed*Time.deltaTime);
        }
        if (torpedoShoot)
        {
            torpedo.position = Vector3.MoveTowards(torpedo.position, torpedTarget, torpedoSpeed * Time.deltaTime);
        }

        if (bullet.position.x== targetPosition.x||bulletHitted==true)
        {
            bulletShoot= false;
            bullet.position = initialBulletPos;
            
        }

       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RedStar"&&!redStarCollision)
        {
            AudioManager.Instance.PlaySound("life");

            if (life > 0 && life < 20)
            {
                redStarCollision = true;
                StartCoroutine(addLife());
            };
        }
        
        if (collision.gameObject.tag == "Creatures" || collision.gameObject.tag == "Enemy")
        {
            life = life - 1;

            if (life <= 0)
            {
                StartCoroutine(gameOverDelay());
            }
            else
            {
                playerAnimation.SetBool("gotHit", true);
                AudioManager.Instance.PlaySound("playerHit");
            }
            


        }
        if ((collision.gameObject.tag == "EnemyTorpedo" || collision.gameObject.tag == "EnemyFastTorpedo")||
            collision.gameObject.tag == "BossTorpedo")
        {
            
                life = life - 1;
                if (life <= 0)
                {
                StartCoroutine(gameOverDelay());
                }
                else
                {


                    AudioManager.Instance.PlaySound("playerMissileHit");
                    playerAnimation.SetBool("gotHit", true);
                    StartCoroutine(endAnimationDelay("gotHit", 0.2f));

                }

            
            Destroy(collision.gameObject);
            
            
        }
        
        
        if (collision.gameObject.tag == "playerLockPoint")
        {
            playerLock= true;
            Boss.bossReady= true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Creatures" || collision.gameObject.tag == "Enemy"))
        {
            playerAnimation.SetBool("gotHit", false);
            
        }
    }

    private IEnumerator endAnimationDelay(string name, float time)
    {
        yield return new WaitForSeconds(time);
        playerAnimation.SetBool(name, false);
        gotHit = false;
    }

    private IEnumerator gameOverDelay()
    {
        bodyLight.intensity = 0f;
        torchLight.intensity = 0f;
        playerAnimation.SetBool("die", true);
        AudioManager.Instance.PlaySound("explosion");
        yield return new WaitForSeconds(1f);
        playerRenderer.enabled = false;
        AudioManager.Instance.PlaySound("gameOver");
        yield return new WaitForSeconds(1f);
        LevelManager.gameOver = true;
    }

    private IEnumerator addLife()
    {
        life++;
        
        yield return new WaitForSeconds(2f);
        redStarCollision = false;
    }

}
