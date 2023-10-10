using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayEnemyAttackCollision_Shouting : MonoBehaviour
{

    public Enemy_GrayDragon grayDragon;
    public Collider collider_Gray;
    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }


    private void OnTriggerEnter(Collider other)
    {

        // 플레이어가 타격하는게 "Enemy" 일 경우, 에너미 클래스의 데미지함수를 호출 
        if (other.CompareTag("Player"))
        {
                other.GetComponent<Player>().Damaged(grayDragon.enemystr);  // 플레이어에게 데미지를 입히는 함수.
            collider_Gray.enabled = false;
        }

    }



    private IEnumerator AutoDisable()
    {
        // 0.2초 후에 오브젝트가 사라지도록 한다.
        yield return new WaitForSeconds(0.2f);
        collider_Gray.enabled = true;
        gameObject.SetActive(false);

    }


}
