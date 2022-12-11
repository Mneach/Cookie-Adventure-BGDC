using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMeterHandler : MonoBehaviour
{
    Transform actionMeterCharacterIcon;
    [SerializeField] GameController gameController;
    [SerializeField] FighterStats fighterStats;
    private float initialPosition;
    private float initialSpeed;
    private bool actionStatus = false;
    void Start()
    {
        gameController = gameController.GetComponent<GameController>();
        actionMeterCharacterIcon = GetComponent<Transform>();
        initialPosition = actionMeterCharacterIcon.position.y;
        initialSpeed = fighterStats.GetSpeed();
    }

    private void FixedUpdate()
    {
        if (!actionStatus && gameController.GetBattleStartStatus() == true)
        {
            actionMeterCharacterIcon.position += new Vector3(0f, fighterStats.GetSpeed() * Time.deltaTime, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actionStatus = true;
        gameController.NextTurn(this.fighterStats);
    }

    public void resetPostiion()
    {
        float xPosition = actionMeterCharacterIcon.position.x;
        float yPosition = initialPosition;
        float zPosition = actionMeterCharacterIcon.position.z;
        actionMeterCharacterIcon.position = new Vector3(xPosition, yPosition, zPosition);
    }

    public float GetInitialSpeed()
    {
        return this.initialSpeed;
    }

    public bool GetActionStatus()
    {
        return this.actionStatus;
    }

    public void SetActionStatus(bool actionStatus)
    {
        this.actionStatus = actionStatus;
    }

    public FighterStats GetFighterStats()
    {
        return this.fighterStats;        
    }
}
