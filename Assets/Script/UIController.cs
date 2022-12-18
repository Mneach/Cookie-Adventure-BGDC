using UnityEngine;

public class UIController : MonoBehaviour
{
    // SINGELTON
    public static UIController UIControllerInstance;

    [Header("Button")]
    [SerializeField] private GameObject battleMenu;
    [SerializeField] private GameObject tryAgainButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject exitButton;

    [Header("Action")]
    [SerializeField] private GameObject attackButton;
    [SerializeField] private GameObject magicButton;
    [SerializeField] private GameObject attackWithAllyButton;
    [SerializeField] private GameObject escapeButton;

    [Header("Game Status")]
    [SerializeField] private GameObject battleStatusText;

    private void Awake()
    {
        if (UIControllerInstance != null && UIControllerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            UIControllerInstance = this;
        }
    }

    public void HideAttackWithAllyButton()
    {
        this.attackWithAllyButton.SetActive(false);
    }
}
