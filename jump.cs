using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public int forceConst = 50;

private bool canJump;
private RigidBody selfRigidbody;

void Start()
{
    selfRigidbody = GetComponent<RigidBody>();
}

void FixedUpdate()
{
    if (canJump)
    {
        canJump = false;
        selfRigidbody.addForce(0, forceConst, 0, ForceMode.Impulse);
    }
}

void Update()
{
    if (Input.GetKeyUp(Keycode.SPACE))
    {
        canJump = true;
    }
