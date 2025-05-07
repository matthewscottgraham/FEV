using System.Collections;
using UnityEngine;

namespace FEV
{
    public class CameraController : MonoBehaviour
    {
        // TODO need an event bus or something so that I dont have the dependencies of the different controllers
        [SerializeField] private InputController inputController;
        [SerializeField] private Transform cameraTransform;
    
        [Header("Camera Settings")]
        [SerializeField, Range(0.001f, 0.2f)] private float zoomSpeed;
        [SerializeField] private Vector2 zoomBounds = new Vector2(7f, 30f);
        [SerializeField] private Vector2 zoomAngleBounds = new Vector2(45f, 15f);

        private readonly float _timeToMove = 0.3f;
        private readonly Vector3 _centerOffset = new Vector3(0, 0, -25f);
    
        void OnEnable()
        {
            inputController.Moved += HandleMove;
            inputController.Zoomed += HandleZoom;
        }

        void OnDisable()
        {
            inputController.Moved += HandleMove;
            inputController.Zoomed -= HandleZoom;
        }

        void Start()
        {
            HandleZoom(0);
        }

        void CenterCamera(Vector3 position)
        {
            StopAllCoroutines();
            StartCoroutine(MoveCameraToPosition(position + _centerOffset));
        }

        IEnumerator MoveCameraToPosition(Vector3 position)
        {
            float time = 0;
            Vector3 startPosition = transform.position;
            while (time < _timeToMove)
            {
                transform.position = Vector3.Lerp(startPosition, position, time / _timeToMove);
                time += Time.deltaTime;
                yield return null;
            }
        }
        void HandleMove(Vector2 delta)
        {
            transform.position += new Vector3(delta.x, 0, delta.y);
        }

        void HandleZoom(float delta)
        {
            // Move the camera in the y-axis, clamping between the zoom bounds
            var newVector = cameraTransform.position + new Vector3(0, delta * zoomSpeed, 0);
            newVector.y = Mathf.Clamp(newVector.y, zoomBounds.x, zoomBounds.y);
            cameraTransform.position = newVector;

            // Rotate the camera along its x-axis dependent on its height.
            var zoomPercent = Mathf.InverseLerp(zoomBounds.x, zoomBounds.y, newVector.y);
            cameraTransform.rotation = Quaternion.Euler(Mathf.Lerp(zoomAngleBounds.x, zoomAngleBounds.y, zoomPercent), 0,0);
        }
    }
}
