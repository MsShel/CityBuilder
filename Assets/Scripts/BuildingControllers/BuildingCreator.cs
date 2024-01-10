using Leopotam.Ecs;
using Services;
using UnityEngine;


namespace BuildingControllers
{
    public class BuildingCreator : MonoBehaviour
    {
        [SerializeField] private StaticData _staticData;
        private EcsWorld _world;

        private void Start()
        {
            _world = EcsStartup.World; // Get the active ECS world
        }

        public void StartAddBuilding(PlacementObjects placementType)
        {
            _world = EcsStartup.World;
            var entity = _world.NewEntity();
            ref var building = ref entity.Get<Building>();
            GameObject buildingGO;
            switch (placementType)
            {
                case PlacementObjects.BlueHouse:
                    buildingGO = Instantiate(_staticData._housePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    building.Size = _staticData._housePrefab.GetComponent<BuildingMonoBeh>().Size;
                    building.GridDrawController = buildingGO.GetComponent<BuildingMonoBeh>();
                    building.PlacementObjectType = PlacementObjects.BlueHouse;

                    break;
                case PlacementObjects.SimpleRoad:
                    buildingGO = Instantiate(_staticData._simpleRoadPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    building.Size = _staticData._simpleRoadPrefab.GetComponent<BuildingMonoBeh>().Size;
                    building.PlacementObjectType = PlacementObjects.SimpleRoad;

                    break;
                default:
                    buildingGO = Instantiate(_staticData._housePrefab, new Vector3(0, 0, 0), Quaternion.identity);

                    break;
            }

            building.MainRenderer = buildingGO.GetComponentInChildren<MeshRenderer>();

            var position = buildingGO.transform.position;
            building.Position.x = Mathf.RoundToInt(position.x);
            building.Position.y = Mathf.RoundToInt(position.z);
            building.IsPlaced = false;
            building.BuildingTransform = buildingGO.transform;
            building.BuildingObject = buildingGO;
            // Initialize the building component as needed
        }
    }
}