using Leopotam.Ecs;
using UnityEngine;


namespace Services
{
    public class BuildingPlacementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Building> _filter;
        private readonly Camera _mainCamera;
        private StaticData _staticData;

        public BuildingPlacementSystem()
        {
            _mainCamera = Camera.main;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var building = ref _filter.Get1(i);

                if (!building.IsPlaced)
                {
                    var groundPlane = new Plane(Vector3.up, Vector3.zero);
                    Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (groundPlane.Raycast(ray, out var distance))
                    {
                        Vector3 worldPosition = ray.GetPoint(distance);

                        var x = Mathf.RoundToInt(worldPosition.x);
                        var y = Mathf.RoundToInt(worldPosition.z);

                        var available = (!(x < 0 || x > _staticData.GridSize.x - building.Size.x))
                                        && !(y < 0 || y > _staticData.GridSize.y - building.Size.y);

                        if (available && IsPlaceTaken(x, y))
                        {
                            available = false;
                        }

                        building.Position = new Vector2Int(x, y);

                        // Set transparency based on availability

                        available = true; // Remove this. It's here just to make the code compile.
                        if (available && Input.GetMouseButtonDown(0))
                        {
                            PlaceBuilding(ref building, x, y);

                            building.IsPlaced = true;

                            // delete building from world
                            _filter.GetEntity(i).Destroy();
                        }
                    }
                }
            }
        }

        private bool IsPlaceTaken(int placeX, int placeY)
        {
            // add logic to check if place is taken

            return true;
        }

        private void PlaceBuilding(ref Building building, int placeX, int placeY)
        {
            Object.Instantiate(_staticData._housePrefab, new Vector3(placeX, 0, placeY), Quaternion.identity);
        }
    }
}