using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static int health = 1000000;

    [SerializeField] private GameObject ragdoll;
    [SerializeField] private TextMeshProUGUI shieldCount;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shield2;
    private bool hasDog;
    [SerializeField] private GameObject deathParticle;

    public bool HasDog
    {
        get => hasDog;
        set => hasDog = value;
    }

    private void Start()
    {
        shieldCount.text = "2/2";
    }

    void Update()
    {
        if (health <= 0)
        {
            var rg = Instantiate(ragdoll, gameObject.transform.position, Quaternion.identity);
            Destroy(GameObject.Find("spawnPlane"));
            ApplyExplosionToRagdoll(rg.transform, 900f, transform.position - new Vector3(0f, 0f, 5f), 10f);
            Destroy(gameObject);
        }
    }

    public void GetDamage()
    {
        if (hasDog)
        {
            var dog = GameObject.FindWithTag("Dog");
            Destroy(dog);
            hasDog = false;
            var spawnPlane = GameObject.Find("spawnPlane");
            spawnPlane.GetComponent<SpawnEntities>().DogSpawned = false;
            var particles = Instantiate(deathParticle,
                transform.parent.position -
                new Vector3(transform.position.x, transform.position.y, transform.position.z - 2f),
                Quaternion.identity);
            Destroy(particles, 3f);
        }
        else
        {
            health -= 1;
            if (health == 2)
            {
                shield.SetActive(false);
            }

            if (health == 1)
            {
                shield2.SetActive(false);
            }

            shieldCount.text = (health - 1) + "/2";
            Debug.Log("current health: " + health);
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