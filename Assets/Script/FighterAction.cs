using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject hero;

    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject magicPrefab;
    [SerializeField] private Sprite faceIcon;

    private GameObject currentAttack;
    [SerializeField] private GameObject battleMenu;

    private void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void SelectAttack(string button)
    {
        battleMenu.SetActive(false);
        GameObject victim = hero;
        if(tag == "Hero")
        {
            victim = enemy;
        }

        if(enemy.GetComponent<FighterStats>().getMagic() == 0 && tag != "Hero")
        {
            button = "melee";
        }

        if (button.CompareTo("melee") == 0)
        {
            meleePrefab.GetComponent<AttackScript>().Attack(victim);
        }else if(button.CompareTo("magic") == 0)
        {
            magicPrefab.GetComponent<AttackScript>().Attack(victim);
        }
        else
        {
            Debug.Log("Abc");
        }
    }
}
