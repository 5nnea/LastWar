using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("할당 O")]
    [SerializeField] GameObject spawner; //총구위치
    [SerializeField] GameObject bullet; //생성할 총알 프리팹
    
    [Header("할당 X")]
    private float yeonsaSpeed = 0.5f; //연사 속도

    private GameObject bullets; //총알의 부모

    void Start()
    {
        InvokeRepeating("CreatBullet", 0f, yeonsaSpeed); //연사속도마다 CreatBullet 함수 실행

        //bullets에 PlayerController에서 있는 bullets 게임오브젝트 할당
        bullets = transform.root.gameObject.GetComponent<PlayerController>().bullets; 
    }

    void CreatBullet(){
        //gameStart변수값이 true라면 실행
        if(transform.root.gameObject.GetComponent<PlayerController>().gameManager.gameStart == true){
            //bullet을 spawner위치에서 복제하고 bullets의 자식으로 넣기
            Instantiate(bullet, spawner.transform.position, bullet.transform.rotation).transform.parent = bullets.transform;
        }
    }
}
