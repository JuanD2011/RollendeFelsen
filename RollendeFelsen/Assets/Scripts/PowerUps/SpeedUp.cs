using System.Collections;
using UnityEngine;

public class SpeedUp : PowerUp {

    private float increased = 0.3f;

    protected override void Start()
    {
        base.Start();
        mType = PowerUpType.speedUp;
        duration = 4f;

        if (actor != null) {
            if(actor is Enemy)
                StartCoroutine(IncreaseSpeed(actor as Enemy));
            else if(actor is Player)
                StartCoroutine(IncreaseSpeed(actor as Player));
        }
    }

    IEnumerator IncreaseSpeed(Enemy _enemy) {
        float enemySpeed = _enemy.Agent.speed;
        float amountToIncrease = enemySpeed * increased;
        _enemy.Agent.speed = enemySpeed + amountToIncrease;
        yield return new WaitForSeconds(duration);
        _enemy.Agent.speed = enemySpeed;
        Destroy(this);
    }

    IEnumerator IncreaseSpeed(Player _player) {
        _player.SpeedPU = true;
        yield return new WaitForSeconds(duration);
        _player.SpeedPU = false;
        Destroy(this);
    }
}
