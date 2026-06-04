using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class BossStateMachine : StateInspector<BossStates>
{
    public BossStateMachine(AudioSource audioSource, MonoBehaviourProcess monoBehaviourProcess, FireBallCastingState.Setting fireballSetting, LightingCastingState.Setting lightingSetting, TeleportationState.Setting teleportationSetting, SpriteRenderer bossSprite, GameObject Canvas)
    {
        FireBallCastingState fireBallCastingState = new FireBallCastingState(BossStates.FireBallCasting, monoBehaviourProcess, fireballSetting);


        LightingCastingState lightingCastingState = new LightingCastingState(BossStates.LightingBallCasting, lightingSetting, monoBehaviourProcess);

        IDLEState idle = new IDLEState(BossStates.IDLE, bossSprite, Canvas, audioSource);

        TeleportationState teleportationState = new TeleportationState(BossStates.Teleport, teleportationSetting);

        States.Add(BossStates.FireBallCasting, fireBallCastingState);
        States.Add(BossStates.LightingBallCasting, lightingCastingState);
        States.Add(BossStates.IDLE, idle);
        States.Add(BossStates.Teleport, teleportationState);

        StartStateMachine(BossStates.FireBallCasting);
    }
    public void ChangeState(BossStates state) 
    { 
         StartStateMachine(state);
    }
    public void DisableMachine()
    {
        StartStateMachine(BossStates.IDLE);
        //CurentState.
    }
}
