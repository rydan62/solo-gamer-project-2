using Unity.Cinemachine;
using UnityEngine;

public class traps : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "trap(insta)")
        {
            playerHealth = maxplayerHealth * 5;
        }
    }
}
