using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwingTrajectoryPredictor : MonoBehaviour
{
    // Public variables
    public Transform planets;

    // Private golf ball variables
    private SwingController swing;
    private SimulationController simu;
    private LineRenderer tLine;

    // Line points array
    private Vector3[] points;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        swing = GetComponent<SwingController>();
        simu  = FindObjectOfType<SimulationController>();
        tLine = GetComponent<LineRenderer>();

        // Initialize points array
        points = new Vector3[tLine.positionCount];
    }

    // Update is called once per frame
    void Update()
    {
        if (swing.InSwingControl)
        {
            // Instantiate Goshtbol in sim
            simu.SimulateObjectTrajectory(swing.Golfball, swing.SwingSpeed, ref points);
            tLine.SetPositions(points);
        }
    }
}
