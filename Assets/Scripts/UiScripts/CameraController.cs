using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movement.z += 1;

        if (Input.GetKey(KeyCode.S))
            movement.z -= 1;

        if (Input.GetKey(KeyCode.D))
            movement.x += 1;

        if (Input.GetKey(KeyCode.A))
            movement.x -= 1;

        transform.position += movement.normalized * moveSpeed * Time.deltaTime;
    }
}
