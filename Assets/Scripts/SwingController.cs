using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = 8*Vector2.right;
    }
}
