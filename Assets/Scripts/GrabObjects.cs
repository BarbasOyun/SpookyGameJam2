using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class GrabObjects : MonoBehaviour
{
    [SerializeField]
    private LayerMask grabMask;
    public float rayDistance = 100f;
    public float grabYOffSet = .5f;
    public float throwForce = .2f;

    public Rigidbody selectedObject;
    private float yPos;
    private Vector3 storedVelocity;

    void Start()
    {
        
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Gears.ReloadCurrentScene();
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Debug.Log("Left mouse button clicked");
            // Detect Object To Grab
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            Physics.Raycast(ray, out hit, rayDistance, grabMask);
            Debug.DrawRay(ray.origin, ray.direction * 100, UnityEngine.Color.red, 0.1f);

            if (hit.collider)
            {
                Debug.Log("Object Clicked : " + hit.collider.gameObject.name);
                selectedObject = hit.collider.attachedRigidbody;
                yPos = selectedObject.position.y;
                selectedObject.isKinematic = true;
            }
        }

        if (Mouse.current.leftButton.isPressed)
        {
            // Move Selected Object
            if (selectedObject)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                float distance = Vector3.Distance(ray.origin, selectedObject.position);
                Vector3 mouseWorldPos = ray.origin + (ray.direction.normalized * distance);
                Vector3 finalPos = new Vector3(mouseWorldPos.x, yPos + grabYOffSet, mouseWorldPos.z);

                // Debug.Log("Mouse World Pos : " + mouseWorldPos);
                Debug.DrawRay(mouseWorldPos, Vector3.up * 10, UnityEngine.Color.green, 0.1f);

                // selectedObject.position = finalPos;
                selectedObject.MovePosition(finalPos);
                storedVelocity = selectedObject.linearVelocity;
                // Debug.Log("Stored Velocity" +  storedVelocity);
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (selectedObject)
            {
                selectedObject.isKinematic = false;
                selectedObject.linearVelocity = storedVelocity * throwForce;
                selectedObject = null;
            }
        }
    }
}
