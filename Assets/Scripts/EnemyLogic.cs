using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private Animator animator;

    private float speed = 6f;
    private float rotationSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 10)
        {
            FollowPlayer();
            animator.SetBool("isPlayerVisible", true);
        }
    }

    private void FollowPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Rock"))
        {
            Destroy(gameObject);
        }
    }
}