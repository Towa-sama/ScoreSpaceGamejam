using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static int health = 1;
    private int shieldHealth = 2;

    [SerializeField] private GameObject ragdoll;
    [SerializeField] private TextMeshProUGUI shieldCount;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shield2;

    private void Start()
    {
        shieldCount.text = "2/2";
    }

    void Update()
    {
        if (health <= 0)
        {
            var rg = Instantiate(ragdoll, gameObject.transform.position, Quaternion.identity);
            ApplyExplosionToRagdoll(rg.transform, 900f, transform.position - new Vector3(0f, 0f, 5f), 10f);
            Destroy(gameObject);
        }
    }

    public void GetDamage()
    {
        if (shieldHealth > 0)
        {
            if (shieldHealth == 2)
            {
                shield.SetActive(false);
            }

            if (shieldHealth == 1)
            {
                shield2.SetActive(false);
            }
            shieldHealth -= 1;
            shieldCount.text = shieldHealth + "/2";
        }
        else
        {
            health -= 1;
        }
        Debug.Log("current shield health: "+ shieldHealth);
        Debug.Log("current health: "+ health);
    }
    
    private void ApplyExplosionToRagdoll(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange)
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
