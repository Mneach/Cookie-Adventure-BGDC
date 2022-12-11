using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    void Start()
    {
        
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
