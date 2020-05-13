using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// A ship which can be moved at any direction and shoot.
///</summary>
public class Ship : MonoBehaviour
{
    // Physics support.
    float colliderRadius = 0;
    Rigidbody2D rb = null;

    // Thrust support.
    const float thrustForceMultiplier = 5;
    float thrustInput = 0;
    Vector2 thrustDirection = new Vector2(1, 0);

    // Rotation support.
    const float RotateDegreesPerSecond = 50;
    float rotationInput = 0;
    float shipOrientation = 0;

    ///<summary>
    /// Start is called before the first frame update.
    ///</summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderRadius = GetComponent<CircleCollider2D>().radius;
    }

    ///<summary>
    /// Update is called once per frame.
    ///</summary>
    void Update()
    {
        // Calculate rotation amount and apply rotation.
        rotationInput = Input.GetAxis("Rotate");

        if (rotationInput != 0)
        {
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;

            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }

            transform.Rotate(Vector3.forward, rotationAmount);

            // Applying thrust to new directions.
            shipOrientation = Mathf.Deg2Rad * transform.eulerAngles.z;

            thrustDirection.y = (float) Mathf.Sin(shipOrientation);
            thrustDirection.x = (float) Mathf.Cos(shipOrientation);
        }
    }

    ///<summary>
    /// FixedUpdate occurs at a measured time step that typically does not coincide with Update.
    ///</summary>
    void FixedUpdate()
    {
        thrustInput = Input.GetAxis("Thrust");

        if (thrustInput != 0)
        {
            rb.AddForce(thrustForceMultiplier * thrustDirection, ForceMode2D.Force);
        }
    }

    ///<summary>
    /// Controls what happens after the ship gets away from the screen.
    ///</summary>
    void OnBecameInvisible()
    {
        // Wrapping the world so ship is respawned on the opposite side of exit.
        Vector2 shipPosition = transform.position;

        if (shipPosition.x > ScreenUtils.ScreenRight)
        {
            shipPosition.x *= -1;
        }
        else if (shipPosition.x < ScreenUtils.ScreenLeft)
        {
            shipPosition.x *= -1;
        }

        if (shipPosition.y > ScreenUtils.ScreenTop)
        {
            shipPosition.y *= -1;
        }
        else if (shipPosition.y < ScreenUtils.ScreenBottom)
        {
            shipPosition.y *= -1;
        }

        transform.position = shipPosition;
    }
}
