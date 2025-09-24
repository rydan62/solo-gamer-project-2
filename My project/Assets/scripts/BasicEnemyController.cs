using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    public int health = 200;
    public int defense = 0;
    public int magic_defense = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        agent.destination = GameObject.Find("Player").transform.position;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "proj")
        {
            health = 20 - health;
        }
    }
}
