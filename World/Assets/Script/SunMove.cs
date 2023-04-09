using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject sunLight;
    [SerializeField]
    private GameObject character;

    private float _sunDistance;
    void Start()
    {
        _sunDistance=(this.transform.position - character.transform.position).magnitude;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = character.transform.position+
            Quaternion.Euler(sunLight.transform.eulerAngles)* character.transform.up*_sunDistance ;
    }
}
