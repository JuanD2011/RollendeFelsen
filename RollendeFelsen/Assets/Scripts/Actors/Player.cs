using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Actor
{
    private Rigidbody m_Rigidbody;
    private Animator m_Animator;

    private Vector2 input, inputDirection;
    private float targetRotation;

    [SerializeField] float moveSpeed, turnSmooth, powerUpSpeed, speedSmooothTime;
    float turnSmoothVel, currentSpeed, speedSmoothVel, targetSpeed, animationSpeedPercent;

    [SerializeField] private bool speedPU;
    private bool canAttack;
    private bool stun, stuned;

    [SerializeField]
    CapsuleCollider pushCapsule;

    public delegate void GameOver();
    public event GameOver OnGameOver;
    //public bool SpeedPU
    //{
    //    get
    //    {
    //        return speedPU;
    //    }
    //}

    private void Awake()
    {
        moveType = MoveTypes.Input;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        canAttack = true;
        pushCapsule.enabled = false;
        stun = false;
        stuned = false;
    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1"));
        if(Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            StartCoroutine(Push());
        }

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Stun());
            stun = true;
        }
        else {
            stun = false;
            pushCapsule.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        inputDirection = input.normalized;

        if (inputDirection != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVel, turnSmooth);
        }

        targetSpeed = ((speedPU) ? powerUpSpeed : moveSpeed) * inputDirection.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVel, speedSmooothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        animationSpeedPercent = ((speedPU) ? 1 : 0.5f) * inputDirection.magnitude;
        m_Animator.SetFloat("speed", animationSpeedPercent, speedSmooothTime, Time.deltaTime);
    }

    protected override IEnumerator Push()
    {
        pushCapsule.enabled = true;
        canAttack = false;
        m_Animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.5f);
        pushCapsule.enabled = false;
        m_Animator.SetBool("isAttacking", false);
        canAttack = true;
    }

    protected override IEnumerator Stun()
    {
        float time = 0f;
        while (stun) {
            time += Time.deltaTime;
            if (time > 2f && time < 2.02f)
            {
                pushCapsule.enabled = true;
                stuned = true;
            }
            else {
                stuned = false;
                pushCapsule.enabled = false;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rock>() != null) {
            Debug.Log("GameOver");
            OnGameOver();
            Time.timeScale = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Push other player
        if (other.gameObject.GetComponent<Actor>() != null && canAttack == false && pushCapsule.enabled == true)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 0f, ForceMode.Impulse);
            Debug.Log("Empujo");
        }

        if (other.gameObject.GetComponent<Actor>() != null && pushCapsule.enabled == true && stuned == true)
        {
            Debug.Log("Stuneo");
            stun = false;
            stuned = false;
        }
    }
}
