using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isSword=false;
    Animator anim;
    public GameObject knifePrefab; // Ok prefabý
    public Transform spawnPos; // Oluþturulacak konum
    public float spawnInterval = 4f; // Oluþturma aralýðý (saniye)
    CharacterController characterController;
    private float timer = 0f;
    private void Start()
    {
        anim=GetComponent<Animator>();
        GameObject gameManager = GameObject.Find("Martial Hero");
        characterController = gameManager.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if(characterController.isAttack == true&&isSword==true)
        {
            Debug.Log("sa");
            Destroy(gameObject);
        }
        // Zamanlayýcýyý güncelle
        timer += Time.deltaTime;

        // Belirli bir aralýkta oluþturma
        if (timer >= spawnInterval)
        {
            

            StartCoroutine(SpawnArrow());
            timer = 0f;
        }
        knifePrefab.transform.position = new Vector2(1, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword" /*&& characterController.isAttack == true*/)
        {

            StartCoroutine(enterTrigger());
            
            
            Debug.Log("bulfu");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword" /*&& characterController.isAttack == true*/)
        {

            
            isSword = false;
            
            
        }
    }
    private IEnumerator enterTrigger()
    {
        yield return new WaitForSeconds(1f);
        isSword = true;
        //isSword = true;
        //yield return new WaitForSeconds(0.4f);
        //isSword = false;
    }




    private IEnumerator SpawnArrow()
    {
        anim.SetBool("atack", true);
       
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("atack", false);

        GameObject newArrow = Instantiate(knifePrefab, spawnPos.position, Quaternion.Euler(0f, 180f, 270f));
        yield return new WaitForSeconds(5f);
        Destroy(newArrow);

    }
}

