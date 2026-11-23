using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private float healAmount = 20f;
    [SerializeField]
    private float rotateSpeed = 90f;

    private Action<Item> onPickedUp = null;

    public void Init(Action<Item> onPickedUp)
    {
        this.onPickedUp = onPickedUp;
    }

    private void Update()
    {
        // 눈에 띄도록 제자리에서 회전
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어에 닿았을 때만 회복
        Player player = other.GetComponentInParent<Player>();
        if (player == null)
        {
            return;
        }

        player.Heal(healAmount);
        onPickedUp?.Invoke(this);
        Destroy(gameObject);
    }
}
