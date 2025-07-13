using MidiPlayerTK;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AnchorSpawner : MonoBehaviour
{
    public GameObject anchorPrefab;
    public GameObject controllerIndicatorPrefab;
    public Material planeMaterial; // Material for generated plane
    public GameObject songMenuCanvas;
    public event System.Action<Transform> OnPlaneCreated;

    public Vector3 TopEdgeStart { get; private set; }
    public Vector3 TopEdgeEnd { get; private set; }

    private List<GameObject> anchors = new List<GameObject>();
    private bool planeCreated = false;
    private GameObject controllerIndicator;

    void Start()
    {
        // Hide the canvas at the start
        if (songMenuCanvas != null)
        {
            songMenuCanvas.SetActive(false);
        }
        
        // Start coroutine to attach indicator when controller is ready
        StartCoroutine(AttachIndicatorWhenReady());

       #if UNITY_EDITOR || UNITY_STANDALONE
       // Automatically create 4 anchors in a rectangular formation for Mac/Editor
       Vector3 center = new Vector3(0, -1f, 2f); // Below and in front of camera
       float width = 2f;
       float height = 0.5f;
        
       // Create 4 anchors in a rectangle
       Vector3[] anchorPositions = {
           center + new Vector3(-width/2, 0, height/2),   // Top left
           center + new Vector3(width/2, 0, height/2),    // Top right
           center + new Vector3(width/2, 0, -height/2),   // Bottom right
           center + new Vector3(-width/2, 0, -height/2)  // Bottom left
       };
        
       foreach (Vector3 position in anchorPositions)
       {
           PlaceAnchor(position);
       }
       #endif
    }

    private IEnumerator AttachIndicatorWhenReady()
    {
        GameObject rightHand = null;
        // Wait until the RightHandAnchor exists in the scene
        while (rightHand == null)
        {
            rightHand = GameObject.Find("RightHandAnchor");
            if (rightHand == null)
                yield return null; // wait for next frame
        }

        controllerIndicator = Instantiate(controllerIndicatorPrefab);
        controllerIndicator.transform.SetParent(rightHand.transform, false);
        controllerIndicator.transform.localPosition = new Vector3(0, 0, 0.05f);
        controllerIndicator.transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) && anchors.Count < 4)
        {
            Vector3 spawnPoint = transform.position + transform.forward * 0.05f;
            PlaceAnchor(spawnPoint);
        }
    }

    void PlaceAnchor(Vector3 position)
    {
        GameObject anchor = Instantiate(anchorPrefab, position, Quaternion.identity);
        anchor.transform.parent = null;  // crucial to prevent movement
        anchors.Add(anchor);

        if (anchors.Count == 4 && !planeCreated)
        {
            CreatePlane();
            planeCreated = true;
        }
    }

    void CreatePlane()
    {
        GameObject plane = new GameObject("AnchorPlane");
        MeshFilter meshFilter = plane.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = plane.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        for (int i = 0; i < 4; i++)
            vertices[i] = anchors[i].transform.position;

        mesh.vertices = vertices;
        mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 };
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        Material matToUse = planeMaterial;
        if (matToUse == null)
        {
            // Use Unlit/Color for proper stereo projection
            matToUse = new Material(Shader.Find("Unlit/Color"));
            matToUse.color = new Color(1f, 0f, 0f, 0.2f); // semi-transparent red

            // Enable transparency and depth support manually
            matToUse.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            matToUse.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            matToUse.SetInt("_ZWrite", 1);
            matToUse.renderQueue = 3000;
        }
        meshRenderer.material = matToUse;

        //Expose the top edge
        TopEdgeStart = anchors[0].transform.position;
        TopEdgeEnd = anchors[1].transform.position;

        Debug.Log($"[AnchorScript] Plane created! TopEdgeStart: {TopEdgeStart}, TopEdgeEnd: {TopEdgeEnd}");
        Debug.Log($"[AnchorScript] Plane width: {Vector3.Distance(TopEdgeStart, TopEdgeEnd)}");

        if (songMenuCanvas != null)
        {
            songMenuCanvas.SetActive(true);
        }

        InstructionPopup popup = FindObjectOfType<InstructionPopup>();
        if (popup != null)
        {
            popup.HidePopup();
        }

        OnPlaneCreated?.Invoke(plane.transform);

        MidiFilePlayer midiPlayer = FindObjectOfType<MidiFilePlayer>();
        if (midiPlayer != null)
        {
            Debug.Log("[AnchorScript] Found MidiFilePlayer, starting playback...");
            midiPlayer.MPTK_Play();
        }
        else
        {
            Debug.LogWarning("[AnchorScript] MidiFilePlayer not found in scene!");
        }

        // Show the song menu canvas
        if (songMenuCanvas != null)
        {
            songMenuCanvas.SetActive(true); // Show the canvas
        }

        // Force the plane to face world +Z
        plane.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    public void ResetAnchorsAndPlane()
    {
        // Destroy all anchor GameObjects
        foreach (var anchor in anchors)
        {
            if (anchor != null)
                Destroy(anchor);
        }
        anchors.Clear();

        // Destroy the plane if it exists
        var planeObj = GameObject.Find("AnchorPlane");
        if (planeObj != null)
            Destroy(planeObj);

        // Reset top edge points
        TopEdgeStart = Vector3.zero;
        TopEdgeEnd = Vector3.zero;

        // Optionally, hide the song menu canvas
        if (songMenuCanvas != null)
            songMenuCanvas.SetActive(false);

        // Show the instruction popup again
        InstructionPopup popup = FindObjectOfType<InstructionPopup>();
        if (popup != null)
        {
            if (popup.instructionCanvas != null)
                popup.instructionCanvas.enabled = true;
        }

        // Reset controller indicator (if it was hidden/disabled, re-enable and reset position)
        //if (controllerIndicator != null)
        //{
        //    controllerIndicator.SetActive(true);
        //    controllerIndicator.transform.SetParent(this.transform);
        //    controllerIndicator.transform.localPosition = new Vector3(0, 0, 0.05f);
        //    controllerIndicator.transform.localRotation = Quaternion.identity;
        //}

        // Reset MidiNoteBarVisualizer (clear bars, destroy grid, reset state)
        var visualizer = FindObjectOfType<MidiNoteBarVisualizer>();
        if (visualizer != null)
        {
            visualizer.ReloadAndRestart(); // This will clear bars and reset state
            // Destroy grid overlay if it exists
            var grid = FindObjectOfType<MidiNoteLaneGrid>();
            if (grid != null)
                Destroy(grid.gameObject);
        }

        // Stop MIDI playback if needed
        var midiPlayer = FindObjectOfType<MidiPlayerTK.MidiFilePlayer>();
        if (midiPlayer != null)
        {
            midiPlayer.MPTK_Stop();
        }

        // Reset state
        planeCreated = false;
    }
}