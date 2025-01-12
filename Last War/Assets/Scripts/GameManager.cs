using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("할당 O")]
    public TrigData[] trigDatas; //생성할 트리그 데이터
    public EnemyData[] enemyDatas; //생성할 적 데이터
    [SerializeField] GameObject trigs; //복제할 트리그모음 프리팹
    [SerializeField] Material goodTrig; //+,* 트리그에 색깔
    [SerializeField] Material badTrig;  //- 트리그에 색깔
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemys;

    [Header("할당 X")]
    public int waves = 0; //게임 웨이브
    public int enemyWaves = 0;
    public bool gameStart = false;

    Vector3 basicCameraPosition = new Vector3(2.4f,1.8f,-7.8f);
    Quaternion basicCameraRotation = Quaternion.Euler(17.5f, -49, 2.63f);

    Quaternion targetRotation = Quaternion.Euler(30.5f, 0, 0);

    void Start(){
        canvas.SetActive(true);
        Camera.main.transform.position = basicCameraPosition;
        Camera.main.transform.rotation = basicCameraRotation;
    }

    void Update(){
        //게임시작되면 카메라 회전
        if(gameStart == true){
            Camera.main.transform.position =
            Vector3.MoveTowards(Camera.main.transform.position, new Vector3(0,6.5f,-12.6f), 6f * Time.deltaTime);
            Camera.main.transform.rotation = 
            Quaternion.RotateTowards(Camera.main.transform.rotation,targetRotation,55f * Time.deltaTime);
            }
    }
    
    //canvas에 있는 Start버튼을 클릭했을 때 실행
    public void StartBtnClick(){
        Destroy(canvas);
        InvokeRepeating("CreatTrig",1f,10f); //1초가 지난뒤 10초마다 CreatTrig함수 실행
        InvokeRepeating("CreatEnemy",1f,5f);
        gameStart = true; //게임 시작!
    }

    //적 생성
    void CreatEnemy(){
        SetEnemyInfo();
        enemyWaves++;
    }

    //적 데이터 설정
    void SetEnemyInfo(){
        Vector3 enemyPosition = enemyPrefab.transform.position;
        enemyPosition.x = Random.Range(-3.5f,3.5f);

        GameObject enemy = 
        Instantiate(enemyPrefab, enemyPosition, enemyPrefab.transform.rotation);
        enemy.transform.parent = enemys.transform;
        enemy.GetComponent<EnemyController>().count = enemyDatas[enemyWaves].enemyCount;
    }

    //트리그 생성
    void CreatTrig(){
        waves++; //wave에 1더하기
        SetTrigInfo(); //트리그에 색, 값등을 설정해주는 함수   
    }

    //주석달기 귀찮기도 하고 여기를 설명할지를 모르겠어서 그냥 안달아야지 - 1/11의 나
    void SetTrigInfo(){
        GameObject newTrigs = Instantiate(trigs, trigs.transform.position, trigs.transform.rotation);
        GameObject trig1 = newTrigs.transform.GetChild(0).gameObject;
        GameObject trig2 = newTrigs.transform.GetChild(1).gameObject;
        TMP_Text trigText1 = trig1.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text trigText2 = trig2.transform.GetChild(0).GetComponent<TMP_Text>();
        
        trigText1.text = trigDatas[waves-1].sign1 == "*" ? "X" + trigDatas[waves-1].num1.ToString() : 
        trigDatas[waves-1].sign1.ToString() + trigDatas[waves-1].num1.ToString();

        trigText2.text = trigDatas[waves-1].sign2 == "*" ? "X" + trigDatas[waves-1].num2.ToString() :
        trigDatas[waves-1].sign2.ToString() + trigDatas[waves-1].num2.ToString();

        trig1.GetComponent<MeshRenderer>().material = trigDatas[waves-1].sign1 == "-" ? badTrig : goodTrig;
        trig2.GetComponent<MeshRenderer>().material = trigDatas[waves-1].sign2 == "-" ? badTrig : goodTrig;

    }
}
