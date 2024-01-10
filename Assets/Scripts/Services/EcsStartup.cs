using Leopotam.Ecs;
using Services;
using UnityEngine;


public class EcsStartup : MonoBehaviour
{
    public StaticData configuration;

    public SceneData sceneData;
    //public UI ui;

    public static EcsWorld World;
    private EcsWorld _ecsWorld;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystems;

    private async void Start()
    {
        _ecsWorld = new EcsWorld();
       
        _updateSystems = new EcsSystems(_ecsWorld);
        _fixedUpdateSystems = new EcsSystems(_ecsWorld);
        RuntimeData runtimeData = new RuntimeData();
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_ecsWorld);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
#endif
        _updateSystems
            .Add(new BuildingPlacementSystem())
         .Add(new GridInitSystem())
        
        // .Add(new GridInitSystem())
        // .Add(new PauseSystem())
        // .Add(new PlayerRotationSystem())
        // .Add(new PlayerAnimationSystem())
        // .Add(new DamageSystem())
        // .Add(new EnemyDeathSystem())
        // .Add(new PlayerDeathSystem())
        // .Add(new WeaponShootSystem())
        // .Add(new SpawnProjectileSystem())
        // .Add(new ProjectileMoveSystem())
        // .Add(new ProjectileHitSystem())
        // .Add(new ReloadingSystem())
        .Inject(configuration)
        .Inject(sceneData);
        // .Inject(ui)
        // .Inject(runtimeData);

        _fixedUpdateSystems
            .Add(new RotatePlacingObject())
            .Add(new CameraControllerSystem())
            // .Add(new PlayerMoveSystem())
            .Inject(configuration)
        // .Add(new CameraFollowSystem())
         .Inject(sceneData);
        // .Inject(runtimeData);

         _updateSystems.Init();
         _fixedUpdateSystems.Init();
       // await UniTask.Delay(1000);
        World = _ecsWorld;
    }

    private void Update()
    {
         _updateSystems?.Run();
    }

    private void FixedUpdate()
    {
         _fixedUpdateSystems?.Run();
    }

    private void OnDestroy()
    {
       //  _ecsWorld.Destroy();
       //  _updateSystems.Destroy();
    }
}