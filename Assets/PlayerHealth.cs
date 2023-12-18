using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    public float Health, HealthMax;

    // Start is called before the first frame update
    void Start()
    {
        Health = HealthMax;
    }

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        OnPlayerDamaged?.Invoke();

        if (Health <= 0)
        {
            Health = 0;
            OnPlayerDeath?.Invoke();
        }
    }
}
