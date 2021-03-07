using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator animator;
    private Enemy _enemy;
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void PlayHurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    private void PlayDieAnimation()
    {
        animator.SetTrigger("Die");
    }


    private float GetCurrentAnimationLenght()
    {
        float animationLenght = animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLenght;
    }

/**
    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght());
        _enemy.ResumeMovement();
    }
    **/



    private IEnumerator PlayDead()
    {
       _enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.3f);
        _enemy.ResumeMovement();
        enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(_enemy.gameObject);
    }

    /**
   private void EnemyHit(Enemy enemy)
   {
       if (_enemy == enemy)
       {
           StartCoroutine(PlayHurt());
       }
   }
     **/

    private void EnemyDead(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayDead());
        }
    }


    private void OnEnable()
    {
        //EnemyHealth.OnEnemyHit += EnemyHit;
        EnemyHealth.OnEnemyKilled += EnemyDead;
    }

    private void OnDisable()
    {
        //EnemyHealth.OnEnemyHit -= EnemyHit;
        EnemyHealth.OnEnemyKilled -= EnemyDead;
    }
}
