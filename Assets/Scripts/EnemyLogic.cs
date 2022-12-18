using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private Animator animator;
    private GameObject root;
    private bool isDead = false;
    private float speed = 2f;
    private float rotationSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        root = transform.Find("Root").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 10)
            {
                FollowPlayer();
                animator.SetBool("isPlayerVisible", true);
            }
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
        if (collision.transform.CompareTag("Rock") || collision.transform.CompareTag("Player"))
        {
            isDead = true;
            animator.enabled = false;
            root.SetActive(true);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            ApplyExplosionToRagdoll(gameObject.transform, 5000f, transform.position - new Vector3(0f, 0f, 5f), 10);
            Destroy(gameObject, 5f);
        }
    }

    private void ApplyExplosionToRagdoll(Transform root, float explosionForce, Vector3 explosionPosition,
        float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidBody))
            {
                childRigidBody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
            }

            ApplyExplosionToRagdoll(child, explosionForce, explosionPosition, explosionRange);
        }
    }
}