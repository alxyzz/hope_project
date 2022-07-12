using UnityEngine;

public class OnlyVisibleWhenHigh : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
       
        DataStorage.GameManagerComponent.TripComponent.highOnlyGameObjects.Add(gameObject);
    }
}
