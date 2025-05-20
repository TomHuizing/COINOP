using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float zoomSpeed = 1f; // Speed of zooming in/out	
    [SerializeField] private float minZoom = 1f; // Minimum zoom level
    [SerializeField] private float maxZoom = 10f; // Maximum zoom level
    [SerializeField] private float moveSpeed = 10f; // Speed of camera movement

    private float cameraZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraZ = Camera.main.transform.position.z; // Store the initial Z position of the camera
    }

    // Update is called once per frame
    void Update()
    {
        ZoomCamera(Input.GetAxis("Mouse ScrollWheel")); 


        if(Input.GetKey(KeyCode.W))
        {
            MoveCamera(Vector2.up);
        }
        if(Input.GetKey(KeyCode.S))
        {
            MoveCamera(Vector2.down);
        }
        if(Input.GetKey(KeyCode.A))
        {
            MoveCamera(Vector2.left);
        }
        if(Input.GetKey(KeyCode.D))
        {
            MoveCamera(Vector2.right);
        }
    }

    private void MoveCamera(Vector3 direction)
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        Vector3 min = new(-Map.Instance.Size.x / 2 + halfWidth, -Map.Instance.Size.y / 2 + halfHeight, cameraZ);
        Vector3 max = new(Map.Instance.Size.x / 2 - halfWidth, Map.Instance.Size.y / 2 - halfHeight, cameraZ);
        // Move the camera in the specified direction
        Camera.main.transform.position = (Camera.main.transform.position + (moveSpeed * Time.deltaTime * Camera.main.orthographicSize * direction)).Clamp(min, max);
    }

    private void ZoomCamera(float zoomAmount)
    {
        if(Mathf.Approximately(zoomAmount, 0f))
            return; // Ignore if no zoom input
        // Adjust the camera's orthographic size based on the zoom amount
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomAmount * zoomSpeed * Camera.main.orthographicSize, minZoom, maxZoom);
    }

    
}
