using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField] private string animationName;
    [SerializeField] private bool magicAttack;
    [SerializeField] private float magicCost;
    [SerializeField] private float minAttackMultiplier;
    [SerializeField] private float maxAttackMultiplier;
    [SerializeField] private float minDefenseMultiplier;
    [SerializeField] private float maxDefenseMultiplier;

    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;

    private Animator animator;

    private void Start()
    {
        animator = owner.GetComponent<Animator>();
    }

    public void Attack(GameObject victim)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        if (attackerStats.magic >= magicCost)
        {
            attackerStats.UpdateMagicFill(magicCost);
            float multipier = Random.Range(minAttackMultiplier, maxAttackMultiplier);
            damage = multipier * attackerStats.melee;
            if (magicAttack)
            {
                damage = multipier * attackerStats.magic;
            }

            float defenseMultipier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);
            damage = Mathf.Max(0, damage - (defenseMultipier - targetStats.defense));

            StartCoroutine(AttackAnimation());
        }
    }

    IEnumerator AttackAnimation()
    {
        owner.GetComponent<Animator>().Play(animationName);
        float length = 0.0f;
        AnimationClip[] clips = owner.GetComponent<Animator>().runtimeAnimatorController.animationClips;

        foreach(AnimationClip clip in clips)
        {
            if(clip.name == animationName)
            {
                length = clip.length;
            }
        }

        Debug.Log(length);
        yield return new WaitForSeconds(length);

        targetStats.RecieveDamage(Mathf.CeilToInt(damage));
        
    }
}
