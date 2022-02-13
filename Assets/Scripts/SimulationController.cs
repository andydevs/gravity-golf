using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * NOTE: There should only be ONE of these loaded into scene 
 */
public class SimulationController : MonoBehaviour
{
    private Scene simuScene;
    private PhysicsScene2D simuScenePhysics;
    private Gravitator[] planets;

    // Start is called before the first frame update
    void Start()
    {
        // Create physics scene
        simuScene = SceneManager.CreateScene("simulation",
            new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        simuScenePhysics = simuScene.GetPhysicsScene2D();

        // Add planets to scene
        planets = new Gravitator[transform.childCount];
        Transform planet;
        GameObject planet2;
        for (int i = 0; i < transform.childCount; ++i)
        {
            planet = transform.GetChild(i);
            planet2 = Instantiate(planet.gameObject, planet.position, planet.rotation);
            planet2.GetComponent<Renderer>().enabled = false;
            planets[i] = planet2.GetComponent<Gravitator>();
            SceneManager.MoveGameObjectToScene(planet2, simuScene);
        }

        // Reset planets after start
        StartCoroutine(AfterStart());
    }

    IEnumerator AfterStart()
    {
        yield return null;
        foreach (Gravitator planet3 in planets)
        {
            planet3.SetGolfbol(null);
        }
    }

    public void SimulateObjectTrajectory(GameObject gobject, Vector2 initialVelocity, ref Vector2[] points)
    {
        // Create new object and add to scene
        GameObject simObject = Instantiate(
            gobject,
            gobject.transform.position,
            gobject.transform.rotation);
        SceneManager.MoveGameObjectToScene(
            simObject,
            simuScene);

        try
        {
            // Initialize physics of object
            Rigidbody2D sim = simObject.GetComponent<Rigidbody2D>();
            sim.velocity = initialVelocity;
            sim.simulated = true;

            // Simulation
            for (int i = 0; i < points.Length; ++i)
            {
                points[i] = sim.transform.position;
                foreach (Gravitator planet in planets)
                {
                    sim.AddForce(planet.ComputeGravityForce(sim));
                }
                simuScenePhysics.Simulate(Time.fixedDeltaTime);
            }
        }
        finally
        {
            // Destroy GolfBall
            Destroy(simObject);
        }
    }
}
