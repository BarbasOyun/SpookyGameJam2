using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider fearMeter;
    public float fearReduction = .1f;
    public float fearIncrementMult = .01f;
    public GameObject person;

    [SerializeField]
    public Rigidbody[] furnitures;
    public float[] effectivness;

    void Start()
    {
        
    }

    void Update()
    {
        if (fearMeter.value >= .99f)
        {
            Debug.Log("WIN WIN WIN");
        }

        fearMeter.value -= fearReduction;

        for(int i = 0; i < furnitures.Length; i++)
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
