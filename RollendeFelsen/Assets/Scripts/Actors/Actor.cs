using System.Collections;
using UnityEngine;

public abstract class Actor : MonoBehaviour{

    [SerializeField]
    protected CapsuleCollider pushCapsule;

    protected bool canAttack;

    protected Rigidbody m_Rigidbody;
    protected Animator m_Animator;

    protected float moveVel;
    protected MoveTypes moveType;

    protected float speedSmooothTime = 0.075f, animationSpeedPercent;

    protected bool hit;

    protected bool canStun;

    public delegate void Race(Actor _actor);
    public static event Race OnDeath;

    private void Awake()
    {
        pushCapsule.enabled = false;
        canAttack = true;

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        StartCoroutine(CanStun());
    }

    protected abstract void Move();

    protected IEnumerator Interacting()
    {
        if (!hit)
        {
            pushCapsule.enabled = true;
            canAttack = false;
            m_Animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(0.2f);
            pushCapsule.enabled = false;
            m_Animator.SetBool("isAttacking", false);
            canAttack = true; 
        }
    }

    protected IEnumerator CanStun()
    {
        canStun = false;
        yield return new WaitForSeconds(5);
        canStun = true;
    }

    protected IEnumerator Hit()
    {
        hit = true;
        m_Animator.SetBool("hit", true);
        yield return new WaitForSeconds(0.5f);
        hit = false;
        m_Animator.SetBool("hit", false);
    }

    /// <summary>
    /// Should Stun the Actor for seconds
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Stun(Actor _otherActor) {
        _otherActor.enabled = false;
        StartCoroutine(CanStun());
        yield return new WaitForSeconds(2);//Stun Time
        _otherActor.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Push other Actor
        if (other.gameObject.GetComponent<Actor>() != null && canAttack == false && pushCapsule.enabled == true && canStun == false)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1f, ForceMode.Impulse);
            StartCoroutine(other.gameObject.GetComponent<Actor>().Hit());
            Debug.Log("Empujo");
        }

        //Stun other Actor
        if (other.gameObject.GetComponent<Actor>() != null && canAttack == false && pushCapsule.enabled == true && canStun == true)
        {
            Actor actor = other.gameObject.GetComponent<Actor>();
            StartCoroutine(Stun(actor));
            Debug.Log("Stun");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rock>() != null)
        {
            if (GetComponent<Enemy>() != null) {
                Enemy enemy = gameObject.GetComponent<Enemy>();
                OnDeath(enemy);
                Destroy(gameObject);
            }
            if (GetComponent<Player>() != null)
            {
                Player player = GetComponent<Player>();
                StartCoroutine(player.Spawn());
            }
        }
    }
}
