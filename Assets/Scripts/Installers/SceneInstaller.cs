using System;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayer();
        BindStates();
        BindUI();
    }

    private void BindPlayer()
    {
    
        Container.Bind<CameraFollow>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerAnimations>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Shooting>().FromComponentInHierarchy().AsSingle();
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