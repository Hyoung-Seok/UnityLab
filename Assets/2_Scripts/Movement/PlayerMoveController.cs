using System;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    
    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        
        TransformMove(h, v);
    }

    private void TransformMove(float h, float v)
    {
        var dir = new Vector3(h, 0, v).normalized;
        transform.position += dir * (speed * Time.deltaTime);
    }
}
