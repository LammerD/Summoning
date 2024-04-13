using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] float FactorMoveUp;
    [SerializeField] float Duration;

    public void OpenDoor()
    {
        StartCoroutine(MoveDoor());
    }

    IEnumerator MoveDoor()
    {
        var timeElapsed = 0f;
        var startPosition = transform.position;
        var up = startPosition + Vector3.up * FactorMoveUp;
        while (timeElapsed < Duration)
        {
            transform.position = Vector3.Lerp(startPosition, up, timeElapsed / Duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = up;
    }
}
