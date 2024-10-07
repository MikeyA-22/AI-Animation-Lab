using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class Transition : MonoBehaviour
{
    public SplineContainer container;

    [SerializeField] float speed = 0.1f;
    [SerializeField] float transitionDuration = 1f; // Duration of the transition between splines

    SplinePath[] paths = new SplinePath[2];
    float t = 0f;
    int currentPathIndex = 0;

    IEnumerator CarPathCoroutine()
    {
        while (true)
        {
            t = 0f;
            var path = paths[currentPathIndex];
            var nextPath = paths[(currentPathIndex + 1) % 2];

            while (t <= 1f)
            {
                var pos = path.EvaluatePosition(t);
                var direction = path.EvaluateTangent(t);
                transform.position = pos;
                transform.LookAt(pos + direction);
                t += speed * Time.deltaTime;
                yield return null;

                // Check for key press to transition to the next path
                if (Input.GetKeyDown(KeyCode.W))
                {
                    yield return StartCoroutine(TransitionToNextPath(path, nextPath));
                    currentPathIndex = (currentPathIndex + 1) % 2;
                    break;
                }
            }

            // Automatically transition to the next path when reaching the end
            if (t > 1f)
            {
                yield return StartCoroutine(TransitionToNextPath(path, nextPath));
                currentPathIndex = (currentPathIndex + 1) % 2;
            }
        }
    }

    IEnumerator TransitionToNextPath(SplinePath currentPath, SplinePath nextPath)
    {
        float transitionTime = 0f;
        Vector3 startPos = currentPath.EvaluatePosition(1f);
        Vector3 endPos = nextPath.EvaluatePosition(0f);
        Vector3 startDir = currentPath.EvaluateTangent(1f);
        Vector3 endDir = nextPath.EvaluateTangent(0f);

        while (transitionTime <= transitionDuration)
        {
            float lerpFactor = transitionTime / transitionDuration;
            transform.position = Vector3.Lerp(startPos, endPos, lerpFactor);
            transform.LookAt(Vector3.Lerp(startPos + startDir, endPos + endDir, lerpFactor));
            transitionTime += Time.deltaTime;
            yield return null;
        }
    }

    void Start()
    {
        var localToWorldMatrix = container.transform.localToWorldMatrix;
        paths[0] = new SplinePath(new[]
        {
            new SplineSlice<Spline>(container.Splines[0], new SplineRange(0, 16), localToWorldMatrix),
        });
        
        paths[1] = new SplinePath(new[]
        {
            new SplineSlice<Spline>(container.Splines[1], new SplineRange(0, 12), localToWorldMatrix),
        });
        
        StartCoroutine(CarPathCoroutine());
    }
}
