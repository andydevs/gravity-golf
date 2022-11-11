using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    // Helper variables
    private int ballMask;

    // Start is called before the first frame update
    void Start()
    {
        ballMask = LayerMask.GetMask("Ball");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if ((ballMask & 1<<collision.gameObject.layer) != 0)
        {
            Debug.Log("A ball is inside me!");
            PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
            player.SendMessage("OnHole");
        }
    }
}
