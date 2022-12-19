using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public event EventHandler OnObstacleCollided;
    [SerializeField] private GameObject rockParticle;
    [SerializeField] private AudioClip rockHitEffect;
    [SerializeField] private AudioClip entityHitEffect;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            audioSource.PlayOneShot(rockHitEffect);
            var particle = Instantiate(rockParticle, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(particle, 3f);
            Destroy(collision.gameObject);
            gameObject.GetComponent<PlayerStats>().GetDamage();
            OnObstacleCollided?.Invoke(this, EventArgs.Empty);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(entityHitEffect);
            gameObject.GetComponent<PlayerStats>().GetDamage();
            Debug.Log($"Collided with {collision.gameObject.name}");
            OnObstacleCollided?.Invoke(this, EventArgs.Empty);
        }
    }
}
