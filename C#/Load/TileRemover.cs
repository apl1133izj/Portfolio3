using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRemover : MonoBehaviour
{
    public Tilemap tilemap; // Ÿ�ϸ� ������Ʈ�� ������ ����

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("Player"))
        {
            // �浹 ���� ��ǥ ��������
            Vector3 hitPosition = collision.GetContact(0).point;

            // Ÿ�� ��ǥ ���
            Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

            // �ش� ��ǥ�� Ÿ�� ��������
            TileBase tile = tilemap.GetTile(cellPosition);

            if (tile != null)
            {
                // Ÿ�� ����
                tilemap.SetTile(cellPosition, null);
            }
        }*/
    }
}
