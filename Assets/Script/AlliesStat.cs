using UnityEngine;

public class AlliesStat : MonoBehaviour
{

    [SerializeField] private float damage;

    public float GetDamage()
    {
        return this.damage;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}