using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogLogic : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    private bool isPicked = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPicked)
        {
            transform.position = new Vector3(player.transform.position.x, 0.1f, player.transform.position.z - 2f);
            return;
        }

        if (player.transform.position.z - gameObject.transform.position.z > 20)
        {
            GameObject.Find("spawnPlane").GetComponent<SpawnEntities>().DogSpawned = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            isPicked = true;
            anim.SetBool("isPicked", true);
        }
    }
}