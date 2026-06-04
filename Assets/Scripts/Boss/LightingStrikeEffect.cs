using DG.Tweening;
using UnityEngine;

public class LightingStrikeEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    private void Awake()
    {
        _sprite.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
    }
}
