using UnityEngine;

public class SmallBlockFading : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject teleporter, noCubeCollidorBlock;
    private float smallTimer, smallerScale;
    private Vector3 originalScale;
    void Start()
    {
        originalScale = transform.localScale;
        smallerScale = 1;
        noCubeCollidorBlock = GameObject.FindGameObjectWithTag("NoCubeCollidor");
        Physics.IgnoreCollision(GetComponent<BoxCollider>(), noCubeCollidorBlock.GetComponent<BoxCollider>(), true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        smallTimer++;
        if (smallTimer > 200) {
            smallerScale -= 0.01f;
            transform.localScale = originalScale * smallerScale;
        }
        if (smallerScale <= 0) { 
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BottomFloor")) {
            Destroy(gameObject);
        }
    }
}
