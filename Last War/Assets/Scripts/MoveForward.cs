using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float TrigSpeed = 3; //트리그 속도
    private float maxZ = -12;

    void Update()
    {
        transform.Translate(Vector3.back * TrigSpeed * Time.deltaTime); //트리그가 Player쪽으로 계속해서 이동

        //트리그의 z값이 maxZ값보다 작으면 실행
        if(transform.position.z < maxZ){
            Destroy(gameObject); //오브젝트 삭제제
        }
    }
}
