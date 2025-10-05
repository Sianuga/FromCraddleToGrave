using UnityEngine;

[System.Serializable]
public class TimedEffect
{
    public string sourceId;
    public int remainingDays;
    public float healthDecayMultiplier = 1f;
    public float healthPerDay;
    public float satisfactionPerDay;
    public float moneyPerDay;

    public TimedEffect(string id, int days, float decayMul, float h, float s, float m)
    {
        sourceId = id;
        remainingDays = days;
        healthDecayMultiplier = decayMul;
        healthPerDay = h;
        satisfactionPerDay = s;
        moneyPerDay = m;
    }
}
