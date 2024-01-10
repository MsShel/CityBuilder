using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    public GameObject playerPrefab;
    public float playerSpeed;
    public float smoothTime;
    public Vector3 followOffset;
    public Vector2 _gridOffset;
    // Move _housePrefab from StaticData.
    public GameObject _housePrefab;
    public GameObject _simpleRoadPrefab;
    public Vector2Int GridSize = new (100, 100);
}