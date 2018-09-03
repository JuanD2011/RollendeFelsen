using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IPowerUp {
    protected float moveVel;
    protected MoveTypes moveType;

    protected abstract void Movement();
    protected abstract void Stun();
    protected abstract void Push();

    public void PickPowerUp(PowerUp _powerUp)
    {
        throw new System.NotImplementedException();
    }
}
