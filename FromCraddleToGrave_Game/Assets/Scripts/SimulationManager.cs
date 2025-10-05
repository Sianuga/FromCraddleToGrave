using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimulationManager : MonoBehaviour
{
    public SimulationConfigSO config;
    public PlayerState player;
    public List<RandomEventSO> randomEvents = new List<RandomEventSO>();
    public UnityEvent onSimulationEnded;
    public UnityEvent<int> onDayAdvanced;

    private readonly List<TimedEffect> activeEffects = new List<TimedEffect>();
    private bool running = true;
    private Coroutine autoRoutine;

    void Start()
    {
        if (player != null) player.Initialize();
    }

    public void StepOneDay()
    {
        if (!running || config == null || player == null) return;

        TickRandomEvents();
        TickEffects();
        TickAging();

        player.ClampValues(config);
        player.AdvanceDays(1);
        onDayAdvanced?.Invoke(player.AgeDays);

        if (player.Health <= config.minHealth)
        {
            running = false;
            onSimulationEnded?.Invoke();
        }
    }

    public void StepDays(int days)
    {
        for (int i = 0; i < days && running; i++) StepOneDay();
    }

    public void StartAuto(int days, float secondsBetweenDays)
    {
        if (autoRoutine != null) StopCoroutine(autoRoutine);
        autoRoutine = StartCoroutine(AutoRun(days, secondsBetweenDays));
    }

    public void StopAuto()
    {
        if (autoRoutine != null) StopCoroutine(autoRoutine);
        autoRoutine = null;
    }

    IEnumerator AutoRun(int days, float interval)
    {
        int left = days;
        while (left > 0 && running)
        {
            StepOneDay();
            left--;
            if (left > 0 && interval > 0f) yield return new WaitForSeconds(interval);
            else yield return null;
        }
        autoRoutine = null;
    }

    void TickAging()
    {
        float decayMul = 1f;
        for (int i = 0; i < activeEffects.Count; i++) decayMul *= Mathf.Max(0f, activeEffects[i].healthDecayMultiplier);
        float healthLoss = config.baseHealthDecayPerDay * decayMul;
        player.AddInstant(-healthLoss, 0f, 0f);
    }

    void TickEffects()
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            var e = activeEffects[i];
            player.AddInstant(e.healthPerDay, e.satisfactionPerDay, e.moneyPerDay);
            e.remainingDays -= 1;
            if (e.remainingDays <= 0) activeEffects.RemoveAt(i);
        }
    }

    void TickRandomEvents()
    {
        for (int c = 0; c < Mathf.Max(1, config.randomEventChecksPerDay); c++)
        {
            for (int i = 0; i < randomEvents.Count; i++)
            {
                var ev = randomEvents[i];
                if (Random.value <= Mathf.Clamp01(ev.probabilityPerCheck))
                {
                    player.AddInstant(ev.healthDeltaInstant, ev.satisfactionDeltaInstant, ev.moneyDeltaInstant);
                    if (ev.durationDays > 0 || ev.healthDecayMultiplier != 1f || ev.effectHealthPerDay != 0f || ev.effectSatisfactionPerDay != 0f || ev.effectMoneyPerDay != 0f)
                    {
                        activeEffects.Add(new TimedEffect(
                            ev.eventId,
                            Mathf.Max(1, ev.durationDays),
                            ev.healthDecayMultiplier,
                            ev.effectHealthPerDay,
                            ev.effectSatisfactionPerDay,
                            ev.effectMoneyPerDay
                        ));
                    }
                }
            }
        }
    }

    public void ApplyAction(GameActionSO action)
    {
        if (!running || action == null) return;
        player.AddInstant(action.healthDelta, action.satisfactionDelta, action.moneyDelta);

        if (action.effectDurationDays > 0 || action.healthDecayMultiplier != 1f || action.effectHealthPerDay != 0f || action.effectSatisfactionPerDay != 0f || action.effectMoneyPerDay != 0f)
        {
            activeEffects.Add(new TimedEffect(
                action.actionId,
                Mathf.Max(1, action.effectDurationDays),
                action.healthDecayMultiplier,
                action.effectHealthPerDay,
                action.effectSatisfactionPerDay,
                action.effectMoneyPerDay
            ));
        }

        int cost = Mathf.Max(0, action.timeCostDays);
        if (cost > 0) StepDays(cost);
    }

    public void PauseSimulation(bool pause) { running = !pause; }
}
