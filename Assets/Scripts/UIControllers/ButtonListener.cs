using BuildingControllers;
using UnityEngine;
using UnityEngine.UI;


namespace UIControllers
{
    public class ButtonListener : MonoBehaviour
    {
        public Button buildingButton;
        public Button roadPartButton;

        public BuildingCreator buildingCreator;

        private void Start()
        {
            buildingButton.onClick.AddListener(() => buildingCreator.StartAddBuilding(PlacementObjects.BlueHouse));
            roadPartButton.onClick.AddListener(() => buildingCreator.StartAddBuilding(PlacementObjects.SimpleRoad));
        }
    }
}