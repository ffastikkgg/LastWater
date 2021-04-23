using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectiles : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPosition;
    [SerializeField] protected float delayBtwAttacks = 2f;
    [SerializeField] protected float damage = 2f;

    public float Damage { get; set; }
    public float DelayPerShot { get; set; }

    protected float nextAttackTime;
    protected ObjectPooler pooler;
    protected Tower tower;
    protected Projectile currentProjectileLoaded;

    private AudioSource shot;

    private void Start()
    {
        tower = GetComponent<Tower>();
        pooler = GetComponent<ObjectPooler>();

        Damage = damage;
        DelayPerShot = delayBtwAttacks;
        LoadProjectile();
        shot = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (IsTurretEmpty())
        {
            LoadProjectile();
        }

        if (Time.time > nextAttackTime)
        {

            if (tower.CurrentEnemyTarget != null && currentProjectileLoaded != null &&
                tower.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {               
                currentProjectileLoaded.transform.parent = null;
                currentProjectileLoaded.SetEnemy(tower.CurrentEnemyTarget);
                
            }

            nextAttackTime = Time.time + DelayPerShot;
        }
    }

    protected virtual void LoadProjectile()
    {
        GameObject newInstance = pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = projectileSpawnPosition.position;
        newInstance.transform.SetParent(projectileSpawnPosition);

        currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        currentProjectileLoaded.TowerOwner = this;
        currentProjectileLoaded.ResetProjectile();
        currentProjectileLoaded.Damage = Damage;
        newInstance.SetActive(true);      
    }

    private bool IsTurretEmpty()
    {
        return currentProjectileLoaded == null;
    }

    public void ResetTurretProjectile()
    {
        shot.Play();
        currentProjectileLoaded = null;
    }


}
