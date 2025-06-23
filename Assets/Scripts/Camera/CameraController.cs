using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float zoomSpeed = 0.1f; // Speed of zooming in/out	
    [SerializeField] private float minZoom = 1f; // Minimum zoom level
    [SerializeField] private float maxZoom = 10f; // Maximum zoom level
    [SerializeField] private float moveSpeed = 10f; // Speed of camera movement

    private float cameraZ;
    private Vector2 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraZ = Camera.main.transform.position.z; // Store the initial Z position of the camera
    }

    // Update is called once per frame
    void Update()
    {
        //ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));


        if (moveDirection != Vector2.zero)
            MoveCamera(moveDirection);
    }

    public void MoveCameraFromInput(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void MoveCamera(Vector2 direction)
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        Vector2 min = new(-Map.Instance.Size.x / 2 + halfWidth, -Map.Instance.Size.y / 2 + halfHeight);
        Vector2 max = new(Map.Instance.Size.x / 2 - halfWidth, Map.Instance.Size.y / 2 - halfHeight);
        // Move the camera in the specified direction
        Vector2 movement = ((Vector2)Camera.main.transform.position + (moveSpeed * Time.deltaTime * Camera.main.orthographicSize * direction)).Clamp(min, max);
        Camera.main.transform.position = new(movement.x, movement.y, cameraZ);
    }

    public void ZoomCameraFromInput(InputAction.CallbackContext context)
    {
        print("context: " + context);

        ZoomCamera(context.ReadValue<Vector2>().y);
    }

    private void ZoomCamera(float zoomAmount)
    {
        if (Mathf.Approximately(zoomAmount, 0f))
            return; // Ignore if no zoom input
        // Adjust the camera's orthographic size based on the zoom amount
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomAmount * zoomSpeed * Camera.main.orthographicSize, minZoom, maxZoom);
    }

    
}
