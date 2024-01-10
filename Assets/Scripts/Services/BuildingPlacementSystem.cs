using Leopotam.Ecs;
using UnityEngine;


namespace Services
{
    public class BuildingPlacementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Building> _filter;
        private readonly Camera _mainCamera;
        private StaticData _staticData;
        private SceneData _sceneData;


        public BuildingPlacementSystem()
        {
            _mainCamera = Camera.main;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var building = ref _filter.Get1(i);
               // ref var grid = ref _filter.Get2(i);

               var grid = _sceneData.Grid;
                if (building.IsPlaced)
                {
                    continue;
                }

                // if (Input.GetKey(KeyCode.R))
                // {
                //     building.BuildingTransform.Rotate(0, 90, 0);
                //     (building.Size.x, building.Size.y) = (building.Size.y, building.Size.x);
                //     (building.GridDrawController.Size.x, building.GridDrawController.Size.y) = (building.Size.x, building.Size.y);
                // }

                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (groundPlane.Raycast(ray, out var distance))
                {
                    Vector3 worldPosition = ray.GetPoint(distance);

                        var x = Mathf.RoundToInt(worldPosition.x);
                        var y = Mathf.RoundToInt(worldPosition.z);

                    var available = (!(x < 0 || x > _staticData.GridSize.x - building.Size.x))
                                    && !(y < 0 || y > _staticData.GridSize.y - building.Size.y);

                    if (available && IsPlaceTaken(x, y, building, grid))
                    {
                        available = false;
                    }

                    building.Position = new Vector2Int(x, y);
                    building.BuildingTransform.position = new Vector3(x, 0, y);

                    SetTransparent(available, building.MainRenderer);

                    if (available && Input.GetMouseButtonDown(0))
                    {
                        building.IsPlaced = true;
                        PlaceBuilding(ref building, x, y, grid);

                        _filter.GetEntity(i).Destroy();
                    }
                }
            }
        }

        private bool IsPlaceTaken(int placeX, int placeY, Building building, Grid grid)
        {
            // for (int x = 1; x < building.Size.x; x++)
            // {
            //     for (int y = 1; y < building.Size.y; y++)
            //     {
            for (var x = - building.Size.x / 2; x < building.Size.x - building.Size.x / 2; x++)
            {
                for (var y = -  building.Size.y / 2; y < building.Size.y -  building.Size.y / 2; y++)
                {
                    if (grid.TerrainGrid[placeX + x, placeY + y].IsPlaced)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void PlaceBuilding(ref Building building, int placeX, int placeY, Grid grid)
        {
            for (var x = - building.Size.x / 2; x < building.Size.x - building.Size.x / 2; x++)
            {
                for (var y = -  building.Size.y / 2; y < building.Size.y -  building.Size.y / 2; y++)
                {
                    grid.TerrainGrid[placeX + x, placeY + y] = building;
                }
            }

            if (building.PlacementObjectType == PlacementObjects.SimpleRoad)
            {
                
            }

            SetNormal(building.MainRenderer);
            // building = null;
            // Object.Instantiate(_staticData._housePrefab, new Vector3(placeX, 0, placeY), Quaternion.identity);
        }
        
        private void SetTransparent(bool available, MeshRenderer renderer)
        {
            renderer.material.color = available ? Color.green : Color.red;
        }

        private void SetNormal( MeshRenderer renderer)
        {
            renderer.material.color = Color.white;
        }
        private Vector2Int AdjustPositionToGrid(Vector2Int position, Vector2Int size)
        {
            int x = position.x;
            int y = position.y;

            // Adjust x
            if (x < 0)
            {
                x = 0;
            }
            else if (x + size.x > _staticData.GridSize.x)
            {
                x = _staticData.GridSize.x - size.x;
            }

            // Adjust y
            if (y < 0)
            {
                y = 0;
            }
            else if (y + size.y > _staticData.GridSize.y)
            {
                y = _staticData.GridSize.y - size.y;
            }

            return new Vector2Int(x, y);
        }
    }
}