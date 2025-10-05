using UnityEngine;

[CreateAssetMenu(fileName = "SimulationConfig", menuName = "LifeSim/Simulation Config")]
public class SimulationConfigSO : ScriptableObject
{
    public float baseHealthDecayPerDay = 0.25f;
    public float minHealth = 0f;
    public float maxHealth = 100f;
    public float minSatisfaction = 0f;
    public float maxSatisfaction = 100f;
    public float minMoney = -100000f;
    public int randomEventChecksPerDay = 1;
}
