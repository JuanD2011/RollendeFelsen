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
        SkinnedMeshRenderer mskinnedMeshRenderer = _enemy.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        Color colorBase = mskinnedMeshRenderer.material.color;
        mskinnedMeshRenderer.material.color = Color.white;
        _enemy.Agent.isStopped = true;
        yield return new WaitForSeconds(duration);
        mskinnedMeshRenderer.material.color = colorBase;
        _enemy.Agent.isStopped = false;
        Destroy(this);
    }

    IEnumerator FreezeActor(Player _player) {
        SkinnedMeshRenderer mskinnedMeshRenderer = _player.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        Color colorBase = mskinnedMeshRenderer.material.color;
        float playerSpeed = _player.MoveSpeed;
        mskinnedMeshRenderer.material.color = Color.white;
        _player.MoveSpeed = 0f;
        yield return new WaitForSeconds(duration);
        mskinnedMeshRenderer.material.color = colorBase;
        _player.MoveSpeed = playerSpeed;
        Destroy(this);
    }
}
