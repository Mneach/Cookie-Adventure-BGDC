using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class FighterStats : MonoBehaviour , IComparable
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject healthFill;
    [SerializeField] private GameObject magicFill;
    [SerializeField] Transform damagePopupPrefab;
    private Transform HeadsUpCanvas;

    [Header("Bar Text")]
    [SerializeField] private TextMeshProUGUI healthFillText;
    [SerializeField] private TextMeshProUGUI magicFillText;

    [Header("stats")]
    public float health;
    public float magic;
    public float melee;
    public float range;
    public float defense;
    public float speed;
    public float experience;

    private float startHealth;
    private float startMagic;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    //resize health and magic bar
    private Transform healthTransform;
    private Transform magicTransform;

    private Vector2 healthScale;
    private Vector2 magicScale;

    private float xNewHealthScale;
    private float xNewMagicScale;

    private GameObject gameController;

    private void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        magicTransform = magicFill.GetComponent<RectTransform>();
        magicScale = magicFill.transform.localScale;

        startHealth = health;
        startMagic = magic;

        healthFillText.text = startHealth.ToString();
        magicFillText.text = startMagic.ToString();

        gameController = GameObject.Find("GameController");
        HeadsUpCanvas = GameObject.Find("HeadsUpCanvas").transform;

    }

    public void RecieveDamage(float damage)
    {
        health -= damage;
        animator.Play("Damaged");
        if(health < 0) updateHealtFillText(0);
        else updateHealtFillText((int)health);

        if (health <= 0)
        {
            dead = true;
            health = 0;
            Destroy(healthFill);
            /*  Destroy(gameObject);*/

            string battleStatusText = "";

            if (gameObject.tag == "Hero") battleStatusText = "You Lose";
            else battleStatusText = "You Win";

            gameController.GetComponent<GameController>().setBattleStatusText(battleStatusText);
            gameController.GetComponent<GameController>().SetBattleStartStatus(false);
            gameObject.tag = "Dead";
            
        }else if(damage > 0)
        {
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale , healthScale.y);
        }
        if( damage > 0)
        {
            Vector3 spawnTextPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            DamagePopup.Create(spawnTextPosition, (int) damage, HeadsUpCanvas, damagePopupPrefab);
        }

        if(health > 0) gameController.GetComponent<GameController>().ContinueGame();
    }

    public void UpdateMagicFill(float cost)
    {
        if(cost > 1)
        {
            magic -= cost;
            updateMagicFillText((int) magic);
            xNewMagicScale = magicScale.x * (magic / startMagic);
            magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);
        }
        
    }

    public void updateHealtFillText(int currentHealthAmout)
    {
        healthFillText.text = currentHealthAmout.ToString();
    }

    public void updateMagicFillText(int currentMagicAmout)
    {
        magicFillText.text = currentMagicAmout.ToString();
    }

    public bool GetDead()
    {
        return dead;
    }

    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }

    public int CompareTo(object otherStats)
    {
        int next = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return next;
    }

    public float getMagic()
    {
        return this.magic;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
