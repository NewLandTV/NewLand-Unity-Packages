using System.Collections;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    [SerializeField]
    protected bool useTickFunction;

    private IEnumerator Start()
    {
        if (useTickFunction)
        {
            while (true)
            {
                Tick(Time.deltaTime);

                yield return null;
            }
        }
    }

    protected virtual void Tick(float deltaTime)
    {
        // Write the code to run each frame ...
    }
}
