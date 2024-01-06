using Leopotam.Ecs;
using Services;
using UnityEngine;

// Replace with your actual namespace

namespace BuildingControllers
{
    public class BuildingCreator : MonoBehaviour
    {
        private EcsWorld _world;

        private void Start()
        {
            _world = EcsStartup.World; // Get the active ECS world
        }
        public void StartAddBuilding()
        {
            //_world = new EcsWorld();

            _world = EcsStartup.World; 
            var entity = _world.NewEntity();
            ref var building = ref entity.Get<Building>();
            building.Size = new Vector2Int(10, 10); // Replace with your actual values
            building.Position = new Vector2Int(0, 0); // Replace with your actual values
            building.IsPlaced = false;
            // Initialize the building component as needed
        }
    }
}