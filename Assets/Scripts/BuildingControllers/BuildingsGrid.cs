using UnityEngine;


public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(100, 100);

    private BuildingMonoBech[,] grid;
    private BuildingMonoBech flyingBuilding;
    private Camera mainCamera;

    private void Start()
    {
        grid = new BuildingMonoBech[GridSize.x, GridSize.y];

        mainCamera = Camera.main;
    }

    private void Update() // убрать Update и сделать отдельную систему
    {
        if (!flyingBuilding)
        {
            return;
        }

        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out var distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);

            var x = Mathf.RoundToInt(worldPosition.x);
            var y = Mathf.RoundToInt(worldPosition.z);

            var available = (!(x < 0
                              || x > GridSize.x - flyingBuilding.Size.x)) && !(y < 0
                                                                             || y > GridSize.y - flyingBuilding.Size.y);

            //if  available = false;

            if (available && IsPlaceTaken(x, y))
            {
                available = false;
            }

            flyingBuilding.transform.position = new Vector3(x, 0, y);
            flyingBuilding.SetTransparent(available);

            if (available && Input.GetMouseButtonDown(0))
            {
                PlaceFlyingBuilding(x, y);
            }
        }
    }

    public void StartPlacingBuilding(BuildingMonoBech buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab);
    }


    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if (grid[placeX + x, placeY + y])
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingBuilding;
            }
        }

        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }
}