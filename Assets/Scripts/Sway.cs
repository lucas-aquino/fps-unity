using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float rotationSizeX = 1.45f;
    public float rotationSizeY = 1.45f;
    


    private Quaternion originLocalRotation;

    private void Start()
    {
        originLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        updateSway();
    }

    private void updateSway()
    {
        float t_xLookInput = Input.GetAxis("Mouse X");
        float t_yLookInput = Input.GetAxis("Mouse Y");

        //Calcular la rotacion del arma
        Quaternion t_xAngleAdjustment = Quaternion.AngleAxis(-t_xLookInput * rotationSizeX, Vector3.up);
        Quaternion t_yAngleAdjustment = Quaternion.AngleAxis(t_yLookInput * rotationSizeY, Vector3.right);
        Quaternion t_targetRotation = originLocalRotation * t_xAngleAdjustment * t_yAngleAdjustment;

        //Rotar a la posicion 
        transform.localRotation = Quaternion.Lerp(transform.localRotation, t_targetRotation, Time.deltaTime * 10);
    }
}
