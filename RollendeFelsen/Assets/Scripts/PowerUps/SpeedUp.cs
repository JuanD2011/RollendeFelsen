using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : PowerUp {

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Actor>() != null) {
        }
    }

}
