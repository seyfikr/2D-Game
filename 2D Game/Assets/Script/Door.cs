using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform Player;
    Animator anim;
    [SerializeField] public bool door = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {


            anim.SetBool("Door", true);
            StartCoroutine(enterTrigger());

        }
        
    }
    private IEnumerator enterTrigger()
    {
        yield return new WaitForSeconds(2f);
        Player.transform.position = new Vector3(-12.98f, -26.78f, 0);
        door= true; 
        
    }
}
