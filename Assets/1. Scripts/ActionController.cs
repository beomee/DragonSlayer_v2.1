using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 아이템 감지 및 습득 
public class ActionController : MonoBehaviour
{
    Player player;
    public Vector3 boxsize = new Vector3(1f, 1f, 1f);  // 아이템이 있는지 확인하기 위한 상자의 크기 => 기즈모 활용
    public Item item;

    [SerializeField]
    private float range; // 습득 가능한 최대 거리

    [SerializeField] 
    private LayerMask layerMask; // 아이템에만 반응하도록 레이어마스크를 설정

    [SerializeField]
    private Text actionText;

    [SerializeField]
    private Inventory theInventory;

    [SerializeField]
    private string openUi; // 플레이어가 아이템을 줍는 소리 

    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {

        Collider[] colls = Physics.OverlapBox(transform.position, boxsize * 1.0f, transform.rotation,layerMask);  // 아이템을 감지할 상자

        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < colls.Length; i++)  // 상자안 아이템의 개수만큼 반복
            {
                PickupItem(colls[i]); // 아이템을 줍기 
            }
        }

        if (colls.Length == 0) // 박스안에 아이템이 아무것도 없다면
        {
            InfoDisAppear();  // 아이템 획득 글씨 OFF
        }

    }
    private void OnDrawGizmos()  // 아이템을 감지할 상자 그리기
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.up, boxsize);
    }

    private void PickupItem(Collider coll) //아이템 줍기
    {
        theInventory.AcquireItem(coll.GetComponent<ItemPickUp>().item); // 인벤토리에 아이템 추가

        AudioManager.instance.PlaySE(openUi, 1, 0.25f); // 아이템 줍는 사운드

        DestroyItem(coll); 
    }

    private void DestroyItem(Collider coll)  // 아이템 오브젝트 삭제
    {
        Destroy(coll.transform.gameObject);
    }

    public void ItemInfoAppear() // 아이템 획득 문구 On
    {
        actionText.gameObject.SetActive(true);
        actionText.text = "아이템 획득 ";
    }

    private void InfoDisAppear() // 아이템 획득 문구 Off
    {
        actionText.gameObject.SetActive(false);
        player.isGetItem = false;
    }

}