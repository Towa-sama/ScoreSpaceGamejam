using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static int health = 3;
    private bool hasDog = false;
    [SerializeField] private GameOverSequence gameOver;
    
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private TextMeshProUGUI shieldCount;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shield2;
    [SerializeField] private GameObject deathParticle;
    private TMP_Text scoreText;
    private static string playerName = "Unknown";
    private int score;
    private Leaderboard _leaderboard;

    public static string PlayerName
    {
        get => playerName;
        set => playerName = value;
    }

    private void Awake()
    {
        gameOver.TurnDeathScreenOff();
    }

    private void Start()
    {
        _leaderboard = new Leaderboard();
        health = 3;
        shieldCount.text = "2/2";
        gameOver.gameObject.SetActive(false);
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
    }

    public bool HasDog
    {
        get => hasDog;
        set => hasDog = value;
    }
    
    void Update()
    {
        score = (int) transform.position.z;
        scoreText.SetText($"Score: {score} m");
        if (health <= 0)
        {
            var rg = Instantiate(ragdoll, gameObject.transform.position, Quaternion.identity);
            Destroy(GameObject.Find("spawnPlane"));
            ApplyExplosionToRagdoll(rg.transform, 900f, transform.position - new Vector3(0f, 0f, 5f), 10f);
            StartCoroutine(_leaderboard.SubmitScoreRoutine(score));
            Destroy(gameObject);
            gameOver.TurnDeathScreen();
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