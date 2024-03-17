using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public int health = 100; // Example health value

    public void TakeDamage(int damage)
    {
        health -= damage;
        // Handle other logic related to taking damage, e.g., checking for death
    }

}

