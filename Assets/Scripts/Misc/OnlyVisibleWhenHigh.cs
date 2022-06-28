using UnityEngine;

public class OnlyVisibleWhenHigh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataStorage.GameManagerComponent.TripComponent.highOnlyGameObjects.Add(gameObject);
        gameObject.SetActive(false);
    }
}
