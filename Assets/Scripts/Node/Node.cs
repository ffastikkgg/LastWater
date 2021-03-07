using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;
    public static Action OnTowerSold;

    [SerializeField] private GameObject attackRangeSprite;

    public Tower Tower { get; set; }

    private float rangeSize;
    private Vector3 rangeOriginalSize;

    private void Start()
    {
        rangeSize = attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        rangeOriginalSize = attackRangeSprite.transform.localScale;
    }

    public void SetTower(Tower tower)
    {
        Tower = tower;
    }

    public bool IsEmpty()
    {
        return Tower == null;
    }

    public void CloseAttackRange()
    {
        attackRangeSprite.SetActive(false);
    }

    public void SelectTower()
    {
        OnNodeSelected?.Invoke(this);
        if (!IsEmpty())
        {
            ShowTowerInfo();
        }
    }

    public void SellTower()
    {
        if (!IsEmpty())
        {
            CurrencySystem.Instance.AddCoins(Tower.TowerUpgrade.GetSellValue());
            Destroy(Tower.gameObject);
            Tower = null;
            attackRangeSprite.SetActive(false);
            OnTowerSold?.Invoke();
        }
    }

    private void ShowTowerInfo()
    {
        attackRangeSprite.SetActive(true);
        attackRangeSprite.transform.localScale = rangeOriginalSize * Tower.AttackRange / (rangeSize / 2);

    }

}
