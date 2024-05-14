using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windzone2d : MonoBehaviour
{
 public float windStrength = 5f; // Strength of the wind

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Gradually alter the player's X coordinate
            Vector2 newPosition = rb.position;
            newPosition.x += windStrength * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
    }
}
