using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlanetPropertyController))]
public class Gravitator : MonoBehaviour
{
    public const float G = 2000.0f;

    private Rigidbody2D golfball;
    private CircleCollider2D surface;
    private PlanetPropertyController planet;

    void Start()
    {
        // Find components
        golfball = GameObject
            .FindGameObjectWithTag("Ball")
            .GetComponent<Rigidbody2D>();
        surface = GetComponent<CircleCollider2D>();
        planet = GetComponent<PlanetPropertyController>();
    }

    void FixedUpdate()
    {
        // Gravity... it idk
        if (golfball) golfball.AddForce(ComputeGravityForce(golfball));
    }

    public void SetGolfbol(Rigidbody2D newGolfBol)
    {
        golfball = newGolfBol;
    }

    public Vector2 ComputeGravityForce(Rigidbody2D rigidbody)
    {
        Vector2 relative = transform.position + (Vector3)surface.offset - rigidbody.transform.position;
        float gravity = G * planet.mass * rigidbody.mass / relative.sqrMagnitude;
        return gravity * relative.normalized;
    }
}
