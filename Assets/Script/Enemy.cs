using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviourPun
{
    public float Health = 100;
    float currentHitPoints;

    public void Start()
    {
        currentHitPoints = Health;
    }
    [PunRPC] public void ReduceHealth (float damage)
    {
        currentHitPoints -= damage;
        if (currentHitPoints <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    /*private void OnCollisionEnter2D(Collision2D collision, float damage)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Destroy(collision.gameObject);
            if (photonView.IsMine)
                Health -= damage;
            else
                Health -= damage;
        }
    }*/
    
}