using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState
{
    public int bunnies, bears, elephants, duration;
    public GameState(int bunnies, int bears, int elephants, int duration)
    {
        this.bunnies = bunnies;
        this.bears = bears;
        this.elephants = elephants;
        this.duration = duration;
    }

}

public static class Communicator {

    public static Queue<GameState> lastGames = new Queue<GameState>();

	// Use this for initialization
	public static void Start () {
        if ( lastGames.Count == 0 )
        {
            lastGames.Enqueue(new GameState(3, 2, 1, 10));
            lastGames.Enqueue(new GameState(3, 2, 1, 10));
            lastGames.Enqueue(new GameState(3, 2, 1, 10));
            lastGames.Enqueue(new GameState(3, 2, 1, 10));
        }
	}
	
}
