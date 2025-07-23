using RosMessageTypes.Geometry;
using Unity.Robotics.ROSTCPConnector;
//using Unity.Robotics.ROSTCPConnector.MessageTypes.Geometry;
using Vec_ctrlMsg = RosMessageTypes.UnityRoboticsDemo.Vec_ctrlMsg;
using UnityEngine;

public class HuskyController : MonoBehaviour
{
    public ArticulationBody front_leftWheel;
    public ArticulationBody front_rightWheel;
    public ArticulationBody back_leftWheel;
    public ArticulationBody back_rightWheel;
    public float wheelRadius = 0.165f; // Husky'nin gerçek teker çapı (yaklaşık)
    public float wheelSeparation = 0.55f * 5;

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<Vec_ctrlMsg>("/vec_ctrl", OnCmdVelReceived);
    }
    // void Update()
    // {
    //     Debug.Log("Deneme"); // Bu çıkıyor mu?
    //     if (leftWheel != null && rightWheel != null)
    //     {
    //         SetWheelVelocity(leftWheel, 10f); // 100 değil, 10 deneyelim
    //         SetWheelVelocity(rightWheel, 10f);
    //     }
    // }

    void OnCmdVelReceived(Vec_ctrlMsg msg)
    {
        Debug.LogWarning("Linear: " + msg.linear + ", Angular: " + msg.angular + ", Stop: " + msg.stop);

        float linear = (float)msg.linear;
        float angular = (float)msg.angular;
        if((bool)msg.stop == true)
        {
            SetWheelVelocity(front_leftWheel, 0f);
            SetWheelVelocity(front_rightWheel, 0f);
            SetWheelVelocity(back_leftWheel, 0f);
            SetWheelVelocity(back_rightWheel, 0f);
            return;
        }
        float leftVelocity = (linear - angular * wheelSeparation / 2f) / wheelRadius;
        float rightVelocity = (linear + angular * wheelSeparation / 2f) / wheelRadius;
        SetWheelVelocity(front_leftWheel, leftVelocity);
        SetWheelVelocity(front_rightWheel, rightVelocity);
        SetWheelVelocity(back_leftWheel, leftVelocity);
        SetWheelVelocity(back_rightWheel, rightVelocity);
    }

    void SetWheelVelocity(ArticulationBody wheel, float velocity)
    {
        var drive = wheel.xDrive;
        drive.targetVelocity = velocity * Mathf.Rad2Deg; // Unity derece kullanıyor
        wheel.xDrive = drive;
    }
}
