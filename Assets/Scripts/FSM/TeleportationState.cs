using System;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationState : BaseState<BossStates>
{
    private Setting _settings;

    [Serializable]
    public class Setting
    {
        public Transform BossTransform;
        public List<Transform> Points;
    }

    public TeleportationState(BossStates estate, Setting setting) : base(estate)
    {
        _settings = setting;

    }

    public override void EnterToState()
    {
        Transform pointToTeleport = _settings.Points[UnityEngine.Random.Range(0, _settings.Points.Count)];
        _settings.BossTransform.position = pointToTeleport.position;
        if (UnityEngine.Random.Range(0, 2) == 0) 
        {
            ChangeState(BossStates.FireBallCasting);
        }
        else
        {
            ChangeState(BossStates.LightingBallCasting);
        }
    }

    public override void ExitToState()
    {
        
    }
}
