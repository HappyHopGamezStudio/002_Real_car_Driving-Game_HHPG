using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

namespace CnControls
{
    public class TouchPad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public Camera CurrentEventCamera { get; set; }

        public string HorizontalAxisName = "Horizontal";
        public string VerticalAxisName = "Vertical";
        
        public bool PreserveInertia = true;
        public float sensitivity = 0.1f;   // Sensitivity for touch movement
        public float Friction = 3f;

        public CinemachineFreeLook freeLookCamera;  // Reference to the Cinemachine FreeLook Camera

        private VirtualAxis _horizontalAxis;
        private VirtualAxis _verticalAxis;
        private int _lastDragFrameNumber;
        private bool _isCurrentlyTweaking;

        [Tooltip("Constraints on the joystick movement axis")]
        public ControlMovementDirection ControlMoveAxis = ControlMovementDirection.Both;

        private void OnEnable()
        {
            _horizontalAxis = _horizontalAxis ?? new VirtualAxis(HorizontalAxisName);
            _verticalAxis = _verticalAxis ?? new VirtualAxis(VerticalAxisName);

            CnInputManager.RegisterVirtualAxis(_horizontalAxis);
            CnInputManager.RegisterVirtualAxis(_verticalAxis);
        }

        private void OnDisable()
        {
            CnInputManager.UnregisterVirtualAxis(_horizontalAxis);
            CnInputManager.UnregisterVirtualAxis(_verticalAxis);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            // Process input based on movement direction constraints
            if ((ControlMoveAxis & ControlMovementDirection.Horizontal) != 0)
            {
                _horizontalAxis.Value = eventData.delta.x * sensitivity;
            }
            if ((ControlMoveAxis & ControlMovementDirection.Vertical) != 0)
            {
                _verticalAxis.Value = eventData.delta.y * sensitivity;
            }

            // Apply rotation to the Cinemachine camera
            if (freeLookCamera != null)
            {
                freeLookCamera.m_XAxis.Value += _horizontalAxis.Value;
                freeLookCamera.m_YAxis.Value -= _verticalAxis.Value;  // Invert for natural up/down movement
            }

            _lastDragFrameNumber = Time.renderedFrameCount;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isCurrentlyTweaking = false;
            if (!PreserveInertia)
            {
                _horizontalAxis.Value = 0f;
                _verticalAxis.Value = 0f;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isCurrentlyTweaking = true;
            OnDrag(eventData);
        }

        private void Update()
        {
            if (_isCurrentlyTweaking && _lastDragFrameNumber < Time.renderedFrameCount - 2)
            {
                _horizontalAxis.Value = 0f;
                _verticalAxis.Value = 0f;
            }

            if (PreserveInertia && !_isCurrentlyTweaking)
            {
                _horizontalAxis.Value = Mathf.Lerp(_horizontalAxis.Value, 0f, Friction * Time.deltaTime);
                _verticalAxis.Value = Mathf.Lerp(_verticalAxis.Value, 0f, Friction * Time.deltaTime);
            }
        }
    }
}
