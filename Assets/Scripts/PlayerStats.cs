using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static int health = 3;

    [SerializeField] private GameObject ragdoll;
    

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
        health -= 1;
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
