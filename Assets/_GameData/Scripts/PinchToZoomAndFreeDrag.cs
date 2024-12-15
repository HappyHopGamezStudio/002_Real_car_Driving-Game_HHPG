using System;
using ToastPlugin;
using UnityEngine;

public class PinchToZoomAndFreeDrag : MonoBehaviour
{
    public Transform CameraPosition;
    public Camera mainCamera; // The Camera that you want to control
    public float zoomSpeed = 0.1f; // Speed of zoom
    public float minCameraSize = 1000f; // Minimum camera size
    public float maxCameraSize = 2000f; // Maximum camera size
    public float dragSpeed = 0.1f; // Speed of dragging

    private Vector2 lastTouchPosition;
    private bool isDragging = false;

    void Update()
    {
        HandleZoom();
        HandleDrag();
    }

    void HandleZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
           // ToastHelper.ShowToast(" ITS Two ");
            // Calculate the distance between touches
            float prevTouchDeltaMag = (touch0.position - touch0.deltaPosition - (touch1.position - touch1.deltaPosition)).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            // Zoom based on the change in distance
            float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;
            float cameraSizeFactor = 1 - deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;

            // Adjust the camera's orthographic size (or field of view for perspective camera)
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize * cameraSizeFactor, minCameraSize, maxCameraSize);
        }
    }

    void HandleDrag()
    {
        // Allow dragging at any zoom level (this applies to both orthographic and perspective cameras)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 currentTouchPosition = touch.position;
                Vector2 deltaPosition = currentTouchPosition - lastTouchPosition;
               // ToastHelper.ShowToast(" ITS one ");
                // Move the camera based on the touch delta in the x and z axes only
                Vector3 deltaMovement = new Vector3(deltaPosition.x, 0f, deltaPosition.y) * dragSpeed;

                // Update the camera position, limiting movement to x and z axes
                Vector3 newCameraPosition = mainCamera.transform.position - deltaMovement;

                // Update the camera position with the new x and z movement, keeping y fixed
                mainCamera.transform.position = new Vector3(newCameraPosition.x, mainCamera.transform.position.y, newCameraPosition.z);

                lastTouchPosition = currentTouchPosition;
            }
        }

        if (Input.touchCount == 0)
        {
            isDragging = false;
        }
    }


    public void restPosiotn()
    {
        mainCamera.transform.position = CameraPosition.transform.position;
        mainCamera.orthographicSize = maxCameraSize;
    }

    private void OnEnable()
    {
        restPosiotn();
        mainCamera.orthographicSize = maxCameraSize;
    }
}
