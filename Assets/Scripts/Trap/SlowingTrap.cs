using System.Collections;
using UnityEngine;

public class SlowingTrap : Trap
{
    [SerializeField] private float _slowingDuration;
    [SerializeField] private float _slowingValue;
    protected override IEnumerator ActivateTrap(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player) && IsTrapActivated == false)
        {
            IsTrapActivated = true;
            player.SpeedMovement -= _slowingValue;
            yield return new WaitForSeconds(_slowingDuration);
            player.SpeedMovement += _slowingValue;
            IsTrapActivated = false;
        }
        yield return null;
    }
}
