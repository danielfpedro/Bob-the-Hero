using UnityEngine;

public class TriggerController : MonoBehaviour {

    public enum TriggerTypes {Touch, ButtonPress};

    public TriggerTypes type;
    public bool oneShot = false;

    public Vector2 size = new Vector2(.5f, .5f);
    public Vector2 offset = Vector2.zero;

    public LayerMask validContacts;

    public bool isOn = false;
    private bool contact = false;
    private Vector2 center;

    public bool fired = false;

    // Use this for initialization
    void Start () {
        CalculateTheCenter();
    }
	
	// Update is called once per frame
	void Update () {

        contact = Physics2D.OverlapBox(center, size, 0f, validContacts);

        if (!oneShot || (oneShot && !fired))
        {
            if (type == TriggerTypes.Touch)
            {
                isOn = contact;
                if (isOn == true)
                {
                    fired = true;
                }
            }
            else
            {
                if (contact == true && Input.GetButtonDown("Action"))
                {
                    isOn = !isOn;
                    fired = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        CalculateTheCenter();

        Color color = Color.blue;
        if (contact == true)
        {
            color = Color.red;
        }
        color.a = .2f;
        Gizmos.color = color;
        Gizmos.DrawCube(center, size);
    }

    private void CalculateTheCenter()
    {
        center = (Vector2)transform.position + offset;
    }
}
