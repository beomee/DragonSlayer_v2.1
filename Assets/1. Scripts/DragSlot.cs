using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    public static DragSlot instance; // Slot 클래스 내 드래그관련 함수를 쉽게 쓰기위해 싱글톤 사용

    public Slot dragSlot; // Slot안에 값을 드로그중에 복사하기 위한 변수  

    [SerializeField]
    private Image imageItem;


    private void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _itemImage)
    {
        imageItem.sprite = _itemImage.sprite;
        SetColor(1);
    }


    // 흰색 배경을 드래그 할 때만 보여주기
    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha;
        imageItem.color = color;
    }


}
