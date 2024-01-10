using UnityEngine;


namespace BuildingControllers
{
    public class BuildingMonoBeh : MonoBehaviour // rename to GridDrawController
    {
        public Renderer MainRenderer;
        public Vector2Int Size = Vector2Int.one;

        public void SetTransparent(bool available)
        {
            MainRenderer.material.color = available ? Color.green : Color.red;
        }

        public void SetNormal()
        {
            MainRenderer.material.color = Color.white;
        }

        private void OnDrawGizmos()
        {
            for (var x = - Size.x / 2; x < Size.x - Size.x / 2; x++)
            {
                for (var y = - Size.y / 2; y < Size.y - Size.y / 2; y++)
                {
                    Gizmos.color = (x + y) % 2 == 0
                        ? new Color(0.88f, 0f, 1f, 0.3f)
                        : new Color(1f, 0.68f, 0f, 0.3f);

                    Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
                }
            }
        }
    }
}