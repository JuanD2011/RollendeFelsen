using System.Collections;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{

    [SerializeField]
    protected CapsuleCollider pushCapsule;

    protected Rigidbody m_Rigidbody;
    protected Animator m_Animator;

    protected float speedSmooothTime = 0.075f, animationSpeedPercent;

    protected bool hit;

    protected bool canStun;

    private bool immunity = false;

    public bool Immunity
    {
        set
        {
            immunity = value;
        }
    }

    private void Awake()
    {
        pushCapsule.enabled = false;

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        StartCoroutine(CanStun());
    }

    protected abstract void Move();

    private void Spawn(Actor _actor)
    {
        m_Animator.SetFloat("speed", 0, 0, Time.deltaTime);
        StartCoroutine(GameController.instance.SpawnPlayer(_actor));
    }

    protected IEnumerator Interacting()
    {
        if (!hit)
        {
            pushCapsule.enabled = true;
            m_Animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(0.2f);
            pushCapsule.enabled = false;
            m_Animator.SetBool("isAttacking", false);
        }
    }

    protected IEnumerator Hit()
    {
        hit = true;
        m_Animator.SetBool("hit", true);
        yield return new WaitForSeconds(0.5f);
        hit = false;
        m_Animator.SetBool("hit", false);
    }

    protected IEnumerator Stun(Actor _otherActor) {
        _otherActor.enabled = false;
        StartCoroutine(CanStun());
        yield return new WaitForSeconds(2);//Stun Time
        _otherActor.enabled = true;
    }

    protected IEnumerator CanStun()
    {
        canStun = false;
        yield return new WaitForSeconds(5);
        canStun = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Actor otherActor = other.gameObject.GetComponent<Actor>();

        if (otherActor != null && pushCapsule.enabled == true && !otherActor.immunity)
        {
            if (!canStun)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1f, ForceMode.Impulse);
                StartCoroutine(other.gameObject.GetComponent<Actor>().Hit());
                Debug.Log("Empujo");
            }
            else {
                StartCoroutine(Stun(otherActor));
                Debug.Log("Stun");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rock>() != null)
        {
            Actor actor = GetComponent<Actor>();

            if (actor != null)
            {
                Spawn(actor);
            }
        }

        if (collision.gameObject.GetComponent<PowerUp>() != null && collision.gameObject.GetComponent<Actor>() == null ) {
            Destroy(collision.gameObject);
            IPowerUp powerUp = collision.gameObject.GetComponent<IPowerUp>();
            powerUp.PickPowerUp(collision.gameObject.GetComponent<PowerUp>(), GetComponent<Actor>());
        }
    }
}
