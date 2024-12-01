using UnityEngine;

public class Porte : MonoBehaviour
{
    [SerializeField] private Transform porteBloc, openPos, closedPos;

    [SerializeField] private float timeToOpenClose = 1;

    private float status = 0;
    private float targetStatus = 0;

    public void Open()
    {
        targetStatus = 1;
    }
    public void Close()
    {
        targetStatus = 0;
    }

    private void Update()
    {
        status = Mathf.MoveTowards(status, targetStatus, Time.deltaTime / timeToOpenClose);

        porteBloc.transform.position = Vector2.Lerp(openPos.position, closedPos.position, status);
    }
}
