using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTowerProjectile : TowerProjectiles
{
    protected override void Update()
    {
        if (Time.time > nextAttackTime)
        {
            if (tower.CurrentEnemyTarget != null
                && tower.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0)
            {
                FireProjectile(tower.CurrentEnemyTarget);
            }

            nextAttackTime = Time.time + delayBtwAttacks;
        }
    }

    protected override void LoadProjectile() { }

    private void FireProjectile(Enemy enemy)
    {
        GameObject instance = pooler.GetInstanceFromPool();
        instance.transform.position = projectileSpawnPosition.position;

        Projectile projectile = instance.GetComponent<Projectile>();
        currentProjectileLoaded = projectile;
        currentProjectileLoaded.TowerOwner = this;
        currentProjectileLoaded.ResetProjectile();
        currentProjectileLoaded.SetEnemy(enemy);
        currentProjectileLoaded.Damage = Damage;
        instance.SetActive(true);
    }
}
