using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Player player;
    private bool playerInSight;
    [SerializeField] private Slider healthBar;
    private int maxHealth = 10;
    private int currentHealth;
    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
    }
    public void ReduceHealth(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (!playerInSight)
            return;

        if (Vector3.Distance(transform.position, player.transform.position) > 10)
        {
            playerInSight = false;
            navMeshAgent.isStopped = true;
            return;
        }

        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            playerInSight = true;
        }
    }
}