using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithAllies : MonoBehaviour
{
    public GameObject owner;

    [SerializeField] private GameObject alliesGameObject;

    [SerializeField] private string animationName;
    [SerializeField] private bool magicAttack;
    [SerializeField] private float magicCost;
    [SerializeField] private float minAttackMultiplier;
    [SerializeField] private float maxAttackMultiplier;
    [SerializeField] private float minDefenseMultiplier;
    [SerializeField] private float maxDefenseMultiplier;

    private FighterStats attackerStats;
    private FighterStats targetStats;

    private Allies allies;

    private float damage = 0.0f;

    private Animator animator;

    private void Start()
    {
        animator = owner.GetComponent<Animator>();
        allies = alliesGameObject.GetComponent<Allies>();
    }

    public void Attack(GameObject victim)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        if (attackerStats.magic >= magicCost)
        {
            //UPDATE MAGIC FILL
            attackerStats.UpdateMagicFill(magicCost);

            // DETERMINE ATTACK TYPE
            if (magicAttack) damage = CalculateMagicAttack();
            else damage = CalculateMeleeAttack();

            damage = Mathf.Max(0, damage - CalculateDefense()) + allies.GetAlliesStat().GetDamage();
            StartCoroutine(AttackAnimation());
        }
    }

    public float CalculateMeleeAttack()
    {
        float multipier = Random.Range(minAttackMultiplier, maxAttackMultiplier);
        return multipier * attackerStats.meleeDamage;
    }

    public float CalculateDefense()
    {
        float defenseMultipier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
        return defenseMultipier * targetStats.defense;
    }

    public float CalculateMagicAttack()
    {
        float multipier = Random.Range(minAttackMultiplier, maxAttackMultiplier);
        return multipier * attackerStats.magic;
    }

    IEnumerator AttackAnimation()
    {
        // SHOW ALLIES
        yield return StartCoroutine(allies.ShowCharacters());

        // PLAY ANIMATION
        owner.GetComponent<Animator>().Play(animationName);
        alliesGameObject.GetComponent<Animator>().Play(animationName);

        float length = 0.0f;
        AnimationClip[] clips = owner.GetComponent<Animator>().runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                length = clip.length;
            }
        }

        // WAIT UNTIL ANIMATION IS FINISH
        yield return new WaitForSeconds(length);

        // HIDE ALLIES
        StartCoroutine(allies.HideCharacters());

        // RECIEVE DAMAGE
        targetStats.RecieveDamage(Mathf.CeilToInt(damage));

        // HIDE BUTTON ATTACK WITH ALLIES
        UIController.UIControllerInstance.HideAttackWithAllyButton();
    }
}
