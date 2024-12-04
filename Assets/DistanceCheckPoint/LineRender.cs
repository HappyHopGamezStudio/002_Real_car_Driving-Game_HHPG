using UnityEngine;

public class LineRender : MonoBehaviour
{
    public Transform start, end;
    public LineRenderer lineRenderer;

    private void FixedUpdate()
    {
        if (lineRenderer && start && end)
        {
            lineRenderer.SetPosition(0, start.position);
            lineRenderer.SetPosition(1, end.position);
        }
    }
}
