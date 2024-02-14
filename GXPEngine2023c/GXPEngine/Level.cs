using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Level : GameObject
{
    private int bpm;
	private List<Sound> melody;

	private int counterBeats;

	public Level(int bpm)
	{
		this.bpm = bpm;

		counterBeats = 0;
    }

	private void Update()
	{ 
		//Spawn the tile with the random sound from the list based on bpm
		counterBeats += Time.deltaTime;
	}
}