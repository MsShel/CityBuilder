using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    public GameObject playerPrefab;
    public float playerSpeed;
    public float smoothTime;
    public Vector3 followOffset;
    public Vector2 _gridOffset;
    // public int playerHealth;
    public GameObject _housePrefab;
    public Vector2Int GridSize = new (100, 100);
}