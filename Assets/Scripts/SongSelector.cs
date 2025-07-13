using UnityEngine;
using MidiPlayerTK;
using UnityEngine.UI;

public class SongSelector : MonoBehaviour
{
    public MidiFilePlayer midiPlayer;
    public Slider progressBar;

    // void Start()
    // {
    //     if (midiPlayer != null)
    //     {
    //         midiPlayer.MPTK_Stop(); // Stop any ongoing playback
    //         midiPlayer.MPTK_MidiName = ""; // Clear song
    //     }
    // }


    public void SelectSong(string songName)
    {
        if (midiPlayer != null)
        {
            Debug.Log("Song selected: " + songName);
            midiPlayer.MPTK_MidiName = songName;
            midiPlayer.MPTK_Play();
            // Notify the visualizer to reload and restart
            MidiNoteBarVisualizer visualizer = FindObjectOfType<MidiNoteBarVisualizer>();
            if (visualizer != null)
            {
                visualizer.ReloadAndRestart();
                visualizer.Play();
            }
        }
    }

    void Update()
    {
        if (midiPlayer != null && midiPlayer.MPTK_IsPlaying && progressBar != null)
        {
            float current = (float)midiPlayer.MPTK_Position; // ms
            float total = (float)midiPlayer.MPTK_Duration.TotalMilliseconds;   // ms
            progressBar.value = total > 0 ? current / total : 0;
        }
        else if (progressBar != null)
        {
            progressBar.value = 0;
        }
    }
}
