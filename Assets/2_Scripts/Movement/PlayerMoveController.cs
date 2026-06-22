using System;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CharacterController cc;
    [SerializeField] private float speed = 10.0f;

    private Vector3 _dir;
    private Vector3 _velocity = Vector3.zero;
    private float _gravity = -9.81f;
    
    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var input = new Vector3(h, 0, v);
        
        // 수평 이동
        var horizontal = input * speed;
        
        // 중력 누적
        if(cc.isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;
        else
            _velocity.y += _gravity * Time.deltaTime;

        cc.Move((horizontal + _velocity) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //rb.AddForce(_dir * speed);
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
