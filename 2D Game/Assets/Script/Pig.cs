using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float moveSpeed = 3f; // Hareket hýzý
    private bool isMovingRight = true; // Baþlangýçta saða doðru hareket eder.

    private void Update()
    {
        if (isMovingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnPoint")
        {

            isMovingRight = !isMovingRight;
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;
            

        }
       
    }
   
}
