using UnityEngine;
using Zenject;

public class IBoss : EnemyStats 
{
    private BossStateMachine _stateMachine;

    [Inject]
    private void Constract(BossStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    private void OnDestroy()
    {
        //_stateMachine.DisableMachine(); 
    }
}
