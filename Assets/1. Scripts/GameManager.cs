using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{

    public Transform[] EnemyRespawnPoints_Purple; // 적이 출현할 위치를 담을 배열
    public Transform[] EnemyRespawnPoints_Grey; // 적이 출현할 위치를 담을 배열
    public Transform[] EnemyRespawnPoints_Brown; // 적이 출현할 위치를 담을 배열
    public TalkManager talkManager; // 토크매니저를 가져오기 위한 변수.
    public FadeEffect fadeEffect;
    public GameObject deathMessagePanel; // 죽었을 때 메세지를 나타내는 게임오브젝트 
    public Transform PlayerRespawnPoint; // 플레이어가 리스폰 될 위치를 담을 배열 
    public GameObject Player; // 플레이어 게임오브젝트를 담을 변수
    public Slider hpbar;
    public Cinemachine.CinemachineBrain camera3;
    Enemy_RedDragon redDragon;
    public Cinemachine.CinemachineFreeLook freeLook;


    // 적을 생성할 주기 -> 리스폰 기능 사용 시 
    public float enemyCreateTime = 3.0f;

    // 적 캐릭터의 최대 생성 개수
    public int maxEnemy;
    public int talkIndex;
    public bool isAction;  // 대화창이 열때 상태체크를 하는 변수 
    public bool isWorldMapOpen = false;


    // 게임 종료 여부를 판단할 변수
    public bool isGameOver = false;

    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public GameObject fullMapCamera;


    // 필요한 사운드 이름
    [SerializeField]
    private string openMapSound;


    // Start is called before the first frame update

    private void Start()
    {

        redDragon = GetComponent<Enemy_RedDragon>();
        /** 리스폰 기능을 추가 했었으나, 현재는 사용하지 않음.*/
        //EnemyRespawnPoints_Purple = GameObject.Find("SpawnPoint_PurpleDragon").GetComponentsInChildren<Transform>(); // 스폰포인트그룹의 자식의 위치값을 저장
        //EnemyRespawnPoints_Grey = GameObject.Find("SpawnPoint_GreyDragon").GetComponentsInChildren<Transform>(); // 스폰포인트그룹의 자식의 위치값을 저장
        //EnemyRespawnPoints_Brown = GameObject.Find("SpawnPoint_BrownDragon").GetComponentsInChildren<Transform>(); // 스폰포인트그룹의 자식의 위치값을 저장


        //StartCoroutine(this.CreateEnemy_Purple());
        //StartCoroutine(this.CreateEnemy_Grey());
        //StartCoroutine(this.CreateEnemy_Brown());


    }

    IEnumerator CreateEnemy_Purple()
    {
        //maxEnemy 갯수만큼만 반복하도록 
        yield return new WaitForSeconds(2f); // 2초가 지난 뒤에

        for (int i = 0; i < EnemyRespawnPoints_Purple.Length; i++)
        {
            Instantiate(Enemy_PurpleDragon.instance, EnemyRespawnPoints_Purple[i]); // 적을 복제하는 코드 
            yield return new WaitForSeconds(enemyCreateTime); // enemyCreateTime 초 뒤에 다시 몬스터가 생성
        }

    }

    IEnumerator CreateEnemy_Grey()
    {
        //maxEnemy 갯수만큼만 반복하도록 
        yield return new WaitForSeconds(2f); // 2초가 지난 뒤에

        for (int i = 0; i < EnemyRespawnPoints_Grey.Length; i++)
        {
            Instantiate(Enemy_GrayDragon.instance, EnemyRespawnPoints_Grey[i]); // 적을 복제하는 코드 
            yield return new WaitForSeconds(enemyCreateTime); // enemyCreateTime 초 뒤에 다시 몬스터가 생성
        }

    }

    IEnumerator CreateEnemy_Brown()
    {
        //maxEnemy 갯수만큼만 반복하도록 
        yield return new WaitForSeconds(2f); // 2초가 지난 뒤에

        for (int i = 0; i < EnemyRespawnPoints_Brown.Length; i++)
        {
            Instantiate(Enemy_BrownDragon.instance, EnemyRespawnPoints_Brown[i]); // 적을 복제하는 코드 
            yield return new WaitForSeconds(enemyCreateTime); // enemyCreateTime 초 뒤에 다시 몬스터가 생성
        }

    }


    public void EnemyDead(GameObject enemy)  // 죽었을때 호출 
    {
        StartCoroutine(RespawnEnemy(enemy));
    }


    public IEnumerator RespawnEnemy(GameObject enemy) // 적이 리스폰되는 코드
    {

        yield return new WaitForSeconds(4f);

        GameObject instanceEnemy = Instantiate(enemy, enemy.transform.parent); // 적을 복제하는 코드 


        instanceEnemy.gameObject.SetActive(false);

        Destroy(enemy);

        yield return new WaitForSeconds(7f);

        instanceEnemy.gameObject.SetActive(true);

    }


    public IEnumerator RespawnPlayer()
    {

        yield return new WaitForSeconds(4.5f); // 4.5초뒤에 

        Player.transform.position = PlayerRespawnPoint.position; // 리스폰장소에 플레이어 살아남 

        Player.GetComponent<Player>().anim.SetTrigger("isRespawn"); // 플레이어 애니메이션 Idle 상태로 원상복구



        yield return new WaitForSeconds(3f);   // 3초 뒤에 

        StartCoroutine(fadeEffect.Fade(1, 0));   // 서서히 화면 밝아지도록함 


        if (redDragon.enemyhp > 0)
        {
            redDragon.enabled = true;
        }


        if (Enemy_GrayDragon.instance.enemyhp > 0)
        {
            Enemy_GrayDragon.instance.enabled = true;
        }



        if (Enemy_BrownDragon.instance.enemyhp > 0)
        {
            Enemy_BrownDragon.instance.enabled = true;
        }



        if (Enemy_PurpleDragon.instance.enemyhp > 0)
        {
            Enemy_PurpleDragon.instance.enabled = true;
        }




        deathMessagePanel.gameObject.SetActive(false); // 죽었을때 나오는 메세지 다시 보이지 않도록 함.

        Player.GetComponent<CharacterController>().enabled = true; // 플레이어 캐릭터컨트롤러 활성화

        Player.GetComponent<PlayerMove>().enabled = true; // 플레이어 이동 기능 다시 재생

        Player.GetComponent<Player>().enabled = true; // 플레이어 기능 다시 재생 


        camera3.enabled = true;

        Json.instance.data.hp = Json.instance.data.maxhp;  // 최대 체력의 값을 현재 Hp만큼 회복 시켜줌 

        hpbar.value = Json.instance.data.maxhp;  // 최대 체력의 값을 HP바에 적용

    }



    // Update is called once per frame
    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);

    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;  //talkdata가 null 일 경우에는 return으로 함수 빠져나가기 
        }

        if (isNpc)
        {
            talkText.text = talkData;
        }

        else
        {
            talkText.text = talkData;

        }

        isAction = true;
        talkIndex++; // 그 다음문장을 가져오기 위해서 작성.
    }

    public void ActionEnd(GameObject scanObj)
    {
        talkPanel.SetActive(false);

    }

    private void Update()
    {

        //TryOpenFullmap();

        if (Json.instance.data.hp <= 0)
        {
            deathMessagePanel.gameObject.SetActive(true);
        }

    }

    void TryOpenFullmap()
    {
        if (isWorldMapOpen == false)
        { 
            if (Input.GetKeyDown(KeyCode.Tab))
            {
              OpenFullmap();
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                CloseFullmap();
            }
          
        }
    }
    void OpenFullmap()
    {
        Cursor.visible = true;  // 마우스 커서 보이게 하기
        Cursor.lockState = CursorLockMode.Confined;
        freeLook.m_XAxis.m_MaxSpeed = 0;
        fullMapCamera.SetActive(true);
        AudioManager.instance.PlaySE(openMapSound,1,1);
        isWorldMapOpen = true;
    }

    public void CloseFullmap()
    {
        freeLook.m_XAxis.m_MaxSpeed = 150;
        Cursor.visible = false;  // 마우스 커서 안보이게 하기 (이동할 건 다하고, 보이지만 않게) 
        Cursor.lockState = CursorLockMode.Locked;
        fullMapCamera.SetActive(false);
        isWorldMapOpen = false;
    }


    
}
