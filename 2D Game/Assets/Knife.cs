using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private bool isSwordBoxCollider = false;
    public float speed = 5f; // Okun hýzý
    CharacterController characterController;
    private void Start()
    {
        GameObject gameManager = GameObject.Find("Martial Hero");
        characterController= gameManager.GetComponent<CharacterController>();
    }
    private void Update()
    {
        //gameObject.transform.rotation = new Vector3(0, 0, 180);
        Vector2 movement = new Vector2(0, speed);
        transform.Translate(movement * Time.deltaTime);
        if (characterController.isAttack == true&&isSwordBoxCollider==true)
        {
            Debug.Log("eeee");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword"/*&&characterController.isAttack==true*/)
        {
            Debug.Log("a");
            StartCoroutine(waitSword());
        }
    }
    private IEnumerator waitSword()
    {
        isSwordBoxCollider = true;
        yield return new WaitForSeconds(0.4f);
        isSwordBoxCollider = false;
    }
}
