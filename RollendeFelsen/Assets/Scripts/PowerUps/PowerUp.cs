using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour{

    PowerUp m;

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisioned = other.gameObject;

        if (collisioned.GetComponent<IPowerUp>() != null) {
            IPowerUp iPowerUp;
            iPowerUp = collisioned.GetComponent<IPowerUp>();
            iPowerUp.PickPowerUp(m);
        }
    }
}
