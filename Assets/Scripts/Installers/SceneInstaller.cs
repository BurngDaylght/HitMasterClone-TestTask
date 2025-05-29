using System;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindStates();
        BindUI();
    }

    private void BindStates()
    {
        Container.Bind<GameState>().FromComponentInHierarchy().AsSingle();
    }


    private void BindUI()
    {
        Container.Bind<LevelUI>().FromComponentInHierarchy().AsSingle();
    }
}