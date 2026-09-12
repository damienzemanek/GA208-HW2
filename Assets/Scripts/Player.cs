using System;
using EMILtools.Extensions;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpScalar;
    public Rigidbody2D rb;
    public PhysEX.GroundedSettings groundedSettings;
    
    
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) Jump(); 
    }

    void Jump()
    {
        if (!transform.IsGrounded2D(ref groundedSettings)) return;
        rb.AddForce(transform.up * jumpScalar, ForceMode2D.Impulse);
    }
    
    
}
