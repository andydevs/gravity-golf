using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwingTrajectoryPredictor : MonoBehaviour
{
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
        // Update trajectory points
        if (swing.InSwingControl) simu.SimulateObjectTrajectory(swing.GolfBall, swing.SwingSpeed, ref points);
        else for (int i = 0; i < tLine.positionCount; i++) points[i] = Vector3.zero;

        // Set positions in array
        tLine.SetPositions(points);
    }
}
