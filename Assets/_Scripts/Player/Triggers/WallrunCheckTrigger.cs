using UnityEngine;

public class WallrunCheckTriger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Wallrun"))
        {
            Debug.Log("wallrun start test");

            Debug.DrawRay(transform.parent.position + new Vector3(0f, 1.5f, 0f), transform.parent.rotation * Vector3.right * 1.1f, Color.red, 2f);
            Physics.Raycast(transform.parent.position + new Vector3(0f, 1.5f, 0f), transform.parent.rotation * Vector3.right, out RaycastHit hitRight, 1.1f, LayerMask.GetMask("Level"));
            Debug.DrawRay(transform.parent.position + new Vector3(0f, 1.5f, 0f), transform.parent.rotation * Vector3.left * 1.1f, Color.red, 2f);
            Physics.Raycast(transform.parent.position + new Vector3(0f, 1.5f, 0f), transform.parent.rotation * Vector3.left, out RaycastHit hitLeft, 1.1f, LayerMask.GetMask("Level"));

            if (hitLeft.normal == new Vector3(0f, 0f, 0f))
            {
                Debug.Log(hitRight.normal);
                GameBootstrap.MessageBus.Publish(new Player_WallrunEnterMessage(hitRight.normal));
            }


            if (hitRight.normal == new Vector3(0f, 0f, 0f))
            {
                Debug.Log(hitLeft.normal);
                GameBootstrap.MessageBus.Publish(new Player_WallrunEnterMessage(hitLeft.normal));
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Wallrun"))
        {
            GameBootstrap.MessageBus.Publish(new Player_WallrunExitMessage());
        }
    }
}
