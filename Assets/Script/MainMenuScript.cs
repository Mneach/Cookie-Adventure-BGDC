using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene("BattleScene");
    }

    IEnumerator LoadLevel (int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
