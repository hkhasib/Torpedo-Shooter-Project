using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStar : MonoBehaviour
{
    private float rotZ;
    public float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        rotateStar();
    }

    private void rotateStar()
    {
        rotZ += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
