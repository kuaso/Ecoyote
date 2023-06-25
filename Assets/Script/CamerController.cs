using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    [SerializeField] Transform coyote;
    [SerializeField] Vector3 minValues, maxValues;

    // Update is called once per frame
    void Update()
    {
        float targetX = coyote.position.x + 1.5f;
        if (targetX < minValues.x) {
            targetX = minValues.x;
        }
        else if (targetX > maxValues.x) {
            targetX = maxValues.x;
        }

        float targetY = coyote.position.y + 1.3f;
        if (targetY < minValues.y)
        {
            targetY = minValues.y;
        }
        else if (targetY > maxValues.y)
        {
            targetY = maxValues.y;
        }
        transform.position = new Vector3(targetX, targetY, transform.position.z);

    }
}
