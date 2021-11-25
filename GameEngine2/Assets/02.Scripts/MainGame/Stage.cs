using UnityEngine;

public class Stage : MonoBehaviour
{
    public int stageNumber => _stageNumber;
    [SerializeField] private int _stageNumber;

    public static bool SpawnerOn = false;

    public void OnStage()
    {
        gameObject.SetActive(true);
        SpawnerOn = true;
    }

    public void OffStage()
    {
        gameObject.SetActive(false);
        SpawnerOn = false;
    }

    public bool IsOnStage()
    {
        return gameObject.activeSelf;
    }
}
