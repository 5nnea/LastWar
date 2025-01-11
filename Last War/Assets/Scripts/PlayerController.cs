using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    //복제할 플레이어자식 프리팹
    public GameObject playerChild;
    public GameObject bullets;

    private float speed = 6;
    private int playerCount = 1;
    private bool isTrigging = false;

    private float childNomalPosition = 0.8f;

    void Start()
    {
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * h *Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Trig") && isTrigging == false){
            float currentPositionX = transform.position.x;
            isTrigging = true;
            int currentWaves = gameManager.waves-1; 
            if(currentPositionX < 0){
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
            if(playerCount < 0){
                Debug.Log("게임오버");
            }
            ChangePlayer();
        }
    }

    void OnTriggerExit(Collider collider){
        if(collider.CompareTag("Trig")){
            isTrigging = false;
        }
    }

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
