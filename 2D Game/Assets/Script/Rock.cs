using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
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

        }
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(this.transform);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);

        }
    }
}
