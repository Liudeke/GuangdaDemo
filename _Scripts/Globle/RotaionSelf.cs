using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaionSelf : MonoBehaviour
{

    //目标
    public Transform target;

    //距离
    public float distance = 5f;

    //右键旋转控制部分
    //旋转速度
    private float speedX = 240;
    private float speedY = 120;
    //Y轴旋转的角度限制
    public float minLimitY = 5;
    public float maxLimitY = 45;
    //旋转角度
    public float mX = 0.0f;
    public float mY = 30f;

    //鼠标缩放控制部分
    //鼠标缩放距离最值
    public float maxDistance = 10;
    public float minDistance = 1.5f;
    //鼠标缩放速度
    public float zoomSpeed = 2f;

    //差值控制部分
    //是否启用差值计算
    public bool isNeedDamping = false;
    //差值速度
    public float dampingSpeed = 10f;

    private Quaternion mRotation;
    private Vector3 mPosition;


    public float moveSpeed = 1;
    public float rx = 0;
    public float ry = 30;
    //  public float iniDis = 10f;
    void Start()
    {
        //初始化旋转角度
        transform.rotation = Quaternion.Euler(mY, mX, 0);
        mPosition = Quaternion.Euler(mY, mX, 0) * new Vector3(0, 0, -distance) + target.position;
    }

    void LateUpdate()
    {
        //第一步（功能一）：右键控制相机旋转部分
        if (target != null && Input.GetMouseButton(1))
        {
            //1.获取鼠标输入
            mX += Input.GetAxis("Mouse X") * speedX * 0.02f;
            mY -= Input.GetAxis("Mouse Y") * speedY * 0.02f;

            //1.1Y轴角度范围限制
            //mY = ClampAngle(mY,minLimitY,maxLimitY);
            mY = Mathf.Clamp(mY, minLimitY, maxLimitY);

            //1.2计算旋转，转化成欧拉角
            mRotation = Quaternion.Euler(mY, mX, 0);

            transform.rotation = mRotation;
            mPosition = mRotation * new Vector3(0, 0, -distance) + target.position;

            //3.对目标物体的状态限定。不是所有的状态都可以旋转的，比如轻功时、战斗时就不可以旋转（去除物体本身旋转的可能）
            target.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0));  //注意，这里mx中的X是针对鼠标的，实际上，在X平面上是绕着Y轴旋转
        }
        if (Input.GetMouseButton(0))
        {
            mRotation = Quaternion.Euler(mY, mX, 0);
            transform.rotation = mRotation;


            mPosition = mRotation * new Vector3(0, 0, -distance) + target.position;

            target.Translate(target.forward * moveSpeed * Time.deltaTime * -Input.GetAxis("Mouse Y"), Space.World);
            target.Translate(target.right * moveSpeed * Time.deltaTime * -Input.GetAxis("Mouse X"), Space.World);
        }


        //第二步（功能二）：鼠标滚轮缩放部分控制
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {

            mRotation = Quaternion.Euler(mY, mX, 0);
            transform.rotation = mRotation;

            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            mPosition = mRotation * new Vector3(0, 0, -distance) + target.position;

        }


        //第三步（功能三）：计算相机位置并进行设定
        //这里出了个小问题，计算位置的时候，Y轴还是先别动了
        if (isNeedDamping)
        {
            transform.position = Vector3.Lerp(transform.position, mPosition, Time.deltaTime * dampingSpeed);
        }
        else
        {
            transform.position = mPosition;

        }

    }

}
