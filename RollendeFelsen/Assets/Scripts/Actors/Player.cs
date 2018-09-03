using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Actor
{
    Rigidbody mRigidBody;

    private void Start()
    {
        moveType = MoveTypes.Input;
        moveVel = 3000f;
        mRigidBody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMove, 0, verticalMove);

        mRigidBody.AddForce(movement * moveVel * Time.deltaTime);
    }

    protected override void Movement()
    {
        
    }

    protected override void Push()
    {
        throw new System.NotImplementedException();
    }

    protected override void Stun()
    {
        throw new System.NotImplementedException();
    }
}
