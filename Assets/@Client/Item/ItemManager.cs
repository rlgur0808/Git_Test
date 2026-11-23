using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab = null;
    // 아이템이 생성될 발판들
    [SerializeField]
    private Collider[] platforms = null;
    [SerializeField]
    private int maxItemCount = 3;
    [SerializeField]
    private float spawnInterval = 5f;
    // 벽에 붙어서 생기지 않도록 발판 가장자리에서 띄울 거리
    [SerializeField]
    private float edgeMargin = 1.5f;
    // 발판 표면에서 띄울 높이
    [SerializeField]
    private float heightOffset = 0.5f;

    private List<Item> itemList = null;

    private void Awake()
    {
        itemList = new List<Item>();
    }

    public void Init()
    {
        StartCoroutine(CoSpawnItems());
    }

    private IEnumerator CoSpawnItems()
    {
        while (true)
        {
            if (itemList.Count < maxItemCount)
            {
                SpawnItem();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnItem()
    {
        if (itemPrefab == null || platforms == null || platforms.Length == 0)
        {
            return;
        }

        if (TryGetRandomPointOnPlatform(out Vector3 spawnPosition) == false)
        {
            return;
        }

        GameObject go = Instantiate(itemPrefab, spawnPosition, Quaternion.identity, this.transform);

        Item item = go.GetComponent<Item>();
        if (item != null)
        {
            item.Init(OnItemPickedUp);
            itemList.Add(item);
        }
    }

    private bool TryGetRandomPointOnPlatform(out Vector3 point)
    {
        // 몇 번 시도해서 발판 위에 맞는 위치를 찾음
        for (int i = 0; i < 10; i++)
        {
            Collider platform = platforms[Random.Range(0, platforms.Length)];
            Bounds bounds = platform.bounds;

            float x = Random.Range(bounds.min.x + edgeMargin, bounds.max.x - edgeMargin);
            float z = Random.Range(bounds.min.z + edgeMargin, bounds.max.z - edgeMargin);
            Vector3 rayOrigin = new Vector3(x, bounds.max.y + 10f, z);

            // 위에서 아래로 쏴서 실제 발판 표면 위치를 구함
            if (platform.Raycast(new Ray(rayOrigin, Vector3.down), out RaycastHit hit, 100f))
            {
                point = hit.point + Vector3.up * heightOffset;
                return true;
            }
        }

        point = Vector3.zero;
        return false;
    }

    private void OnItemPickedUp(Item item)
    {
        itemList.Remove(item);
    }
}
