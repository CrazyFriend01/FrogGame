using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action OnDamaged;
    public static event Action OnDeath;

    public float HealthCurrent, MaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        HealthCurrent = MaxHealth;
    }

    public void TakeDamage(float dmg)
    {
        HealthCurrent -= dmg;
        OnDamaged?.Invoke();

        if (HealthCurrent <= 0)
        {
            HealthCurrent = 0;
            OnDeath?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
