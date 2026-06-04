using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightingCastingState : BaseState<BossStates>
{
    private Setting _setting;
    private MonoBehaviourProcess _mono;

    [Serializable]
    public class Setting
    {
        public float IntervalCasting;
        public int MaxAmountOfLighting;
        public List<Transform> CastingPositions, StrikePosition;
        public int AmountCast;
        public int MaxDelay;
        public int MinDelay;
        public LayerMask PlayerLayer;
    }
    public LightingCastingState(BossStates estate, Setting setting, MonoBehaviourProcess mono) : base(estate)
    {
        _setting = setting;
        _mono = mono;
    }

    private IEnumerator CastingSpell()
    {
        for (int x = 0; x < _setting.AmountCast; x++)
        {
            
            List<int> positionsID = GetRandomPositionID(_setting.MaxAmountOfLighting);
            for (int i = 0; i < _setting.MaxAmountOfLighting; i++)
            {
                LightingBall lightingBall = GameObject.Instantiate<LightingBall>(Resources.Load<LightingBall>("Prefabs/LightingBall"));
                LightingStrikeEffect effect = GameObject.Instantiate<LightingStrikeEffect>(Resources.Load<LightingStrikeEffect>("Prefabs/LightingStrikePosition"));
                if (positionsID.Count > 0)
                {
                    if (lightingBall != null && effect != null && positionsID != null)
                    {
                        lightingBall.transform.position = _setting.CastingPositions[positionsID[i]].position;
                        effect.transform.position = _setting.StrikePosition[positionsID[i]].position;
                        _mono.StartCoroutine(DelayBeforeStrike(lightingBall, effect));
                    }
                }
                else
                {
                    yield return null;
                }
                //создаём префаб молнии 
                //создаём префаб места удара молнии 
                //после некоторой задержки бьём молнией в точку удара, создаём сферу для определения находится ли игрок в этом месте и наносим урон
            }
            yield return new WaitForSeconds(_setting.IntervalCasting);
        }
        ChangeState(BossStates.FireBallCasting);
    }
    private IEnumerator DelayBeforeStrike(LightingBall lightingBall, LightingStrikeEffect lightingStrikeEffect)
    {
        yield return new WaitForSeconds(Random.Range(_setting.MinDelay, _setting.MaxDelay));
        lightingBall.TrailRenderer.enabled = true;
        lightingBall.transform.DOMove(lightingStrikeEffect.transform.position, 0.2f);
        Collider2D player = Physics2D.OverlapCircle(lightingStrikeEffect.transform.position, lightingBall.AttackRadius, _setting.PlayerLayer);
        if (player != null)
        {
            if (player.TryGetComponent(out IDamageAble stats))
            {
                stats.GetDamage(lightingBall.AttackValue);
            }
        }
        yield return new WaitForSeconds(1);
        GameObject.Destroy(lightingBall.gameObject);
        GameObject.Destroy(lightingStrikeEffect.gameObject);
    }

    private List<int> GetRandomPositionID(int positionCount)
    {
        List<int> result = new List<int>();
        List<int> positionsID = new List<int>();
        for (int i = 0; i < _setting.CastingPositions.Count; i++) 
        { 
            positionsID.Add(i);
        }
        for (int i = 0; i < positionCount; i++)
        { 
            int ID = positionsID[Random.Range(0, positionsID.Count)];
            result.Add(ID);
            positionsID.Remove(ID);
        }
        return result;
    }

    public override void EnterToState()
    {
        Debug.Log("Молния");
        _mono.StartCoroutine(CastingSpell());
    }

    public override void ExitToState()
    {
        Debug.Log("Выход из состояния молния");
    }
}
