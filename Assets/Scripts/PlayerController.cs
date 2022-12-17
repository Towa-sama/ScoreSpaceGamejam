using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float sideSpeed = 7f;
    [SerializeField] private Animator animator;
    public CharacterController characterController;
    
    private float horizontal;
    private float vertical;
    private Rigidbody rigidBody;
    private float timer = 2f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            transform.Translate(Vector3.forward * (movementSpeed * Time.deltaTime));
            animator.SetBool("isRunning", true);
        }
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0f, 0f).normalized;

        if (movement.magnitude >= 0.1f)
        {
            characterController.Move(movement * (sideSpeed * Time.deltaTime));
        }
    }
}
