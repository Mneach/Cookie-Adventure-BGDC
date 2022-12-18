using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hero;

    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject magicPrefab;

    [SerializeField] private GameObject callAllyPrefab;

    [SerializeField] private GameObject escapePrefab;

    [SerializeField] private Sprite faceIcon;

    private GameObject currentAttack;
    [SerializeField] private GameObject battleMenu;

    private void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void SelectAction(string button)
    {
        Debug.Log("selection");
        battleMenu.SetActive(false);
        GameObject victim = hero;

        // DETERMINE TARGET
        if (tag == "Hero") victim = enemy;

        // CONDITION WHERE ENEMY MP IS NOT ENOUGH TO USE MAGIC
        if (enemy.GetComponent<FighterStats>().getMagic() == 0 && tag != "Hero") button = "melee";

        // CHARACTER ACTION
        if (button.CompareTo("melee") == 0) meleePrefab.GetComponent<AttackScript>().Attack(victim);
        else if (button.CompareTo("magic") == 0) magicPrefab.GetComponent<AttackScript>().Attack(victim);
        else if (button.CompareTo("call_ally") == 0) callAllyPrefab.GetComponent<AttackWithAllies>().Attack(victim);
        else if (button.CompareTo("escape") == 0) escapePrefab.GetComponent<EscapeFromBattle>().Escape();
    }
}
