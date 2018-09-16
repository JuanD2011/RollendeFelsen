using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    PowerUp m;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IPowerUp>() != null)
        {
            IPowerUp iPowerUp;
            iPowerUp = other.GetComponent<IPowerUp>();
            iPowerUp.PickPowerUp(PickedPU());
        }
    }

    protected virtual PowerUp PickedPU()
    {
        PowerUp _powerUp = null;

        return _powerUp;
    }
}
