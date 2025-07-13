//Code that works for dropdown is below this code, this code is meant to work with buttons

using UnityEngine;
using UnityEngine.UI;
using MidiPlayerTK;
using System.Collections.Generic;

public class MidiUIManager : MonoBehaviour
{
    public MidiFilePlayer midiFilePlayer;
    public Button playButton43;
    public Button playButton3;
    public Button playButton15;

    private List<string> midiNames;

    void Start()
    {
        // Get MIDI names from the global list
        midiNames = MidiPlayerGlobal.MPTK_ListMidi.ConvertAll(m => m.Label);

        // Safety check
        if (midiNames.Count <= 43)
        {
            Debug.LogError("Not enough MIDI files loaded. Please check your MidiDB.");
            return;
        }

        // Hook up the buttons
        playButton43.onClick.AddListener(() => PlayMidiAtIndex(43));
        playButton3.onClick.AddListener(() => PlayMidiAtIndex(3));
        playButton15.onClick.AddListener(() => PlayMidiAtIndex(15));
    }

    void PlayMidiAtIndex(int index)
    {
        if (index >= 0 && index < midiNames.Count)
        {
            midiFilePlayer.MPTK_MidiName = midiNames[index];
            midiFilePlayer.MPTK_RePlay();
            Debug.Log("Playing MIDI: " + midiNames[index]);
        }
        else
        {
            Debug.LogWarning("Invalid MIDI index: " + index);
        }
    }
}


//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using MidiPlayerTK;
//using System.Collections.Generic;

//public class MidiUIManager : MonoBehaviour
//{
//    public MidiFilePlayer midiFilePlayer;
//    public TMP_Dropdown midiDropdown;
//    public Button playButton;

//    void Start()
//    {
//        // Get string names from the list of MPTKListItem
//        List<string> midiNames = MidiPlayerGlobal.MPTK_ListMidi.ConvertAll(m => m.Label);

//        // Populate dropdown
//        midiDropdown.ClearOptions();
//        midiDropdown.AddOptions(midiNames);

//        // Set default MIDI
//        if (midiNames.Count > 0)
//        {
//            midiFilePlayer.MPTK_MidiName = midiNames[43];
//        }

//        // Register listeners
//        midiDropdown.onValueChanged.AddListener(OnMidiSelected);
//        playButton.onClick.AddListener(PlaySelectedMidi);
//    }

//    void OnMidiSelected(int index)
//    {
//        string selectedMidi = MidiPlayerGlobal.MPTK_ListMidi[index].Label;
//        midiFilePlayer.MPTK_MidiName = selectedMidi;
//    }

//    void PlaySelectedMidi()
//    {
//        midiFilePlayer.MPTK_RePlay();
//    }
//}


