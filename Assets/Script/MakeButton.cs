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
            hero.GetComponent<FighterAction>().SelectAction("melee");
        }
        else if (button.CompareTo("MagicButton") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAction("magic");
        }
        else if (button.CompareTo("CallAllyButton") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAction("call_ally");
        }
        else if (button.CompareTo("EscapeButton") == 0)
        {
            hero.GetComponent<FighterAction>().SelectAction("escape");
        }
    }
}
