using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CreditEditorScript : MonoBehaviour
{
    private TextMeshProUGUI _creditText;
    public CamerController camScript;
    public Rigidbody2D coyoteRb;

    // Start is called before the first frame update
    void Start()
    {
        _creditText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var maxX = camScript.maxValues.x;
        switch (coyoteRb.position.x)
        {
            case var _ when coyoteRb.position.x >= maxX - 1f:
                _creditText.text = "Thank you for playing!";
                break;
            case var _ when coyoteRb.position.x >= maxX / 6 * 5:
                _creditText.text = "Special Thanks to Our Sanity";
                break;
            case var _ when coyoteRb.position.x >= maxX / 6 * 4:
                _creditText.text = "Soren Caron (Code)";
                break;
            case var _ when coyoteRb.position.x >= maxX / 6 * 3:
                _creditText.text = "Kenneth Goh (Code)";
                break;
            case var _ when coyoteRb.position.x >= maxX / 6 * 2:
                _creditText.text = "Daphne Zhang (Art)";
                break;
            case var _ when coyoteRb.position.x >= maxX / 6:
                _creditText.text = "Alice Wang (Code)";
                break;
            case var _ when coyoteRb.position.x >= maxX / 30:
                _creditText.text = "Created by";
                break;
            default:
                _creditText.text = "Credits";
                break;
        }
    }
}