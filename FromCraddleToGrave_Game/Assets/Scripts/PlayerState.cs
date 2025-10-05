using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerAttributesSO attributes;
    public float Health { get; private set; }
    public float Satisfaction { get; private set; }
    public float Money { get; private set; }
    public int AgeDays { get; private set; }
    public int EducationLevel { get; private set; }

    public void Initialize()
    {
        Health = attributes ? attributes.startHealth : 100f;
        Satisfaction = attributes ? attributes.startSatisfaction : 50f;
        Money = attributes ? attributes.startMoney : 0f;
        EducationLevel = attributes ? attributes.startEducationLevel : 0;
        AgeDays = 0;
    }

    public float AgeYears => AgeDays / 365f;

    public void ClampValues(SimulationConfigSO cfg)
    {
        Health = Mathf.Clamp(Health, cfg.minHealth, cfg.maxHealth);
        Satisfaction = Mathf.Clamp(Satisfaction, cfg.minSatisfaction, cfg.maxSatisfaction);
        Money = Mathf.Max(Money, cfg.minMoney);
    }

    public void AddInstant(float h, float s, float m)
    {
        Health += h;
        Satisfaction += s;
        Money += m;
    }

    public void AdvanceDays(int days)
    {
        AgeDays += days;
    }
}
