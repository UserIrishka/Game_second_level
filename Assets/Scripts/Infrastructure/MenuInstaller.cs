using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;
    public override void InstallBindings()
    {
        
    }
    public override void Start()
    {
        Container.Resolve<MasterSave>().LoadAllData();
        _settings.LoadSettings();
    }
}
