using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float dissaperTimer;
    private Color textColor;
    private const float DISSAPEAR_TIMER_MAX = 1f;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
        if (textMesh != null)
        {
            Debug.Log("masok");
        }
        else Debug.Log("Error");
    }

    public void Setup(int damage)
    {
        textMesh.text = damage.ToString();
        textColor = textMesh.color;
        dissaperTimer = DISSAPEAR_TIMER_MAX;
    }

    public static DamagePopup Create(Vector3 position , int damage , Transform Canvas , Transform damagePopupPrefab)
    {
        Transform damagePopupTransform = Instantiate(damagePopupPrefab, position, Quaternion.identity);
        damagePopupTransform.transform.parent = Canvas.transform;
        damagePopupTransform.transform.localScale = Vector3.one;
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damage);

        return damagePopup;
    }

    private void Update()
    {
        float moveYSpeed = 1f;
        transform.position += new Vector3(0 , moveYSpeed) * Time.deltaTime;

        if(dissaperTimer > DISSAPEAR_TIMER_MAX * .5f)
        {
            float increaseScaleAmout = 1f;
            transform.localScale += Vector3.one * increaseScaleAmout * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmout = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmout * Time.deltaTime;
        }

        dissaperTimer -= Time.deltaTime;

        if(dissaperTimer < 0)
        {
            float dissaperSpeed = 3f;
            textColor.a -= dissaperSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
