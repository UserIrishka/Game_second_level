using System.Collections;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected bool IsTrapActivated;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ActivateTrap(collision));
    }

    protected abstract IEnumerator ActivateTrap(Collider2D collision);
}
