using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    public float speed;
    public bool isPlayerBuffed = false;
    private float buffDuration = 5.0f;
    private float freezeDuration = 2.0f;
    private float freezeTimer = 0.0f;
    private float freezeForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerBuffed)
        {
            // Player is buffed, freeze the enemy
            MoveAwayFromPlayer();
        }
        else
        {
            // Player is not buffed, normal behavior
            FollowPlayer();
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            ScoreManager.scoreCount += 1;
        }

    }

    private void FollowPlayer()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
    }

    private void MoveAwayFromPlayer()
    {
        // Calculate the direction away from the player
        Vector3 awayDirection = (transform.position - player.transform.position).normalized;

        // Apply a force in the opposite direction
        enemyRb.AddForce(awayDirection * freezeForce);
    }

    public void ApplyBuff()
    {
        // Called when the player receives the status buff (e.g., when colliding with super_pickup)
        isPlayerBuffed = true;
        Debug.Log("Player buffed");

        // Start a timer to revert the buff after buffDuration
        Invoke("RevertBuff", buffDuration);
    }

    private void RevertBuff()
    {
        // Called after buffDuration to revert the buff
        isPlayerBuffed = false;
    }
}
