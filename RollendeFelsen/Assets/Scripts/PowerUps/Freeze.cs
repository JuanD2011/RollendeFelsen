using System.Collections;
using UnityEngine;

public class Freeze : PowerUp {

    protected override void Start()
    {
        base.Start();
        mType = PowerUpType.freeze;
        duration = 5f;

        if (actor != null)
        {
            if (actor is Enemy)
                StartCoroutine(FreezeActor(actor as Enemy));
            else if (actor is Player)
                StartCoroutine(FreezeActor(actor as Player));
        }
    }

    IEnumerator FreezeActor(Enemy _enemy) {
        _enemy.Agent.isStopped = true;
        yield return new WaitForSeconds(duration);
        _enemy.Agent.isStopped = false;
        Destroy(this);
    }

    IEnumerator FreezeActor(Player _player) {
        float playerSpeed = _player.MoveSpeed;
        _player.MoveSpeed = 0f;
        yield return new WaitForSeconds(duration);
        _player.MoveSpeed = playerSpeed;
        Destroy(this);
    }
}
