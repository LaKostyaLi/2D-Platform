using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float value = 1f; //урон

    private void OnCollisionEnter2D(Collision2D col)
    {
        var health = col.gameObject.GetComponent<Health>();
        if(health!=null)
        {
            SetDamage(health);
        }
    }

    private void SetDamage(Health health )
    {
        health.CurrentValue -= value;
    }

}
