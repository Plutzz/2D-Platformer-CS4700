using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBase : MonoBehaviour
{
    public Vector3 velocity;
    public float gravityFactor;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = 9.81f * gravityFactor * Vector2.down;
        velocity += acceleration * Time.deltaTime;
        Movement(velocity * Time.deltaTime);
    }

    public void Movement(Vector3 move)
    {
        if (move.magnitude < 0.0001f) return;
        
        RaycastHit2D[] results = new RaycastHit2D[16];
        // Can be optimized by caching the rigidbody
        int cnt = rb.Cast(move, results, move.magnitude + 0.01f);

        if (cnt > 0) return;

        transform.position += move;
    }
}
