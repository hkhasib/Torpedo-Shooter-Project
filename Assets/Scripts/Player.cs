using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float speed = 5f;
    public float movement = 0f;
    public float bulletSpeed = 20f;
    public float torpedoSpeed = 5f;
    public Transform bullet;
    public Transform torpedo;
    public Transform target;
    public Transform torpedoPoint;
    public Transform bulletPoint;
    public float vertical_movement = 0f;

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

    public float upperAxisLimit, lowerAxisLimit;
    // Start is called before the first frame update
    void Start()
    {
        initialBulletPos=bullet.position;
        initialTorpedoPos = torpedo.position;
        targetPosition = target.position;
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerX=player.transform.position.x;
        movement = Input.GetAxis("Horizontal");

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

        playerActionPoint.position = new Vector2(player.transform.position.x+10f, playerActionPoint.position.y);
        playerHintPoint.position = new Vector2(player.transform.position.x + 17f, player.transform.position.y);
        

        if(Input.GetKeyUp(KeyCode.Space)) {
            
            if(bulletShoot==false)
            {

                //targetPosition= target.position;
                //bullet.position = bulletPoint.position;
                //bulletShoot = true;
                clonedTorpedo = Instantiate(bullet, bulletPoint.position, transform.rotation);
                AudioManager.Instance.PlaySound("bullet");

            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
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
        if (collision.gameObject.tag == "Creatures" || collision.gameObject.tag == "Enemy")
        {
            life = life - 1;
            playerAnimation.SetBool("gotHit", true);
            AudioManager.Instance.PlaySound("playerHit");


        }
        if (collision.gameObject.tag == "EnemyTorpedo")
        {
            playerAnimation.SetBool("gotHit", true);
            Destroy(collision.gameObject);
            StartCoroutine(endAnimationDelay("gotHit", 0.2f));
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
    }

    

}
