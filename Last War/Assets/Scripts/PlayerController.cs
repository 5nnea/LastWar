using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager; //게임메니저 오브젝트
    public GameObject bullets; //총알을 자식으로 넣을 부모 오브젝트
    public GameObject playerChild; //플레이어자식 프리팹

    private float playerSpeed = 6; //플레이어 이동속도
    private int playerCount = 1; //플레이어 인원수
    private bool isTrigging = false; //트리그에 닿았는지 여부 확인 변수
    private float childNomalPosition = 0.8f; //플레이어자식의 기본 x값

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); //a,d 입력감지
        transform.Translate(Vector3.right * playerSpeed * h *Time.deltaTime); //왼쪽, 오른쪽으로 이동
    }

    //콜라이더 충돌시 실행
    void OnTriggerEnter(Collider collider){
        //충돌한 콜라이더의 태그가 Trig이고 현재 트리거에 닿아있지 않는 상태라면 실행
        if(collider.CompareTag("Trig") && isTrigging == false){
            isTrigging = true;
            float currentPositionX = transform.position.x;
            int currentWaves = gameManager.waves-1; 
            
            //플레이어의 위치로 왼쪽 트리그, 오른쪽 트리그 판명
            //콜라이더로 계산하면 겹쳤을 때 문제가 귀찮기도 하고 이건 2개밖에 없어서 이런식으로 해도됨.
            if(currentPositionX < 0){
                //trig1 데이터에 따라서 playerCount 변화
                if(gameManager.trigDatas[currentWaves].sign1 == "+"){
                    playerCount += gameManager.trigDatas[currentWaves].num1;
                }
                else if(gameManager.trigDatas[currentWaves].sign1 == "*"){
                    playerCount *= gameManager.trigDatas[currentWaves].num1;
                }
                else if(gameManager.trigDatas[currentWaves].sign1 == "-"){
                    playerCount -= gameManager.trigDatas[currentWaves].num1;
                }
                else{
                    Debug.Log("error1");
                }
            }
            else{
                //trig2 데이터에 따라서 playerCount 변화
                if(gameManager.trigDatas[currentWaves].sign2 == "+"){
                    playerCount += gameManager.trigDatas[currentWaves].num2;
                }
                else if(gameManager.trigDatas[currentWaves].sign2 == "*"){
                    playerCount *= gameManager.trigDatas[currentWaves].num2;
                }
                else if(gameManager.trigDatas[currentWaves].sign2 == "-"){
                    playerCount -= gameManager.trigDatas[currentWaves].num2;
                }
                else{
                    Debug.Log("error2");
                }
            }

            //플레이어 수가 0이면 게임오버
            if(playerCount < 0){
                Debug.Log("게임오버");
            }

            ChangePlayer(); //변화한 플레이어 수에 따라 이쁘게 정렬해주는 함수
        }
    }

    void OnTriggerExit(Collider collider){
        if(collider.CompareTag("Trig")){
            isTrigging = false;
        }
    }

    //이건 수학연산이여서 그냥 코드 복붙해서 주지 않을까 분석하고 싶은 사람만 분석해~
    void ChangePlayer(){
        for(int i = 1; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }

        Vector3 childPosition;

        int rows = (playerCount / 5 == 0) ? 1 : playerCount/5 + 1;
        int lastRowCount = playerCount % 5;
        for(int i = 0; i< rows; i++){
            int InRow = (i+1 == rows) ? lastRowCount : 5;
            for(int j = 1; j<=InRow; j++){
                childPosition = transform.position;

                if (i == 0){ 
                    if (j % 2 == 0){
                        childPosition.x = childNomalPosition * (j / 2) + childPosition.x; 
                    } 
                    else if (j > 1){
                        childPosition.x = -childNomalPosition * (j - 1) / 2 + childPosition.x; 
                    } 
                    else {
                        continue;
                    }
                } 
                else{

                    if (j % 2 == 0){
                        childPosition.x = childNomalPosition * (j / 2) + childPosition.x; 
                    } 
                    else if (j > 1){
                        childPosition.x = -childNomalPosition * (j - 1) / 2 + childPosition.x; 
                    }
                }

                childPosition.z =  -childNomalPosition * i + childPosition.z;
                Instantiate(playerChild,childPosition, playerChild.transform.rotation)
                .transform.parent = transform;
                Debug.Log(childPosition.x);
            }
        }
    }
}
