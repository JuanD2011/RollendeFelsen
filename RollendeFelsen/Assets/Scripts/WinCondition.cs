using UnityEngine;

public class WinCondition : MonoBehaviour
{

    public delegate void Finish(Actor _actor);
    public event Finish OnFinish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Actor>() != null)
        {
            Actor actor = other.gameObject.GetComponent<Actor>();
            OnFinish(actor);
            if (other.gameObject.GetComponent<Enemy>() != null)
            {
                actor.gameObject.SetActive(false);
            }                
            else
                actor.enabled = false;
        }
    }

}
