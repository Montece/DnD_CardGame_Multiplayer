using UnityEngine;

public class UiMovement : MonoBehaviour
{
    public bool IsMoving;
    public bool MovingForward;
    public float Speed;
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public float StartTime;
    public float Distance;
    public RectTransform Target;
    public float MovingTime;

    float DistanceMoved;
    float Koeff;

    public void StartMove(Transform target, Vector2 endPosition, Transform parent, float speed = 1f)
    {
        if (Target != null) Destroy(Target.gameObject);
        Target = Instantiate(target.gameObject).GetComponent<RectTransform>();
        Target.transform.SetParent(parent);
        StartTime = Time.time;
        StartPosition = target.transform.position;
        EndPosition = endPosition;
        Speed = speed;
        Distance = Vector3.Distance(StartPosition, EndPosition);
        MovingTime = Distance / Speed * 2;
        MovingForward = true;
        IsMoving = true;
    }

    void Update()
    {
        if (IsMoving)
        {
            DistanceMoved = (Time.time - StartTime) * Speed * Time.deltaTime;
            Koeff = DistanceMoved / Distance;

            if (MovingForward)
            {
                Target.position = Vector3.Lerp(StartPosition, EndPosition, Koeff);
                if (Target.position == new Vector3(EndPosition.x, EndPosition.y, 0))
                {
                    StartTime = Time.time;
                    Vector2 temp = EndPosition;
                    EndPosition = StartPosition;
                    StartPosition = temp;
                    Distance = Vector3.Distance(StartPosition, EndPosition);
                    MovingForward = false;
                }
            }
            else
            {
                Target.position = Vector2.Lerp(StartPosition, EndPosition, Koeff);
                if (Target.position == new Vector3(EndPosition.x, EndPosition.y, 0))
                {
                    IsMoving = false;
                    Destroy(Target.gameObject);
                }
            }
        }
    }
}
