using UnityEngine;

public class SmallBlockSpliting : MonoBehaviour
{
    public GameObject bulletPrefab;     // �����ӵ� prefab
    public Transform[] shootPoints;        // ����λ�ã�һ��������ͷǰ��С�����壩
    public float bulletForce, maxShootInterval;   // �������
    private float shootCounter;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && shootCounter==0) // ������
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
            rb.AddForce(shootPoints[i].forward * bulletForce);  // �� shootPoint ǰ��������
        }

    }
}
