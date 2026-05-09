using UnityEngine;
using UnityEngine.AI;

// kamikaze strategy: when enemy get close to target, he speed up.
public class KamikazeMovement : MonoBehaviour, IMovementStrategy
{
    [SerializeField] private float baseSpeed = 3f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float boostDistance = 8f; // how close he start speed up

    private NavMeshAgent agent;

    private void Awake()
    {
        agent =GetComponent<NavMeshAgent>();
    }

    public void Move(Transform self, Transform target)
    {
        if (agent == null|| target == null) return;

        // calculate distance to target
        float distance =Vector3.Distance(self.position, target.position);

        // if close, speed up. if far, normal speed.
        if (distance < boostDistance)
        {
            float t = 1f - (distance / boostDistance); // 0 when far, 1 when very close
            agent.speed = Mathf.Lerp(baseSpeed, maxSpeed, t);
        }
        else
        {
            agent.speed= baseSpeed;
        }

        agent.SetDestination(target.position);
    }

    // kamikaze NEVER stop, he run into the core
    public bool ShouldStopAtRange(float distance)
    {
        return false;
    }
}