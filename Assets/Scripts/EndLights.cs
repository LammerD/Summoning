using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class EndLights : MonoBehaviour
{
    [SerializeField] GameObject EyePrefab;
    [SerializeField] int AmountOfEyes;
    [SerializeField] float MinDistance;
    [SerializeField] float MaxDistance;

    public void OpenGameOverEyes()
    {
        StartCoroutine(OpenEyes(PointsOnSphere(AmountOfEyes)));
    }

    IEnumerator OpenEyes(Vector3[] pts)
    {
        for (int i = pts.Length - 1; i > 0; i--)
        {
            var value = pts[i];
            var eye = Instantiate(EyePrefab, value, Quaternion.identity);
            yield return new WaitForSeconds(GameManager.Instance.GameOverTime / pts.Length);
        }
    }

    Vector3[] PointsOnSphere(int n)
    {
        var upts = new List<Vector3>();
        var inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        var off = 2.0f / n;
        for (var k = 0; k < n; k++)
        {
            var y = k * off - 1 + (off / 2);
            var r = Mathf.Sqrt(1 - y * y);
            var phi = k * inc;
            var x = Mathf.Cos(phi) * r;
            var z = Mathf.Sin(phi) * r;

            upts.Add(new Vector3(transform.position.x + (x * Random.Range(MinDistance, MaxDistance)), transform.position.y + (y * Random.Range(MinDistance, MaxDistance)), transform.position.z + (z * Random.Range(MinDistance, MaxDistance))));
        }
        return upts.OrderBy(x => Random.value).ToArray();
    }
}