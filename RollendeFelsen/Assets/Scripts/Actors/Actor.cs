using System.Collections;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IPowerUp {

    [SerializeField]
    protected CapsuleCollider pushCapsule;

    protected bool canAttack;

    protected Rigidbody m_Rigidbody;
    protected Animator m_Animator;

    protected float moveVel;
    protected MoveTypes moveType;

    private void Awake()
    {
        pushCapsule.enabled = false;
        canAttack = true;

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    protected abstract void Move();

    protected IEnumerator Push() {
        pushCapsule.enabled = true;
        canAttack = false;
        m_Animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.5f);
        pushCapsule.enabled = false;
        m_Animator.SetBool("isAttacking", false);
        canAttack = true;
    }

    protected IEnumerator Stun() {
        yield return null;
    }

    public void PickPowerUp(PowerUp _powerUp)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Push other player
        if (other.gameObject.GetComponent<Actor>() != null && canAttack == false && pushCapsule.enabled == true)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
            Debug.Log("Empujo");
        }

        /*if (other.gameObject.GetComponent<Actor>() != null && pushCapsule.enabled == true && stuned == true)
        {
            Debug.Log("Stuneo");
        }*/
    }
}
