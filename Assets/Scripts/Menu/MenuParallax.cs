using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;

    private Vector2 _startPosition;
    private Vector3 _velocity;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, _startPosition + (offset * offsetMultiplier), ref _velocity, smoothTime);
    }
}
