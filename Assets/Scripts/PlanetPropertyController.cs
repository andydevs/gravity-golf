using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlanetPropertyController : MonoBehaviour
{
    public const float MASS_TO_SCALE_FACTOR = 1.0f;

    // Start is called before the first frame update
    public float mass = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = mass * MASS_TO_SCALE_FACTOR * Vector2.one;
    }
}
