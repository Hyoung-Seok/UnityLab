using System;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 10.0f;

    private Vector3 _dir;
    
    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        
        _dir = new Vector3(h, 0, v).normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(_dir * speed);
    }

    private void TransformMove(float h, float v)
    {
        var dir = new Vector3(h, 0, v).normalized;
        transform.position += dir * (speed * Time.deltaTime);
    }

    private void RigidbodyMovePosition()
    {
        rb.MovePosition(rb.position + _dir * (speed * Time.fixedDeltaTime));
    }
    
    private void RigidbodyLinearVelocity()
    {
        var v = _dir * speed;
        v.y = rb.linearVelocity.y;

        rb.linearVelocity = v;
    }
}
