using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    public int playerCount = 1;
    public GameManager gameManager;
    public bool isTrigging = false;
    public GameObject playerChild;

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
        for(int i = 1; i<=playerCount; i++){
            childPosition = transform.position;
            if (i % 2 == 0) {
                childPosition.x = childNomalPosition * (i/2) + childPosition.x; 
            }
            else if (i > 1) {
                childPosition.x = -childNomalPosition * (i-1)/2 + childPosition.x; 
            }
            else{
                continue;
            }
            Instantiate(playerChild,childPosition, playerChild.transform.rotation)
            .transform.parent = transform;
            Debug.Log(childPosition.x);
        }
    }
}
