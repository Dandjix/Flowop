using UnityEngine;

public class CentralSnowflake : MonoBehaviour
{
    [SerializeField] private float turnsPerSecond;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, turnsPerSecond * Time.time * 360);
    }
}
