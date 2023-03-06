using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float CharacterSpeed = 300;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float factor = Time.deltaTime*CharacterSpeed;
        float dx = factor*Input.GetAxis("Horizontal");
        float dy = factor*Input.GetAxis("Vertical");
        Vector3 moveDirection =
            dy * this.transform.forward +
            dx * this.transform.right;
        characterController.SimpleMove(moveDirection);
    }
}
