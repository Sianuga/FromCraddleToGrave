using UnityEngine;

[CreateAssetMenu(fileName = "GameAction", menuName = "LifeSim/Action")]
public class GameActionSO : ScriptableObject
{
    public string actionId;
    public int timeCostDays;
    public float healthDelta;
    public float satisfactionDelta;
    public float moneyDelta;
    public float healthDecayMultiplier = 1f;
    public int effectDurationDays;
    public float effectHealthPerDay;
    public float effectSatisfactionPerDay;
    public float effectMoneyPerDay;
}
