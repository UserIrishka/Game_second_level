using UnityEngine;

public class IDLEState : BaseState<BossStates>
{
    private SpriteRenderer _bossSprite;
    private GameObject _canvas;
    private AudioSource _audioSource;

    public IDLEState(BossStates estate, SpriteRenderer bossSprite, GameObject Canvas, AudioSource audioSource) : base(estate)
    {
        _bossSprite = bossSprite;
        _canvas = Canvas;
        _audioSource = audioSource;
    }

    public override void EnterToState()
    {
        Debug.Log("Босс умер");
        _bossSprite.enabled = false;
        BossItem BossItem = GameObject.Instantiate<BossItem>(Resources.Load<BossItem>("Prefabs/BossItem"));
        BossItem.transform.position = _bossSprite.transform.position;
        BossItem.Inicialize(_audioSource);
        _canvas.SetActive(false);
    }

    public override void ExitToState()
    {
        
    }
}
