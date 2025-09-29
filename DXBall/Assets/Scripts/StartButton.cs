using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        // Get the Button component (TMP button uses the same underlying Button)
        button = GetComponent<Button>();

        // Add listener for click
        button.onClick.AddListener(LoadLevel1);
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene("Level1"); 
    }
}
