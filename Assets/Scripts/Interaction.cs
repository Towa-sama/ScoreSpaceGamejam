using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public event EventHandler OnObstacleCollided;
    [SerializeField] private GameObject rockParticle;
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            gameObject.GetComponent<PlayerStats>().GetDamage();
            var particle = Instantiate(rockParticle, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(particle, 3f);
            Destroy(collision.gameObject);
            OnObstacleCollided?.Invoke(this, EventArgs.Empty);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<PlayerStats>().GetDamage();
            Destroy(collision.gameObject);
            OnObstacleCollided?.Invoke(this, EventArgs.Empty);
        }
    }
}
