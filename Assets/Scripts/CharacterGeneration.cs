using UnityEngine;

public class CharacterGeneration : MonoBehaviour
{
    public GameObject[] heads;
    public GameObject[] bodies;
    public GameObject[] legs;
    void Start()
    {
        RandomGeneration();
    }

    public void RandomGeneration()
    {
        foreach(var h in heads) h.SetActive(false);
        foreach(var b in bodies) b.SetActive(false);
        foreach(var l in legs) l.SetActive(false);

        heads[Random.Range(0, heads.Length)].SetActive(true);
        bodies[Random.Range(0, bodies.Length)].SetActive(true);
        legs[Random.Range(0, legs.Length)].SetActive(true);
    }

}
