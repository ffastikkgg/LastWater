using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTowerProjectile : TowerProjectiles
{
    [SerializeField] private bool isDualMachine;
    [SerializeField] private float spreadRange;

    private AudioSource shot;



    protected override void Update()
    {
        if (Time.time > nextAttackTime)
        {
            if (tower.CurrentEnemyTarget != null)
            {
                Vector3 dirToTarget = tower.CurrentEnemyTarget.transform.position - transform.position;
                FireProjectile(dirToTarget);
            }

            nextAttackTime = Time.time + delayBtwAttacks;
        }
    }

    protected override void LoadProjectile()
    {
        shot = GetComponent<AudioSource>();
    }

    private void FireProjectile(Vector3 direction)
    {
        shot = GetComponent<AudioSource>();
        shot.Play();

        GameObject instance = pooler.GetInstanceFromPool();
        instance.transform.position = projectileSpawnPosition.position;


        MachineProjectile projectile = instance.GetComponent<MachineProjectile>();
        projectile.Direction = direction;
        projectile.Damage = Damage;
        

        if (isDualMachine)
        {
            float randomSpread = Random.Range(-spreadRange, spreadRange);
            Vector3 spread = new Vector3(0f, 0f, randomSpread);
            Quaternion spreadValue = Quaternion.Euler(spread);
            Vector2 newDirection = spreadValue * direction;
            projectile.Direction = newDirection;
        }

        instance.SetActive(true);
    }

}
