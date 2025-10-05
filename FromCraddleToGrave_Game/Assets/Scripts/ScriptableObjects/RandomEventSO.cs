using UnityEngine;

[CreateAssetMenu(fileName = "RandomEvent", menuName = "LifeSim/Random Event")]
public class RandomEventSO : ScriptableObject
{
    public string eventId;
    public float probabilityPerCheck;
    public int durationDays;
    public float healthDeltaInstant;
    public float satisfactionDeltaInstant;
    public float moneyDeltaInstant;
    public float healthDecayMultiplier = 1f;
    public float effectHealthPerDay;
    public float effectSatisfactionPerDay;
    public float effectMoneyPerDay;
}
