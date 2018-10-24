using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour,IPowerUp
{
    protected PowerUpType mType;
    protected float duration;

    protected Actor actor;

    protected virtual void Start() {
        actor = GetComponent<Actor>();
    }

    public void PickPowerUp(PowerUp _powerUp, Actor actor)
    {
        switch (_powerUp.mType)
        {
            case PowerUpType.speedUp:
                if(actor.GetComponent<SpeedUp>() == null)
                    actor.gameObject.AddComponent<SpeedUp>();
                break;
            case PowerUpType.freeze:
                ActorsToFreeze(actor);
                break;
            case PowerUpType.invencibility:
                if (actor.GetComponent<Invencibility>() == null)
                    actor.gameObject.AddComponent<Invencibility>();
                break;
            default:
                break;
        }
    }

    private void ActorsToFreeze(Actor _actor) {
        List<Actor> actorsToFreeze = GameController.instance.players;
        actorsToFreeze.Remove(_actor);
        foreach (Actor a in actorsToFreeze) {
            if(a.gameObject.GetComponent<Freeze>() == null)
                a.gameObject.AddComponent<Freeze>();
        }
    }
}
