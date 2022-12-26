using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private CharacterController CC;
    private Camera Cam;
    private float Speed;
    private float XRotation;
   
    public bool CanMove;//Множно ли двигаться(так же зависит от Time.timeScale) (!НЕ изменять вручную!)
    public float StepSpeed;//Скорость Шага
    public float RunSpeed;//Скорость бега
    public float RotateSpeed;//Скрорость вращения(Чувствительность Мыши)
    public float MoveGravity;//Величина гравитации, для возможных случаев падения(вводить со знаком -)

    // Start is called before the first frame update
    void Awake()
    {
        CC = gameObject.GetComponent<CharacterController>();
        Cam = GetComponentInChildren<Camera>();
        Time.timeScale = 1;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove == true)
        {
            CC.Move(transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), MoveGravity, Input.GetAxis("Vertical"))) * Speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.LeftShift)) Speed = RunSpeed;
            else Speed = StepSpeed;

            CC.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime, Space.World);
            XRotation = Mathf.Clamp(XRotation, -90, 90);
            XRotation -= Input.GetAxis("Mouse Y") * RotateSpeed * Time.deltaTime;
            Cam.transform.localRotation = Quaternion.Euler(XRotation, 0, 0);

            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
