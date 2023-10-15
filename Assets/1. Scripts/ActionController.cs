using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ������ ���� �� ���� 
public class ActionController : MonoBehaviour
{
    Player player;
    public Vector3 boxsize = new Vector3(1f, 1f, 1f);  // �������� �ִ��� Ȯ���ϱ� ���� ������ ũ�� => ����� Ȱ��
    public Item item;

    [SerializeField]
    private float range; // ���� ������ �ִ� �Ÿ�

    [SerializeField] 
    private LayerMask layerMask; // �����ۿ��� �����ϵ��� ���̾��ũ�� ����

    [SerializeField]
    private Text actionText;

    [SerializeField]
    private Inventory theInventory;

    [SerializeField]
    private string openUi; // �÷��̾ �������� �ݴ� �Ҹ� 

    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {

        Collider[] colls = Physics.OverlapBox(transform.position, boxsize * 1.0f, transform.rotation,layerMask);  // �������� ������ ����

        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < colls.Length; i++)  // ���ھ� �������� ������ŭ �ݺ�
            {
                PickupItem(colls[i]); // �������� �ݱ� 
            }
        }

        if (colls.Length == 0) // �ڽ��ȿ� �������� �ƹ��͵� ���ٸ�
        {
            InfoDisAppear();  // ������ ȹ�� �۾� OFF
        }

    }
    private void OnDrawGizmos()  // �������� ������ ���� �׸���
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.up, boxsize);
    }

    private void PickupItem(Collider coll) //������ �ݱ�
    {
        theInventory.AcquireItem(coll.GetComponent<ItemPickUp>().item); // �κ��丮�� ������ �߰�

        AudioManager.instance.PlaySE(openUi, 1, 0.25f); // ������ �ݴ� ����

        DestroyItem(coll); 
    }

    private void DestroyItem(Collider coll)  // ������ ������Ʈ ����
    {
        Destroy(coll.transform.gameObject);
    }

    public void ItemInfoAppear() // ������ ȹ�� ���� On
    {
        actionText.gameObject.SetActive(true);
        actionText.text = "������ ȹ�� ";
    }

    private void InfoDisAppear() // ������ ȹ�� ���� Off
    {
        actionText.gameObject.SetActive(false);
        player.isGetItem = false;
    }

}