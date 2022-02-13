using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwingTrajectoryPredictor : MonoBehaviour
{
    // Public variables
    public int numPoints = 100;
    public Transform planets;

    // Private golf ball variables
    private SwingController swing;

    // Line points array
    private Vector2[] points;

    // Physics scene
    private SimulationController simu;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        swing = GetComponent<SwingController>();
        simu = FindObjectOfType<SimulationController>();

        // Initialize points array
        points = new Vector2[numPoints];
    }

    // Update is called once per frame
    void Update()
    {
        if (swing.InSwingControl)
        {
            // Instantiate Goshtbol in sim
            simu.SimulateObjectTrajectory(
                swing.Golfball, 
                swing.SwingSpeed, 
                ref points);
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 1; i < numPoints; ++i)
        {
            Gizmos.DrawLine(points[i - 1], points[i]);
        }
    }
}
