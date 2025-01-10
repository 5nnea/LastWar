using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float objectSpeed = 3;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * objectSpeed * Time.deltaTime);
        if(transform.position.z < -12){
            Destroy(gameObject);
        }
    }
}
