using Leopotam.Ecs;
using UnityEngine;


namespace Services
{
    public class CameraControllerSystem : IEcsRunSystem
    {
        private SceneData _sceneData;
        private float _sensitivity = 15f;
        private float _speed = 5f;
        private float _rotationSmoothTime = 0.005f;
        private float _currentYRotation;
        private float _yRotationVelocity;

        public void Run()
        {
            // Define the ground level
            float groundLevel = 2f;

            // Get the camera's forward and right vectors
            Vector3 forward = _sceneData._mainCamera.transform.forward;
            Vector3 right = _sceneData._mainCamera.transform.right;

            // Remove the Y component from the vectors to make the camera move only in the XZ plane
            forward.y = 0f;
            right.y = 0f;

            // Move the camera horizontally and vertically
            var moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
            var moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * _speed;
            Vector3 newPosition = _sceneData._mainCamera.transform.position + right * moveHorizontal + forward * moveVertical;

            // Move the camera up and down
            if (Input.GetKey(KeyCode.Q))
            {
                newPosition.y -= _speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                newPosition.y += _speed * Time.deltaTime;
            }

            // Clamp the camera's Y position to the ground level
            newPosition.y = Mathf.Max(newPosition.y, groundLevel);

            // Apply the new position to the camera
            _sceneData._mainCamera.transform.position = newPosition;

            // Rotate the camera
            if (Input.GetMouseButton(1)) // Right mouse button
            {
                float targetYRotation = _sceneData._mainCamera.transform.eulerAngles.y + Input.GetAxis("Mouse X") * _sensitivity;
                float targetXRotation = _sceneData._mainCamera.transform.eulerAngles.x - Input.GetAxis("Mouse Y") * _sensitivity;
                _currentYRotation = Mathf.SmoothDampAngle(_currentYRotation, targetYRotation, ref _yRotationVelocity, _rotationSmoothTime);

                // Apply the rotation in local coordinates
                _sceneData._mainCamera.transform.localRotation = Quaternion.Euler(targetXRotation, _currentYRotation, 0);
            }
        }
    }
}