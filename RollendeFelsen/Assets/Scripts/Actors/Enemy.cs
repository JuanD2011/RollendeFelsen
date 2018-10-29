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
    private bool rockIsComing;
    private bool goForPU;
    [SerializeField] LayerMask rockLayer, powerUps;
    Vector3 destination;
    [SerializeField]
    Collider ground;

    public NavMeshAgent Agent
    {
        get
        {
            return agent;
        }

        set
        {
            agent = value;
        }
    }

    private void Start()
    {
        player = PlayerManager.instance.player.transform;
        target = PlayerManager.instance.target.transform;
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(Time.frameCount % 15 == 0)
        {
            distanceToPlayer = Vector3.Distance(player.localPosition, transform.localPosition);
            LookForPowerUps();
            LookForARock();
        }

        if (!hit || !rockIsComing || !goForPU)
        {
            Move(); 
        }

        if (rockIsComing)
        {
            if (Agent.remainingDistance > Agent.stoppingDistance)
            {
                Agent.SetDestination(destination);
            }
            else
            {
                if (Agent.remainingDistance <= Agent.stoppingDistance)
                {
                    rockIsComing = false;
                }
            } 
        }
        else if(goForPU)
        {
            if (Agent.remainingDistance > Agent.stoppingDistance)
            {
                Agent.SetDestination(destination);
            }
            else
            {
                if (Agent.remainingDistance <= 0.05f)
                {
                    goForPU = false;
                }
            }
        }
    }

    protected override void Move()
    {
        if (distanceToPlayer <= goPushRadius)
        {
            Agent.SetDestination(player.position);

            if (distanceToPlayer <= Agent.stoppingDistance)
            {
                LookAt(player);
                StartCoroutine(Interacting());
            }
        }
        else
        {
            Agent.SetDestination(target.position);
            LookAt(target);
        }

        animationSpeedPercent = (Agent.velocity.magnitude > 0.2f) ? 0.5f : 0;
        m_Animator.SetFloat("speed", animationSpeedPercent, speedSmooothTime, Time.deltaTime);
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

            destination = new Vector3(Mathf.Clamp(destination.x, ground.bounds.min.x + 0.2f, ground.bounds.max.x - 0.2f), destination.y, destination.z);

            //NavMeshHit navMeshHit;
            //if (NavMesh.FindClosestEdge(transform.localPosition, out navMeshHit, NavMesh.AllAreas))
            //{
            //    Debug.Log("Holi");
            //    agent.SetDestination(navMeshHit.position);
            //}
        }
    }

    private void LookForPowerUps()
    {
        RaycastHit raycastHit;

        if (Physics.SphereCast(transform.localPosition, lookForRockRadius, Vector3.forward, out raycastHit, lookForRockRadius * 2, powerUps))
        {
            Transform hitTransform = raycastHit.transform;
            destination = hitTransform.position;
            goForPU = true;
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
