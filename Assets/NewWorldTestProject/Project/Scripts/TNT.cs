using System.Collections;
using UnityEngine;

public class TNT : WorldObject
{
    [SerializeField]
    private GameObject explosionEffectGroup;
    [SerializeField]
    private GameObject explositonArea;

    public void Explosion(float time) => StartCoroutine(ExplosionCoroutine(time));

    private IEnumerator ExplosionCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(ExplosionArea());

        explosionEffectGroup.SetActive(true);

        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        for (int i = 1; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].enabled = false;
        }

        while (explositonArea.activeSelf)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }

    private IEnumerator ExplosionArea()
    {
        explositonArea.SetActive(true);

        float percent = 0f;

        while (percent < 1f)
        {
            percent += Time.deltaTime * 0.25f;

            explositonArea.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 9724f, percent);

            yield return null;
        }

        explositonArea.SetActive(false);
    }
}
