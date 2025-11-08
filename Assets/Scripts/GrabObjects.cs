using System;
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
    // public float grabYOffSet = .5f;
    // public float throwForce = .2f;
    private Vector3[] cameraPos = {new Vector3(-87, 24, -38), new Vector3(-12, 24, -38) };
    private int cameraPosIndex = 0;

    public Rigidbody selectedObject;
    // public float selectedObjectMult;
    // private float yPos;
    // private Vector3 storedVelocity;

    private Vector2 previousMousePosition;
    public float grabMult;

    void Start()
    {
        Camera.main.transform.position = cameraPos[cameraPosIndex];
        previousMousePosition = Mouse.current.position.ReadValue();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Gears.gears.LoadMainMenu();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Gears.ReloadCurrentScene();
        }

        // Change Camera
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (cameraPosIndex < cameraPos.Length - 1)
            {
                cameraPosIndex++;
            }
            else
                cameraPosIndex = 0;
            
            Camera.main.transform.position = cameraPos[cameraPosIndex];
        }

        // Mouse Velocity
        Vector2 currentMousePosition = Mouse.current.position.ReadValue();
        Vector2 delta = currentMousePosition - previousMousePosition;
        // float timeDelta = Time.deltaTime;
        // float pixelsPerSecond = delta.magnitude / timeDelta;
        previousMousePosition = currentMousePosition;
        // Debug.Log("Mouse Velocity : " + delta);

        // SELECT Object
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
                // selectedObjectMult = Array.Find(Gears.gears.gameManager.objects, p => p.value2 == selectedObject).value1;
                // yPos = selectedObject.position.y;
                // selectedObject.isKinematic = true;
            }
        }

        // MOVE Object
        if (Mouse.current.leftButton.isPressed)
        {
            // Move Selected Object
            if (selectedObject)
            {
                // MovePos();

                MoveSelectedObject(delta);
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (selectedObject)
            {
                // selectedObject.isKinematic = false;
                // selectedObject.linearVelocity = storedVelocity * throwForce;
                selectedObject = null;
            }
        }
    }

    private void MovePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        float distance = Vector3.Distance(ray.origin, selectedObject.position);
        Vector3 mouseWorldPos = ray.origin + (ray.direction.normalized * distance);
        // Vector3 finalPos = new Vector3(mouseWorldPos.x, yPos + grabYOffSet, mouseWorldPos.z);

        // Debug.Log("Mouse World Pos : " + mouseWorldPos);
        Debug.DrawRay(mouseWorldPos, Vector3.up * 10, UnityEngine.Color.green, 0.1f);

        // selectedObject.position = finalPos;
        // selectedObject.MovePosition(finalPos);
        // storedVelocity = selectedObject.linearVelocity;
        // Debug.Log("Stored Velocity" +  storedVelocity);
    }

    private void MoveSelectedObject(Vector2 delta)
    {
        Vector3 force = new Vector3(delta.x, 0, delta.y) * grabMult;
        // Debug.Log("Force Added : " + force);
        selectedObject.AddForce(force);      
    }
}
