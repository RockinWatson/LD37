using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalElf : BaseFortification {

    [SerializeField]
    private Transform _shotOrigin;

    [SerializeField]
    private float _timeToNewShot = 3.0f;
    private float _timer = 0.0f;

	// Use this for initialization
	private void Start ()
    {
        _timer = 0.0f;

        
        //Audio Effect
        var create_audio = GameObject.Find("coal_elf(Clone)");
        create_audio.GetComponent<ElfAudio1>().Creation_Audio();
    
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (CanUpdate())
        {
            UpdateCoalShot();
        }
	}

    private void UpdateCoalShot()
    {
        if (_timer >= _timeToNewShot)
        {
            CreateNewSpirit();

            _timer = 0.0f;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private void CreateNewSpirit()
    {
        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("coal_shot"));
        CoalShot shot = go.GetComponent<CoalShot>();
        shot.Initialize(_shotOrigin.position);
    }
}
