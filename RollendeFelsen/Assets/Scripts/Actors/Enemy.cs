using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Actor
{
    [SerializeField]
    private float goPushRadius = 10f;

    private Transform player;
    private NavMeshAgent agent;
    private int interval = 10;
    private float distanceToPlayer;
    private Transform target;
    Vector3 direction;
    Quaternion lookRotation;

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
        target = PlayerManager.instance.target.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(player.localPosition, transform.localPosition);
        //if(Time.frameCount % interval == 0)
        //{
        //}
        Move();
    }

    protected override void Move()
    {
        if (!hit)
        {
            if (distanceToPlayer <= goPushRadius)
            {
                agent.SetDestination(player.position);

                if (distanceToPlayer <= agent.stoppingDistance)
                {
                    LookAt(player);
                    StartCoroutine(Interacting());
                }
            }
            else
            {
                agent.SetDestination(target.position);
                LookAt(target);
            }

            animationSpeedPercent = (agent.velocity.magnitude > 0.2f) ? 0.5f : 0;
            m_Animator.SetFloat("speed", animationSpeedPercent, speedSmooothTime, Time.deltaTime); 
        }
    }

    private void LookAt(Transform _target)
    {
        direction = (_target.position - transform.localPosition).normalized;
        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, goPushRadius);
    }
}
