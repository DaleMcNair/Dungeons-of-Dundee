using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GoblinTween : MonoBehaviour
{
    GameObject go;
    Vector2 originalPosition;
    Quaternion originalRotation;
    Task animateTask;

    private void Awake()
    {
        go = gameObject;
        originalPosition = go.transform.localPosition;
        originalRotation = go.transform.localRotation;
        animateTask = new Task(GoblinAnimate(), false);

        animateTask.Finished += delegate (bool manual) {
            ResetPosition();
        };
    }

    public void StartTween()
    {
        animateTask.Start();
    }
    public void StopTween()
    {
        if (animateTask.Running)
        {
            iTween.Stop();
            animateTask.Stop();
        }
    }

    void ResetPosition ()
    {
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
    }

    public IEnumerator GoblinAnimate()
    {
        while (true)
        {
            iTween.MoveTo(go, iTween.Hash(
                "position", new Vector3(0, 0.25f),
                "time", 0.25f,
                "easeType", iTween.EaseType.easeOutSine,
                "islocal", true
            ));

            iTween.RotateTo(go, iTween.Hash(
                "rotation", new Vector3(0, 0, 5),
                "time", 0.25f,
                "easetype", iTween.EaseType.linear,
                "islocal", true
            ));

            yield return new WaitForSeconds(0.25f);

            iTween.MoveTo(go, iTween.Hash(
                "position", new Vector3(0, 0),
                "time", 0.25f,
                "easeType", iTween.EaseType.easeInSine,
                "isLocal", true
            ));

            iTween.RotateTo(go, iTween.Hash(
                "rotation", new Vector3(0, 0, 10),
                "time", 0.25f,
                "easetype", iTween.EaseType.linear,
                "islocal", true
            ));

            yield return new WaitForSeconds(0.3f);

            iTween.MoveTo(go, iTween.Hash(
                "position", new Vector3(0, 0.25f),
                "time", 0.25f,
                "easeType", iTween.EaseType.easeOutSine,
                "islocal", true
            ));

            iTween.RotateTo(go, iTween.Hash(
                "rotation", new Vector3(0, 0, -5),
                "time", 0.25f,
                "easetype", iTween.EaseType.linear,
                "islocal", true
            ));

            yield return new WaitForSeconds(0.25f);

            iTween.MoveTo(go, iTween.Hash(
                "position", new Vector3(0, 0),
                "time",  0.25f,
                "easeType", iTween.EaseType.easeInSine,
                "isLocal", true
            ));

            iTween.RotateTo(go, iTween.Hash(
                "rotation", new Vector3(0, 0, -10),
                "time",  0.25f,
                "easetype", iTween.EaseType.linear,
                "islocal", true
            ));

            yield return new WaitForSeconds(0.3f);
        }
    }
}
