using UnityEngine;
using UnityEngine.UI;

public class PCHillDrive : PlayerController
{
    [Header("Exclusive Configuration")]
    [SerializeField] private float rotationSpeed;

    [Header("Exclusive Dependencies")]
    [SerializeField] private WheelJoint2D backWheel;
    [SerializeField] private WheelJoint2D frontWheel;
    [SerializeField] private AudioSource audioSource;

    [Header("Camera")]
    [SerializeField] private float aheadAmount;
    [SerializeField] private float aheadSpeed;

    [Header("UI")]
    [SerializeField] private Image fuelUI;

    private void Start() => AudioManager.Instance.PlayBgHillDrive();

    void FixedUpdate()
    {
        if (GCHillDrive.Instance.fuel > 0)
        {
            if (horizontalMove == 0f)
            {
                backWheel.useMotor = false;
                frontWheel.useMotor = false;

                audioSource.volume = 0.2f;
            }
            else
            {
                backWheel.useMotor = true;
                frontWheel.useMotor = true;

                JointMotor2D motor = new JointMotor2D { motorSpeed = -horizontalMove * speed, maxMotorTorque = 10000 };
                backWheel.motor = motor;
                frontWheel.motor = motor;

                GCHillDrive.Instance.fuel -= GCHillDrive.Instance.fuelConsumption * Mathf.Abs(horizontalMove) * Time.fixedDeltaTime;
                GCHillDrive.Instance.fuelUI.fillAmount = GCHillDrive.Instance.fuel;
                audioSource.volume = 0.7f;

                rigidBody.AddTorque(-horizontalMove * rotationSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;

            GCHillDrive.Instance.NoFuelGameOver();
            audioSource.volume = 0.2f;
        }

        /*if (rigidBody.velocity.y < 0) animator.SetBool("IsFalling", true);
        else animator.SetBool("IsFalling", false);*/

    }

    public void DoInstaGameOver() => GCHillDrive.Instance.OnGameOver();
}
