package pacman.controllers.BT;
import pacman.controllers.Controller;
import pacman.game.Constants.MOVE;
import pacman.game.Game;

public class AssignmentController extends Controller<MOVE> 
{
	Node currNode;
	DataContext data;
	
	Selector root, escapeOrHunt;
	Sequence huntingSequence;
	
	State eat, escape, powerUp, hunt, ghostNear; 
	Leaf eatLeaf, escapeLeaf, powerUpLeaf, huntLeaf; 
	
	public AssignmentController()
	{			
		this.data = new DataContext();
		eat = new Eat(null, data);
		escape = new Escape(null, data);
		powerUp = new PowerUp(null,data);
		hunt = new HuntGhost(null, data);
		ghostNear = new GhostNear(null, data);
		
		root = new Selector();
		escapeOrHunt = new Selector();
		huntingSequence = new Sequence();
		escapeLeaf = new Leaf(escape);
		powerUpLeaf = new Leaf(powerUp);
		huntLeaf = new Leaf(hunt);
		eatLeaf = new Leaf(eat);

		this.currNode = root;

		root.addChild(ghostNear);				
		root.addChild(eatLeaf);
		ghostNear.addChild(escapeOrHunt);
		escapeOrHunt.addChild(huntingSequence);
		escapeOrHunt.addChild(escapeLeaf);
		huntingSequence.addChild(powerUpLeaf);
		huntingSequence.addChild(huntLeaf);
	}
	
	public void Reset()
	{
		this.currNode = root;
	}
	
	@Override
	public MOVE getMove(Game game, long timeDue) 
	{
		escape.UpdateState(game);
		hunt.UpdateState(game);
		powerUp.UpdateState(game);
		eat.UpdateState(game);
		ghostNear.UpdateState(game);		
		data.UpdateState(game);
		
		currNode = currNode.process();
		StateValue currNodeState = this.currNode.nodeState();
		
		switch(currNodeState)
		{
			case FAILURE:
				Reset();
				break;
			case SUCCESS:
				Reset();
				break;
			case RUNNING:
				break;		
		}
		return data.GetCurrentMove();	
	}	
}