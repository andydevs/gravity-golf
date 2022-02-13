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
    private GameObject goffbolSim;
    private Rigidbody2D goffbolSimPhysics;

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
            goffbolSim = Instantiate(
                swing.Golfball,
                swing.Golfball.transform.position,
                swing.Golfball.transform.rotation);
            SceneManager.MoveGameObjectToScene(goffbolSim, simu.SimuScene);

            try
            {
                // Set physics of golfball
                goffbolSimPhysics = goffbolSim.GetComponent<Rigidbody2D>();
                goffbolSimPhysics.velocity = swing.SwingSpeed;
                goffbolSimPhysics.simulated = true;

                // Simulation
                for (int i = 0; i < numPoints; ++i)
                {
                    points[i] = goffbolSimPhysics.transform.position;
                    foreach (Gravitator planet in simu.Planets)
                    {
                        goffbolSimPhysics.AddForce(planet.ComputeGravityForce(goffbolSimPhysics));
                    }
                    simu.SimuScenePhysics.Simulate(Time.fixedDeltaTime);
                }
            }
            finally
            {
                // Destroy GolfBall
                Destroy(goffbolSim);
            }
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
