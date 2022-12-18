using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMeterController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] ActionMeterHandler heroMeterHandler;
    [SerializeField] ActionMeterHandler enemyMeterHandler;

    private FighterStats FighterStats;
    private ActionMeterHandler currentActionMeterHandler;
    void Start()
    {
        gameController = gameController.GetComponent<GameController>();
        heroMeterHandler = heroMeterHandler.GetComponent<ActionMeterHandler>();
        enemyMeterHandler = enemyMeterHandler.GetComponent<ActionMeterHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetBattleStartStatus() == true)
        {
            speedHandler();
            moveHanlder();
        }
    }

    void moveHanlder()
    {
        if (heroMeterHandler.GetActionStatus() == true)
        {
            SetMoveFighterStat(heroMeterHandler.GetFighterStats());
            SetCurrentMeterHanlder(heroMeterHandler);
        }
        else if (enemyMeterHandler.GetActionStatus() == true)
        {
            SetMoveFighterStat(enemyMeterHandler.GetFighterStats());
            SetCurrentMeterHanlder(enemyMeterHandler);
        }

    }

    void speedHandler()
    {
        if (heroMeterHandler.GetActionStatus() == true || enemyMeterHandler.GetActionStatus() == true)
        {
            heroMeterHandler.GetFighterStats().SetSpeed(0);
            enemyMeterHandler.GetFighterStats().SetSpeed(0);
        }
        else
        {
            heroMeterHandler.GetFighterStats().SetSpeed(heroMeterHandler.GetInitialSpeed());
            enemyMeterHandler.GetFighterStats().SetSpeed(enemyMeterHandler.GetInitialSpeed());
        }
    }

    public void SetCurrentMeterHanlder(ActionMeterHandler actionMeterHandler)
    {
        this.currentActionMeterHandler = actionMeterHandler;
    }

    public ActionMeterHandler GetActionMeterHandler()
    {
        return this.currentActionMeterHandler;
    }

    public void SetMoveFighterStat(FighterStats fighterStats)
    {
        this.FighterStats = fighterStats;
    }

    public FighterStats GetFighterStats()
    {
        return this.FighterStats;
    }
}
