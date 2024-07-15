using System;
using UnityEngine;
using Random = System.Random;

public class PopUpDamage : MonoBehaviour
{
    public Vector2 InitialVelocity;
    public Rigidbody2D rb;
    public float lifetime = 1f;

    private void Start()
    {
        rb.velocity = new Vector2(InitialVelocity.x * RandomSide(), InitialVelocity.y);
        Destroy(gameObject, lifetime);
    }
    private int RandomSide()
    {
        Random rand = new Random();
        return rand.Next(2) == 0 ? 1 : -1;
    }
}