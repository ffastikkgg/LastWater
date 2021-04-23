using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{

    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    private AudioSource deadSound;


    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;

    [SerializeField] private float initialHealth = 10;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }

    private Image healthBar;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;

        enemy = GetComponent<Enemy>();

        deadSound = GetComponent<AudioSource>();
    }

    private void Update()
    {

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, CurrentHealth / maxHealth,
            Time.deltaTime * 10f);
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position,
            Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        healthBar = container.FillAmountImage;
    }

    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            //deadSound.Play();
            Die();
        }
        else 
        {
            OnEnemyHit?.Invoke(enemy);
        }
    }


    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        healthBar.fillAmount = 1f;
    }

    private void Die()
    {
        AchievementsManager.Instance.AddProgress("Kill20", 1);
        AchievementsManager.Instance.AddProgress("Kill50", 1);
        AchievementsManager.Instance.AddProgress("Kill100", 1);       
        OnEnemyKilled?.Invoke(enemy);
    }
}
