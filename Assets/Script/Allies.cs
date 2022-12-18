using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allies : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float alphaColor;
    private Color spriteColor;

    [SerializeField] private AlliesStat alliesStat;

    private bool showStatus = false;
    private bool hideStatus = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alliesStat = alliesStat.GetComponent<AlliesStat>();
        alphaColor = 0;
        changeALphaColor(alphaColor);
    }

    public AlliesStat GetAlliesStat()
    {
        return this.alliesStat;
    }

    public void changeALphaColor(float alphaColor)
    {
        spriteColor = new Color(1, 1, 1, alphaColor);
        spriteRenderer.color = spriteColor;
    }

    public IEnumerator ShowCharacters()
    {
        while (alphaColor < 1)
        {

            alphaColor += 0.8f * Time.deltaTime;
            changeALphaColor(alphaColor);
            yield return null;
        }
        yield return new WaitWhile(() => alphaColor < 1);
        Debug.Log("attack");
    }

    public IEnumerator HideCharacters()
    {
        while (alphaColor >= 0)
        {

            alphaColor -= 0.9f * Time.deltaTime;
            changeALphaColor(alphaColor);
            yield return null;
        }
        yield return new WaitWhile(() => alphaColor >= 0);
    }
}