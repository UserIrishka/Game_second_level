using UnityEngine;
using Zenject;

public class LaunchInTheStoryZone : MonoBehaviour
{
    private DialogHandler _dialogHandler;
    [SerializeField] private DialogData _dialogData;
    [Inject]
    public void Constract(DialogHandler dialogHandler)
    {
        _dialogHandler = dialogHandler;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Срабатывание метода");
        if (other.TryGetComponent(out Player_movement player))
        {
            _dialogHandler.StartDialog(_dialogData);
        }
    }
}
