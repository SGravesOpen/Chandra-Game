using System.Collections; using System.Collections.Generic; using UnityEngine;  [RequireComponent(typeof(SphereCollider))] public class Crash : MonoBehaviour {      private SphereCollider cl;     private bool exploded = false;     public GameObject explosion;      // Start is called before the first frame update     void Start()     {         cl = GetComponent<SphereCollider>();              }      private void OnCollisionEnter(Collision collision)     {         if(!exploded) {             GameObject ps = Instantiate(explosion, this.transform);             Destroy(collision.gameObject);             Destroy(ps, 2.0f);             exploded = true;             Debug.Log("done");         }     } }  