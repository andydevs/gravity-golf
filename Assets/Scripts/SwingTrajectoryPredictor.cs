using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwingTrajectoryPredictor : MonoBehaviour
{
    // Public variables
    public int numPoints = 100;
    public Transform planets;
    public GameObject goshtbol;

    // Private golf ball variables
    private Rigidbody2D physics;
    private SwingController swing;
    private StrokeController stroke;

    // Line points array
    private Vector2[] points;

    // Physics scene
    private Scene simulationScene;
    private PhysicsScene2D physicsScene;
    private GameObject goffbolSim;
    private Rigidbody2D goffbolSimPhysics;
    private Gravitator[] planetSimGs;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        physics = GetComponent<Rigidbody2D>();
        swing = GetComponent<SwingController>();
        stroke = GetComponent<StrokeController>();

        // Initialize points array
        points = new Vector2[numPoints];

        // Create physics scene
        simulationScene = SceneManager.CreateScene("simulation", 
            new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicsScene = simulationScene.GetPhysicsScene2D();

        // Add planets to scene
        planetSimGs = new Gravitator[planets.childCount];
        GameObject planetSim;
        for (int index = 0; index < planets.childCount; ++index)
        {
            Transform planet = planets.GetChild(index);
            planetSim = Instantiate(planet.gameObject, planet.position, planet.rotation);
            planetSim.GetComponent<Renderer>().enabled = false;
            planetSimGs[index] = planetSim.GetComponent<Gravitator>();
            SceneManager.MoveGameObjectToScene(planetSim, simulationScene);
        }

        // Run after start
        StartCoroutine(AfterStart());
    }

    /**
     * Update planet references after start so that 
     * they don't get set after I create new planet 
     * objects...
     * 
     * TODO: There has to be a better way to do this...
     */
    IEnumerator AfterStart()
    {
        yield return null;
        foreach (Gravitator planet in planetSimGs)
        {
            planet.SetGolfbol(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (swing.InSwingControl)
        {
            // Instantiate Goshtbol in sim
            goffbolSim = Instantiate(gameObject, transform.position, transform.rotation);
            SceneManager.MoveGameObjectToScene(goffbolSim, simulationScene);

            try
            {
                // Set physics of golfball
                goffbolSimPhysics = goffbolSim.GetComponent<Rigidbody2D>();
                goffbolSimPhysics.velocity = Mathf.Lerp(
                    stroke.strokeInitialSpeedMin,
                    stroke.strokeInitialSpeedMax,
                    swing.SwingArgument.magnitude
                ) * swing.SwingArgument.normalized;
                goffbolSimPhysics.simulated = true;

                // Simulation
                for (int i = 0; i < numPoints; ++i)
                {
                    points[i] = goffbolSimPhysics.transform.position;
                    foreach (Gravitator planetg in planetSimGs)
                    {
                        goffbolSimPhysics.AddForce(planetg.ComputeGravityForce(goffbolSimPhysics));
                    }
                    physicsScene.Simulate(Time.fixedDeltaTime);
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
