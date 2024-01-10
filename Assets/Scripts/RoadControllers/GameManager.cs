using System;
using UIControllers;
using UnityEngine;


namespace RoadControllers
{
    public class GameManager : MonoBehaviour
    {
        public RoadManager roadManager;
        public InputManager inputManager;
        public StructureManager structureManager;
        public ButtonListener uiController;

        private void Start()
        {
            uiController.OnRoadPlacement += RoadPlacementHandler;
        }

        private void SpecialPlacementHandler()
        {
            ClearInputActions();
            inputManager.OnMouseClick += structureManager.PlaceSpecial;
        }

        private void HousePlacementHandler()
        {
            ClearInputActions();
            inputManager.OnMouseClick += structureManager.PlaceHouse;
        }

        private void RoadPlacementHandler()
        {
            ClearInputActions();

            inputManager.OnMouseClick += roadManager.PlaceRoad;
            inputManager.OnMouseHold += roadManager.PlaceRoad;
            inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
        }

        private void ClearInputActions()
        {
            inputManager.OnMouseClick = null;
            inputManager.OnMouseHold = null;
            inputManager.OnMouseUp = null;
        }
    }
}