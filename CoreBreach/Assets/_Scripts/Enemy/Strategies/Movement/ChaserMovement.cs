using UnityEngine;
using UnityEngine.AI;

// chaser strategy: enemy run straight to target with normal speed.
// most simple movement, just go go go.
public class ChaserMovement : MonoBehaviour, IMovementStrategy
{
    [SerializeField] private float speed = 3.5f;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent =GetComponent<NavMeshAgent>();
        if (agent!= null)
        {
            agent.speed = speed;
        }
    }

    // enemy call this every frame
    public void Move(Transform self,Transform target)
    {
        if (agent == null ||target == null) return;
        agent.SetDestination(target.position);
    }

    // chaser dont stop, he keep running until he can attack
    public bool ShouldStopAtRange(float distance)
    {
        return false;
    }
}