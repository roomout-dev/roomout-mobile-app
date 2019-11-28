using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureHandler : MonoBehaviour
{
    /// <summary>
    /// Snackbar text displayed
    /// </summary>
    public Text m_text;

    /// <summary>
    /// The Prefab of the Treasure Chest, instantiated by the LocalPlayer
    /// </summary>
    public GameObject m_TreasureChestPrefab;

    /// <summary>
    /// The Prefab of the Golden Key, instantiated by the LocalPlayer
    /// </summary>
    public GameObject m_GoldenKey;

    /// <summary>
    /// Is the key found ?
    /// </summary>
    public bool keyFound { get; private set; }

    /// <summary>
    /// Flag for animation of opening the chest
    /// </summary>
    public bool playOpen { get; private set; }

    /// <summary>
    /// Flag for animation of closing the chest
    /// </summary>
    public bool playClose { get; private set; }

    /// <summary>
    /// The animator component of the prefab
    /// </summary>
    public Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        keyFound = false;
        playOpen = false;
        playClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Test if the component can play an animation
        if (!IsAnimatorPlaying())
        {
            if (playOpen)
            {
                m_animator.Play("TreasureChest_OPEN");
                playOpen = false;
            }
            if (playClose)
            {
                m_animator.Play("TreasureChest_CLOSE");
                playClose = false;
            }
        }
    }

    /// <summary>
    /// Set the inital prefab
    /// </summary>
    /// <param name="prefab"></param>
    public void SetPrefab(GameObject prefab)
    {
        m_TreasureChestPrefab = prefab;
        m_animator = m_TreasureChestPrefab.GetComponent<Animator>();
    }

    /// <summary>
    /// Set the prefab of the Golden Key
    /// </summary>
    /// <param name="goldenKey"></param>
    public void SetGoldenKey(GameObject goldenKey)
    {
        m_GoldenKey = goldenKey;
    }

    /// <summary>
    /// Write text in the Snackbar text box
    /// </summary>
    /// <param name="msg"></param>
    public void WriteText(string msg)
    {
        m_text.text = msg;
    }

    /// <summary>
    /// Action for selecting the chest
    /// </summary>
    public void SelectChest()
    {
        if (!keyFound)
        {
            WriteText("Hum, le coffre ne s'ouvre pas...");
        }
        else
        {
            WriteText("Un coffre au trésor !");
            playOpen = true;
            GameObject gameState = GameObject.Find("GameState");
            gameState.GetComponent<GameStateController>().SetGameState(1);
        }
    }

    /// <summary>
    /// Action for deselecting the chest
    /// </summary>
    public void DeselectChest()
    {
        // No need to flag if the key is not found yet
        if (!keyFound)
        {
            return;
        }

        playClose = true;
    }

    /// <summary>
    /// Action for selecting the Golden Key
    /// </summary>
    public void SelectKey()
    {
        WriteText("Vous trouvez une clef");
        keyFound = true;
    }

    /// <summary>
    /// Check if an animation is currently played
    /// </summary>
    /// <returns></returns>
    private bool IsAnimatorPlaying()
    {
        return m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }
}
