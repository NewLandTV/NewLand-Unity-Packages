using NewLandPackages;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private Text[] valuesText;

    [SerializeField]
    private float maxHp;
    private float currentHp;
    public float CurrentHP => currentHp;
    [SerializeField]
    private float maxPower;
    private float currentPower;
    public float CurrentPOWER => currentPower;
    [SerializeField]
    private float maxEnergy;
    private float currentEnergy;
    public float CurrentEnergy => currentEnergy;
    [SerializeField]
    private float maxHungry;
    private float currentHungry;
    public float CurrentHUNGRY => currentHungry;
    [SerializeField]
    private float maxThirst;
    private float currentThirst;
    public float CurrentTHIRST => currentThirst;

    public enum StateType
    {
        HP,
        POWER,
        ENERGY,
        HUNGRY,
        THIRST
    }

    private void Awake()
    {
        currentHp = maxHp;
        currentPower = maxPower;
        currentEnergy = maxEnergy;
        currentHungry = maxHungry;
        currentThirst = maxThirst;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            CheckValidValue();
            UpdateState();

            yield return null;
        }
    }

    private void CheckValidValue()
    {
        currentHp = currentHp > maxHp ? maxHp : NMath.Max(currentHp, 0f);
        currentPower = currentPower > maxHungry ? maxHungry : NMath.Max(currentPower, 0f);
        currentEnergy = currentEnergy > maxEnergy ? maxEnergy : NMath.Max(currentEnergy, 0f);
        currentHungry = currentHungry > maxHungry ? maxHungry : NMath.Max(currentHungry, 0f);
        currentThirst = currentThirst > maxThirst ? maxThirst : NMath.Max(currentThirst, 0f);
    }

    private void UpdateState()
    {
        valuesText[0].text = $"HP {currentHp} / {maxHp}";
        valuesText[1].text = $"POWER {currentPower} / {maxPower}";
        valuesText[2].text = $"ENERGY {currentEnergy} / {maxEnergy}";
        valuesText[3].text = $"HUNGRY {currentHungry} / {maxHungry}";
        valuesText[4].text = $"THIRST {currentThirst} / {maxThirst}";
    }

    #region State

    public void Increase(StateType type, float value)
    {
        switch (type)
        {
            case StateType.HP:
                currentHp += value;
                break;
            case StateType.POWER:
                currentPower += value;
                break;
            case StateType.ENERGY:
                currentEnergy += value;
                break;
            case StateType.HUNGRY:
                currentHungry += value;
                break;
            case StateType.THIRST:
                currentThirst += value;
                break;
        }
    }

    public void Descrease(StateType type, float value)
    {
        switch (type)
        {
            case StateType.HP:
                currentHp -= value;
                break;
            case StateType.POWER:
                currentPower -= value;
                break;
            case StateType.ENERGY:
                currentEnergy -= value;
                break;
            case StateType.HUNGRY:
                currentHungry -= value;
                break;
            case StateType.THIRST:
                currentThirst -= value;
                break;
        }
    }

    #endregion
}
