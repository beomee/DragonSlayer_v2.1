using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject 
{

    public string itemName; // �������� �̸�
    public ItemType itemType; // �������� ����
    public int itemPrice;    // �������� ����
    public Sprite itemImage; // �������� �̹���
    public GameObject itemPrefab; // �������� ������
    public int ItemNumber; // �� �������� ������ȣ

    [TextArea] // ������ ����� ���� ���
    public string itemInfo;  // �������� ���� 
    public enum ItemType
    {
        Equipment, // ���
        Used, // �Ҹ�ǰ 
        Ingredint, // ���
        ETC, // ���
    }
}


