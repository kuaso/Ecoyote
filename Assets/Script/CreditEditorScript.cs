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
           
            case var _ when coyoteRb.position.x >= maxX / 8 * 7.8:
                _creditText.text = "Thank you for playing!";
                break;
            case var _ when coyoteRb.position.x >= maxX / 8 * 7.1:
                _creditText.text = "Bandits - Pixel Art from Sven Thole (Asset Store)\nShooting Enemy image from MintFresh (OpenGameArt)\nMeat image from cadmium_red (Freepik)\n";
                _creditText.enableAutoSizing = true;
                break;
            case var _ when coyoteRb.position.x >= maxX / 8 * 6.3:
                _creditText.text =
                    "Poison Apple image from PngAAA\nTrash images from Saphatthachat (Freepik)\nFire image from A455dc (Pixel Art Maker)\nTree Trunk image from Mroker (Deviant Art)";
                _creditText.enableAutoSizing = true;
                break;
            case var _ when coyoteRb.position.x >= maxX / 8 * 6:
                _creditText.text = "Assets credits";
                break;
            case var _ when coyoteRb.position.x >= maxX / 8 * 5.3:
                _creditText.text = "David Eirew (Narration)";
                break;
            case var _ when coyoteRb.position.x >= maxX / 8 * 5:
                _creditText.text = "Special Thanks";
                break;
            case var _ when coyoteRb.position.x >= maxX / 8 * 4:
                _creditText.text = "Soren Caron (Code)";
                break;
            case var _ when coyoteRb.position.x >= maxX / 8 * 3:
                _creditText.text = "Kenneth Goh (Code)";
                break;
            case var _ when coyoteRb.position.x >= maxX / 8 * 2:
                _creditText.text = "Daphne Zhang (Art)";
                break;
            case var _ when coyoteRb.position.x >= maxX / 8:
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