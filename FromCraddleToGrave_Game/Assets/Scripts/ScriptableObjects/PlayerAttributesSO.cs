using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributes", menuName = "LifeSim/Player Attributes")]
public class PlayerAttributesSO : ScriptableObject
{
    public float startHealth = 100f;
    public float startSatisfaction = 50f;
    public float startMoney = 1000f;
    public int startEducationLevel = 0;
    public int currentDay = 1;
}
