using System.Collections;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private GameObject currentPickUpItem;
    private Rigidbody currentPickUpItemRigidbodyComponent;

    private IEnumerator Start()
    {
        while (true)
        {
            TryPickUp();
            PickUpItemHandle();

            yield return null;
        }
    }

    private void TryPickUp()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2.5f, 11 << 11))
            {
                PickUpItemDrop();

                currentPickUpItem = hit.collider.gameObject;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (HasPickUpItem())
            {
                currentPickUpItemRigidbodyComponent.AddForce(transform.forward * 8f, ForceMode.Impulse);

                if (currentPickUpItem.GetComponent<TNT>() != null)
                {
                    currentPickUpItem.GetComponent<TNT>().Explosion(Random.Range(2.75f, 4.5f));
                }

                PickUpItemDrop();
            }
        }
    }

    private void PickUpItemHandle()
    {
        if (currentPickUpItem != null)
        {
            if (currentPickUpItemRigidbodyComponent == null)
            {
                currentPickUpItemRigidbodyComponent = currentPickUpItem.GetComponent<Rigidbody>();

                currentPickUpItemRigidbodyComponent.constraints = RigidbodyConstraints.FreezeAll;
            }

            currentPickUpItem.transform.SetParent(transform);

            currentPickUpItem.transform.localPosition = Vector3.forward * 3.5f;
        }
    }

    // Null is return to false
    private bool HasPickUpItem()
    {
        return currentPickUpItem != null;
    }

    private void PickUpItemDrop()
    {
        if (currentPickUpItem != null)
        {
            currentPickUpItem.transform.SetParent(null);

            currentPickUpItem = null;
        }

        if (currentPickUpItemRigidbodyComponent != null)
        {
            currentPickUpItemRigidbodyComponent.constraints = RigidbodyConstraints.None;

            currentPickUpItemRigidbodyComponent = null;
        }
    }
}
