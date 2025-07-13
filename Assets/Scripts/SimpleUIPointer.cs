using UnityEngine;
using UnityEngine.UI;

public class SimpleUIPointer : MonoBehaviour
{
    public LineRenderer line;
    public float maxDistance = 10f;
    public Color defaultColor = Color.white;
    public Color hoverColor = Color.red;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Vector3 endPos = transform.position + transform.forward * maxDistance;
        Color currentColor = defaultColor;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            endPos = hit.point;

            Button button = hit.collider.GetComponent<Button>();
            if (button)
            {
                currentColor = hoverColor;

                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    button.onClick.Invoke();
                    Debug.Log("Clicked button: " + button.name);
                }
            }
        }

        // Update line position
        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPos);

        // Set color
        line.startColor = currentColor;
        line.endColor = currentColor;
    }
}
