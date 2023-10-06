using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float m_rotateScale = 50;
    public void RotateLeft(float rotateScale)
    {
        transform.eulerAngles -= Vector3.up * Time.deltaTime * rotateScale;
    }

    public void RotateRight(float rotateScale)
    {
        transform.eulerAngles += Vector3.up * Time.deltaTime * rotateScale;
    }

    public void MoveUp()
    {
        Camera.main.transform.position += Vector3.up*Time.deltaTime;
    }
    public void MoveDown()
    {
        Camera.main.transform.position -= Vector3.up * Time.deltaTime;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRight(m_rotateScale);
        }
        if (Input.GetKey(KeyCode.D))
        {
            RotateLeft(m_rotateScale);
        }
        if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
    }
}
