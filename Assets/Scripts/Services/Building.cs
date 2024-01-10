using BuildingControllers;
using UnityEngine;


namespace Services
{
    public struct Building
    {
        public Vector2Int Size;
        public Vector2Int Position;
        public bool IsPlaced;
        public Transform BuildingTransform;
        public GameObject BuildingObject;
        public MeshRenderer MainRenderer;
        public BuildingMonoBeh GridDrawController;
        public PlacementObjects PlacementObjectType;
    }
}