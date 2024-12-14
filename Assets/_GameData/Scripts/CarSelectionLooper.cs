using System;
using UnityEngine;
using UnityEngine.UI;

public class CarSelectionLooper : MonoBehaviour
{
    public Scrollbar scrollbar;       // Reference to the Scrollbar component
    public float scrollSpeed = 0.5f;  // Speed of scrolling
    private bool movingRight = true;  // Direction flag
    private bool isMoving = false;    // Controls when movement starts

    void OnEnable()
    {
        // Start movement when the GameObject is enabled
        isMoving = true;
        movingRight = true; // Reset direction
        if (scrollbar != null)
            scrollbar.value = 0f; // Start from the left
    }

    void Update()
    {
        if (!isMoving || scrollbar == null)
            return;

        if (movingRight)
        {
            // Move the scrollbar to the right
            scrollbar.value += scrollSpeed * Time.deltaTime;

            if (scrollbar.value >= 1f)
            {
                // Change direction when it reaches the end
                movingRight = false;
            }
        }
        else
        {
            // Move the scrollbar back to the left
            scrollbar.value -= scrollSpeed * Time.deltaTime;

            if (scrollbar.value <= 0f)
            {
                // Stop movement when it reaches the start
                isMoving = false;
            }
        }
    }
}