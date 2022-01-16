using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Gravitator : MonoBehaviour
{
    public const float G = 1000.0f;

    public float mass = 1.0f;

    private Rigidbody2D golfball;
    private CircleCollider2D surface;

    void Start()
    {
        // Find components
        golfball = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<Rigidbody2D>();
        surface = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        // Gravity.... it idk
        Vector2 relative = transform.position + (Vector3)surface.offset - golfball.transform.position;
        float gravity = G * mass * golfball.mass / relative.sqrMagnitude;
        golfball.AddForce(gravity * relative.normalized);
    }
}
