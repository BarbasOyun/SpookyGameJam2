using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool runFearMeter;
    public Slider fearMeter;
    public float fearReduction = .1f;
    public float fearIncrementMult = .01f;
    public GameObject person;
    public GameObject victoryPanel;
    private Action fearMeterAction = null;

    [SerializeField]
    public Rigidbody[] furnitures;
    public float[] effectivness;

    void Start()
    {
        if (runFearMeter)
        {
            fearMeterAction = fearMeterUpdate;
        }
    }

    void Update()
    {
        fearMeterAction?.Invoke();
    }

    void fearMeterUpdate()
    {
        // Debug.Log("Fear Meter Update");

        if (fearMeter.value >= .99f && !victoryPanel.activeSelf)
        {
            victoryPanel.SetActive(true);
        }
        
        switch (fearMeter.value)
        {
            case < .5f:
                PNJ.UpdateAnimation(1);
                break;
            case >= .75f:
                PNJ.UpdateAnimation(3);
                break;
            default:
                PNJ.UpdateAnimation(2);
                break;
        }

        fearMeter.value -= fearReduction;

        for (int i = 0; i < furnitures.Length; i++)
        {
            Rigidbody furniture = furnitures[i];
            float distancePerson = 1 / Vector3.Distance(person.transform.position, furniture.position);
            float massMult = furniture.mass * .1f;
            float fearIncrement = distancePerson * furniture.linearVelocity.magnitude * massMult * effectivness[i] * fearIncrementMult; // Add Object Effectivness / selectedObjectMult
            // Debug.Log("Distance : " + distancePerson + " * Velocity : " + selectedObject.linearVelocity.magnitude);
            // Debug.Log("Fear Increment : " + fearIncrement);
            fearMeter.value += fearIncrement;
        }
    }
}
