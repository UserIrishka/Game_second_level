using System.Collections;
using UnityEngine;
using Zenject;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected float IncreaseValue;
    [SerializeField] protected float IncreaseDuration;
    protected AudioSource AudioSorce;
    [SerializeField] protected AudioClip AudioClip;

    [Inject]
    void Constract(AudioSource audioSource)
    {
        AudioSorce = audioSource;
    }
    public abstract IEnumerator PickUpItem(PlayerStats player);
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            StartCoroutine(PickUpItem(player));
        }
    }
}
