using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // SINGELTON
    public static GameController gameControllerInstance;

    [SerializeField] private ActionMeterController actionMeterController;

    [Header("Button")]
    [SerializeField] private GameObject battleMenu;
    [SerializeField] private GameObject battleStatusText;
    [SerializeField] private GameObject tryAgainButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject exitButton;

    [Header("Sounds")]
    [SerializeField] private AudioSource battleBackgroundSound;
    [SerializeField] private AudioClip battleWinSound;
    [SerializeField] private AudioClip battleLoseSound;

    private AudioSource battleSound;
    private bool battleIsOver;
    private bool battleStartStatus;

    private void Awake()
    {
        if (gameControllerInstance != null && gameControllerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            gameControllerInstance = this;
        }
    }
    void Start()
    {
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        FighterStats currentFighterStats = hero.GetComponent<FighterStats>();
        FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();

        SetupButtonFirst();

        battleIsOver = false;
        battleStartStatus = false;

        battleSound = gameObject.GetComponent<AudioSource>();
        actionMeterController = actionMeterController.GetComponent<ActionMeterController>();
        Debug.Log(battleStartStatus);

    }

    public void NextTurn(FighterStats currentFighterStats)
    {
        if (battleIsOver == false)
        {
            if (currentFighterStats != null)
            {
                Debug.Log("tester321");
                if (currentFighterStats.GetDead() == false)
                {
                    GameObject currentUnit = currentFighterStats.gameObject;
                    if (currentUnit.tag == "Hero")
                    {
                        this.battleMenu.SetActive(true);
                        this.battleStatusText.GetComponent<TextMeshProUGUI>().text = "Your Turn";
                    }
                    else
                    {
                        this.battleMenu.SetActive(false);
                        this.battleStatusText.GetComponent<TextMeshProUGUI>().text = "Enemy Turn";
                        int random = Random.Range(0, 2);
                        string attackType = "";
                        if (random == 0)
                        {
                            attackType = "melee";
                        }
                        else
                        {
                            attackType = "magic";
                        }
                        Debug.Log("tester");
                        currentUnit.GetComponent<FighterAction>().SelectAction(attackType);
                    }
                    ShowBattleStatusText();
                }
            }
        }

    }

    public void setBattleStatusText(string battleStatus)
    {
        this.battleStatusText.GetComponent<TextMeshProUGUI>().text = battleStatus;
        ShowBattleStatusText();
        ShowTryAgainButton();
        ShowExitButton();
        battleIsOver = true;
        battleBackgroundSound.Stop();

        if (battleStatus == "You Lose") battleSound.clip = battleLoseSound;
        else battleSound.clip = battleWinSound;
        battleSound.Play();
    }

    public void ShowBattleStatusText()
    {
        battleStatusText.SetActive(true);
    }

    public void HideBattleStatusText()
    {
        battleStatusText.SetActive(false);
    }

    public void ShowTryAgainButton()
    {
        tryAgainButton.SetActive(true);
    }

    public void ShowExitButton()
    {
        this.exitButton.SetActive(true);
    }

    public void StartBattle()
    {
        this.exitButton.SetActive(false);
        this.playButton.SetActive(false);
        battleStartStatus = true;
    }

    public void SetupButtonFirst()
    {
        this.battleMenu.SetActive(false);
        this.battleStatusText.SetActive(false);
        this.tryAgainButton.SetActive(false);
        this.playButton.SetActive(true);
        this.exitButton.SetActive(true);
    }

    public void ExitBattle()
    {
        Application.Quit();
    }

    public ActionMeterController GetActionMeterController()
    {
        return this.actionMeterController;
    }

    public void ContinueGame()
    {
        HideBattleStatusText();
        this.actionMeterController.GetActionMeterHandler().SetActionStatus(false);
        this.actionMeterController.GetActionMeterHandler().resetPostiion();
    }

    public bool GetBattleStartStatus()
    {
        return this.battleStartStatus;
    }

    public void SetBattleStartStatus(bool battleStartStatus)
    {
        this.battleStartStatus = battleStartStatus;
    }
}
