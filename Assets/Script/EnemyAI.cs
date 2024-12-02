using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Public variables
    public Transform player;
    public float moveSpeed = 3f;
    private bool move = false;

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the enemy should move
        if (distanceToPlayer < 12.5f){
            move = true;
        }
        
        if (move){
            // Calculate the direction to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position += directionToPlayer * moveSpeed * Time.deltaTime;

            // Make the enemy face the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }
}
