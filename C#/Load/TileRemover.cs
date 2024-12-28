using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRemover : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵 오브젝트를 참조할 변수

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("Player"))
        {
            // 충돌 지점 좌표 가져오기
            Vector3 hitPosition = collision.GetContact(0).point;

            // 타일 좌표 계산
            Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

            // 해당 좌표의 타일 가져오기
            TileBase tile = tilemap.GetTile(cellPosition);

            if (tile != null)
            {
                // 타일 삭제
                tilemap.SetTile(cellPosition, null);
            }
        }*/
    }
}
