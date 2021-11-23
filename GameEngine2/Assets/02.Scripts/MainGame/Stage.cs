using UnityEngine;

public class Stage : MonoBehaviour
{
    public int stageNumber => _stageNumber;
    [SerializeField] private int _stageNumber;

    public void OnStage()
    {
        gameObject.SetActive(true);
    }

    public void OffStage()
    {
        gameObject.SetActive(false);
    }

    public bool IsOnStage()
    {
        return gameObject.activeSelf;
    }
}
