using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool physical;

    private GameObject hero;
    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallBack(temp));
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    private void AttachCallBack(string button)
    {
        if (button.CompareTo("MeleeButton") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("melee");
        }else if(button.CompareTo("MagicButton") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAttack("magic");
        }
        else
        {
            hero.GetComponent<FighterAction>().SelectAttack("run");
        }
    }
}
