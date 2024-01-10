using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;


namespace Services
{
    public class RotatePlacingObject : IEcsRunSystem
    {
        private EcsFilter<Building> _filter;

        public void Run()
        {
            if (Input.GetKey(KeyCode.R))
            {
                var building = _filter.Get1(0);
                            building.BuildingTransform.Rotate(0, 90, 0);
            }
            
        }
    }
}