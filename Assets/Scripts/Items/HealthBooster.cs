using System.Collections;
using UnityEngine;

public class HealthBooster : Item
{
    public ParticleSystem HillEffect;
    public override IEnumerator PickUpItem(PlayerStats player)
    {
        player.Health += (int)IncreaseValue;
        if (player.Health > 100)
        {
            player.Health = 100;
        }
        AudioSorce.PlayOneShot(AudioClip);
        HillEffect.Play();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        //player._slider.value = player.Health;
        player.UpdateHealthUI();

        GameObject.Destroy(gameObject);

        yield return null; 
    }
}

