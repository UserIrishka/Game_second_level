using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    private float _startPosition, _lenght;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _paralaxValue;
    [SerializeField] private bool _canChangeY;

    private void Start()
    {
        _startPosition = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void LateUpdate()
    {
        float distance = _camera.position.x * _paralaxValue;
        float movement = _camera.position.x * (1 - _paralaxValue);
        if (_canChangeY == false)
        {
            transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_startPosition + distance, _camera.position.y, transform.position.z);
        }
        if (movement > _startPosition + _lenght)
        {
            _startPosition = _startPosition + _lenght;
        }
         else if(movement < _startPosition - _lenght)
        {
            _startPosition -= _lenght;
        }
    }
}
