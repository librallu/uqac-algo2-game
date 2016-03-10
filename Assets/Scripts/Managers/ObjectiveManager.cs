using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour {

    public ScoreManager scoreManager;
    protected Text text;
    protected int startTime;
    protected int zombunniesKilled;
    protected int zombearsKilled;
    protected int hellephantsKilled;
    public ObjectiveTree tree;
    public bool timerStopped = false;
    public int deathAt = 0;
    public int diffScore = 0;


    public void addZombunniesKilled()
    {
        zombunniesKilled++;
    }

    public void addZombearsKilled()
    {
        zombearsKilled++;
    }

    public void addHellephantsKilled()
    {
        hellephantsKilled++;
    }

    protected ObjectiveTree generateObjectives()
    {
        int bunnies = 0;
        int bears = 0;
        int elephants = 0;
        int duration = 0;
        int factor = 1;
        Communicator.Start();
        foreach (GameState i in Communicator.lastGames.ToArray() )
        {
            bunnies += i.bunnies*factor;
            bears += i.bears*factor;
            elephants += i.elephants*factor;
            duration += i.duration*factor;
            factor++;
        }

        bunnies = (int) ((float)bunnies / 10f) + 2;
        bears = (int)((float)bears / 10f) + 2;
        elephants = (int)((float)elephants / 10f) + 1;
        duration = (int)((float)duration / 10f) + 15;


        return new ObjectiveOr(
            new ObjectiveAnd(
                new ObjectiveAnd(new ObjectiveZombunnies(this, bunnies), new ObjectiveZombears(this, bears))
                , new ObjectiveHellephants(this, elephants)
            ), new ObjectiveDuration(this, duration)
        );
    }

	// Use this for initialization
	void Start () {
        startTime = (int)Time.time;
        zombunniesKilled = 0;
        zombearsKilled = 0;
        hellephantsKilled = 0;
        text = GetComponent<Text>();
        tree = generateObjectives();
    }
	
	// Update is called once per frame
	void Update () {
        text.text = toString();
        tree = tree.simplify();
    }

    public int getDuration()
    {
        if (timerStopped)
            return deathAt;
        else {
            if ( diffScore < (int)Time.time - startTime)
            {
                scoreManager.setScore(scoreManager.getScore() + 1);
                diffScore = (int)Time.time - startTime;
            }
            return (int)Time.time - startTime;
        }
    }

    public int getScore()
    {
        return scoreManager.getScore();
    }

    public int getZombunniesKilled()
    {
        return zombunniesKilled;
    }

    public int getZombearsKilled()
    {
        return zombearsKilled;
    }

    public int getHellephantsKilled()
    {
        return hellephantsKilled;
    }

    protected string treeToString(ObjectiveTree t, string padding)
    {
        if ( t is ObjectiveLeaf || t is ObjectiveEnd )
        {
            return padding + t.toString();
        } else if ( t is ObjectiveBinary )
        {
            ObjectiveBinary tmp = (ObjectiveBinary)t;
            return treeToString(tmp.left, padding + ". ") + '\n' +
                padding+tmp.toString() + '\n' +
                treeToString(tmp.right, padding + ". ");
        } else
        {
            return "error";
        }
    }

    public string toString()
    {
        if ( tree is ObjectiveOk)
        {
            return "SURVIVE THE LONGEST !!!\n\n"+
            "duration: " + getDuration() +
            "\nZombunnies: " + zombunniesKilled +
            "\nZombears: " + zombearsKilled +
            "\nhellephants: " + hellephantsKilled +
            "\nscore: " + getScore();
        }
        return treeToString(tree, "");
        /*  */
    }
}
