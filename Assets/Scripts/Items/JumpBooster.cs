using System.Collections;
using UnityEngine;

public class JumpBooster : Item
{
    public ParticleSystem JumpEffect;
    public override IEnumerator PickUpItem(PlayerStats player)
    {
        player.JumpForce += IncreaseValue;
        AudioSorce.PlayOneShot(AudioClip);
        JumpEffect.Play();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(IncreaseDuration);
        player.JumpForce -= IncreaseValue;
        GameObject.Destroy(gameObject);
    }
}
