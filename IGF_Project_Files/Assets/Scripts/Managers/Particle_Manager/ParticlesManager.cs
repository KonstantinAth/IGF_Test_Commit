using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Particle collection manager...
//we store all the particles in use here...
public class ParticlesManager : MonoBehaviour {
    [SerializeField] public ParticleSystem destuctedTreesParticles;
    [SerializeField] public ParticleSystem hitPoitParticles;
    #region Singleton
    public static ParticlesManager _instance;
    private void Awake() {
        _instance = this;
    }
    #endregion
}
