using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOrbit
{
    /*
     * a method that applies gravity laws on the current IOrbit object
     * and the Orbiter object.
     * 
     * o: an Orbiter object
     */
    void gravitate(Orbiter o);
}
