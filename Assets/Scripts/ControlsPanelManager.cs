using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ControlsPanelManager : MonoBehaviour
{
    string[] saveEntries = new string[]{"DOWNKEY", "LEFTKEY", "RIGHTKEY", "JUMPKEY", "HITKEY", "DASHKEY", "PAUSEKEY", "PLAYERSKEY"};

    private string DownDefault = "S";
    private string LeftDefault = "A";
    private string RightDefault = "D";
    private string JumpDefault = "Space";
    private string HitDefault = "Mouse0";
    private string DashDefault = "Mouse1";
    private string PauseDefault = "Escape";
    private string PlayersDefault = "Tab";

    public TMP_Text down;
    public TMP_Text left;
    public TMP_Text right;
    public TMP_Text jump;
    public TMP_Text hit;
    public TMP_Text dash;
    public TMP_Text pause;
    public TMP_Text players;

    public Saves sv;


    List<KeyCode> allowedKeys = new List<KeyCode>
    {
        KeyCode.A,
        KeyCode.B,
        KeyCode.C,
        KeyCode.D,
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.UpArrow,
        KeyCode.Space,
        KeyCode.LeftShift,
        KeyCode.RightShift,
        KeyCode.Escape, 
        KeyCode.Mouse0,
        KeyCode.Mouse1,
        KeyCode.Mouse2,
        KeyCode.Tab
    };

    bool listenForInput = false;

    int currentEntryListenId;

    void Awake()
    {
        sv = FindObjectOfType<Saves>();
    }
    void Update()
    {
        if (listenForInput)
        {
            if (Input.anyKeyDown)
            {
                bool wasSelected = false;
                foreach(KeyCode k in allowedKeys)
                {
                    if (Input.GetKeyDown(k))
                    {
                        wasSelected = true;
                        Debug.Log("key pressed "+ k);
                        sv.saveKeyBind(saveEntries[currentEntryListenId], k.ToString());
                        listenForInput = false;
                    }
                }
                if (!wasSelected)
                {
                    sv.saveKeyBind(saveEntries[currentEntryListenId], "-");
                    listenForInput = false;
                }
                loadSaves();
            }
        }
    }
    public void onButtonPressed(int id)
    {
        currentEntryListenId = id;
        listenForInput = true;
        showButtonListen(id);
    }
    public void showButtonListen(int id)
    {
        switch (id)
        {
            case 0:
            down.text = ">> <<";
            break;
            case 1:
            left.text = ">> <<";
            break;
            case 2:
            right.text = ">> <<";
            break;
            case 3:
            jump.text = ">> <<";
            break;
            case 4:
            hit.text = ">> <<";
            break;
            case 5:
            dash.text = ">> <<";
            break;
            case 6:
            pause.text = ">> <<";
            break;
            case 7:
            players.text = ">> <<";
            break;

        }
    }
    public void loadSaves()
    {
        if (sv.getKeyBind("DOWNKEY") == "-")
        {
            sv.saveKeyBind("DOWNKEY", DownDefault);
        }
        down.text = sv.getKeyBind("DOWNKEY");

        if (sv.getKeyBind("LEFTKEY") == "-")
        {
            sv.saveKeyBind("LEFTKEY", LeftDefault);
        }
        left.text = sv.getKeyBind("LEFTKEY");

        if (sv.getKeyBind("RIGHTKEY") == "-")
        {
            sv.saveKeyBind("RIGHTKEY", RightDefault);
        }
        right.text = sv.getKeyBind("RIGHTKEY");

        if (sv.getKeyBind("JUMPKEY") == "-")
        {
            sv.saveKeyBind("JUMPKEY", JumpDefault);
        }
        jump.text = sv.getKeyBind("JUMPKEY");

        if (sv.getKeyBind("HITKEY") == "-")
        {
            sv.saveKeyBind("HITKEY", HitDefault);
        }
        hit.text = sv.getKeyBind("HITKEY");

        if (sv.getKeyBind("DASHKEY") == "-")
        {
            sv.saveKeyBind("DASHKEY", DashDefault);
        }
        dash.text = sv.getKeyBind("DASHKEY");

        if (sv.getKeyBind("PAUSEKEY") == "-")
        {
            sv.saveKeyBind("PAUSEKEY", PauseDefault);
        }
        pause.text = sv.getKeyBind("PAUSEKEY");

        if (sv.getKeyBind("PLAYERSKEY") == "-")
        {
            sv.saveKeyBind("PLAYERSKEY", PlayersDefault);
        }
        players.text = sv.getKeyBind("PLAYERSKEY");
    }
}
