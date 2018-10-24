using System.Collections;
using UnityEngine;

public class Invencibility : PowerUp {

    protected override void Start()
    {
        base.Start();
        mType = PowerUpType.invencibility;
        duration = 4f;

        if (actor != null)
        {
            StartCoroutine(ImmunityActor(actor));
        }
    }

    IEnumerator ImmunityActor(Actor _actor) {
        _actor.Immunity = true;
        yield return new WaitForSeconds(duration);
        _actor.Immunity = false;
        Destroy(this);
    }
}
