using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float sideSpeed = 4f;
    [SerializeField] private LayerMask aimLayerMask;

    Interaction playerController;
    private float horizontal;
    private float vertical;
    private bool collision;
    
    private Rigidbody rigidBody;
    private float timer = 3f;
    
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<Interaction>();

    }

    private void Start()
    {
        playerController.OnObstacleCollided += PlayerController_OnObstacleCollided;
    }

    void Update()
    {
        //AimTowardMouse();
        horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));

        if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= sideSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }
        
        float velocityX = Vector3.Dot(movement.normalized, transform.right);
        
        animator.SetFloat("xInput", velocityX, 0.1f, Time.deltaTime);
        animator.SetFloat("zInput", 1, 0.1f, Time.deltaTime);

        if (collision == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                speed = 8f;
                collision = false;
                Debug.Log("speed is normal again");
                timer = 3f;
            }
        }


    }

    private void AimTowardMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask))
        {
            var direction = hitInfo.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
            transform.forward = direction;
        }
    }

    private void PlayerController_OnObstacleCollided(object sender, EventArgs e)
    {
        Debug.Log("Reduced speed!");
        speed /= 2;
        collision = true;

    }
}
