using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{

    public static Action<Enemy> OnEndReached;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float loot = 3;

    [SerializeField] private AudioSource dead;


    // Move Speed Of our enemy
    public float MoveSpeed { get; set; }

    // Waypoint Reference
    public Waypoint Waypoint { get; set; }

    public EnemyHealth EnemyHealth { get; set; }

    public float Loot { get; set; }

    // Returns the current Point Position where this enemy needs to go

    public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(currentWaypointIndex);

    private int currentWaypointIndex;
    private Vector3 lastPointPosition;

    private EnemyHealth _enemyHealth;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        EnemyHealth = GetComponent<EnemyHealth>();

        currentWaypointIndex = 0;
        MoveSpeed = moveSpeed;
        Loot = GetRandomLoot();

        lastPointPosition = transform.position;
    }

    private void Update()
    {
        Move();
        Rotate();

        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }


    private float GetRandomLoot()
    {
        float randomLoot = Random.Range(1, 3);
        return randomLoot;
    }

    private void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position,
            CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumeMovement()
    {
        MoveSpeed = moveSpeed;
    }

    private void Rotate()
    {
        if (CurrentPointPosition.x > lastPointPosition.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            lastPointPosition = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (currentWaypointIndex < lastWaypointIndex)
        {
            currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        OnEndReached?.Invoke(this);
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }    

    public void ResetEnemy()
    {
        currentWaypointIndex = 0;
    }

}
