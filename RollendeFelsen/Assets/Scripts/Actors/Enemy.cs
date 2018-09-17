using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Actor
{
    [SerializeField] private float goPushRadius, lookForRockRadius;

    private Transform player;
    private NavMeshAgent agent;
    private float distanceToPlayer;
    private float distanceToRock;
    private Transform target;
    Vector3 direction;
    Quaternion lookRotation;
    bool rockIsComing;
    [SerializeField] LayerMask rockLayer;
    Vector3 destination;
    [SerializeField]
    Collider ground;

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
        target = PlayerManager.instance.target.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(Time.frameCount % 30 == 0)
        {
            distanceToPlayer = Vector3.Distance(player.localPosition, transform.localPosition);
        }
        if (Time.frameCount % 60 == 0)
        {
            LookForARock();
        }

        Move();

        if(rockIsComing && agent.remainingDistance > agent.stoppingDistance)
        {
            agent.SetDestination(destination);
        }
        else
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                rockIsComing = false;
            }
        }
    }

    protected override void Move()
    {
        if (!hit || !rockIsComing)
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

    private void LookForARock()
    {
        RaycastHit raycastHit;

        if(Physics.SphereCast(transform.localPosition, lookForRockRadius, Vector3.forward, out raycastHit, lookForRockRadius * 2, rockLayer))
        {
            rockIsComing = true;

            if(raycastHit.transform.position.x > transform.localPosition.x)
            {
                destination = transform.localPosition + new Vector3(-3.5f, 0, 1);
            }
            else
            {
                destination = transform.localPosition + new Vector3(3.5f, 0, 1);
            }

            destination = new Vector3(Mathf.Clamp(destination.x, ground.bounds.min.x + 0.1f, ground.bounds.max.x - 0.1f), destination.y, destination.z);

            Debug.Log("Ols");
            //NavMeshHit navMeshHit;
            //if (NavMesh.FindClosestEdge(transform.localPosition, out navMeshHit, NavMesh.AllAreas))
            //{
            //    Debug.Log("Holi");
            //    agent.SetDestination(navMeshHit.position);
            //}
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
