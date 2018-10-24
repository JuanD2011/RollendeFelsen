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

    public bool SpeedPU
    {
        get
        {
            return speedPU;
        }

        set
        {
            speedPU = value;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1"));
        if(Input.GetKeyDown(KeyCode.Space) && pushCapsule.enabled == false)
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

            targetSpeed = ((SpeedPU) ? powerUpSpeed : MoveSpeed) * inputDirection.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVel, speedSmooothTime);

            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
            animationSpeedPercent = ((SpeedPU) ? 1 : 0.5f) * inputDirection.magnitude;
            m_Animator.SetFloat("speed", animationSpeedPercent, speedSmooothTime, Time.deltaTime); 
        }
    }
}
