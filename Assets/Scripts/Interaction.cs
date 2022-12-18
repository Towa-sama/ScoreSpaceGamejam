using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    [SerializeField] private GameObject rockParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            var particle = Instantiate(rockParticle, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(particle, 3f);
            Destroy(collision.gameObject);
        }
    }
}