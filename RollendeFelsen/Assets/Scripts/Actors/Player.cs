using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : Actor
{
    private Vector2 input, inputDirection;
    private float targetRotation;

    [SerializeField] float moveSpeed, turnSmooth, powerUpSpeed;
    float turnSmoothVel, currentSpeed, speedSmoothVel, targetSpeed;

    [SerializeField] private bool speedPU;

    /*private void Awake()
    {
        moveType = MoveTypes.Input;
    }*/

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
            StartCoroutine(Interacting());
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        if (!hit)
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
    }
}
