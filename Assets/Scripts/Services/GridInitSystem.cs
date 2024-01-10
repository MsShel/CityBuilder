using Leopotam.Ecs;


namespace Services
{
    public class GridInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private StaticData _staticData;
        private SceneData _sceneData;

        public void Init()
        {
            EcsEntity playerEntity = _ecsWorld.NewEntity();

            ref var grid = ref playerEntity.Get<Grid>();
            grid.TerrainGrid = new Building[_staticData.GridSize.x, _staticData.GridSize.y];
            _sceneData.Grid = grid;

        }
    }
}