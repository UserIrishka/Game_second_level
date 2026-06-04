using System.ComponentModel;
using UnityEngine;
using Zenject;

public class MainProjectInstaller  : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MasterSave>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}
