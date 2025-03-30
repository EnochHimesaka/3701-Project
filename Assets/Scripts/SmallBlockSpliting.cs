using UnityEngine;

public class SmallBlockSpliting : MonoBehaviour
{
    public GameObject bulletPrefab;     // 拖入子弹 prefab
    public Transform[] shootPoints;        // 发射位置（一般是摄像头前的小空物体）
    public float bulletForce, maxShootInterval;   // 射出力道
    private float shootCounter;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && shootCounter==0) // 鼠标左键
        {
            Shoot();
            shootCounter = maxShootInterval;
        }
        shootCounter--;
        shootCounter = Mathf.Clamp(shootCounter, 0, maxShootInterval);
    }

    void Shoot()
    {
        for (int i = 0; i < shootPoints.Length; ++i) {
            GameObject bullet = Instantiate(bulletPrefab, shootPoints[i].position, shootPoints[i].rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(shootPoints[i].forward * bulletForce);  // 从 shootPoint 前方向外射
        }

    }
}
