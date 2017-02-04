using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Button _Button;
    [SerializeField]
    private Image _Image;
    [SerializeField]
    private Sprite[] _PlayersSprites;

    private int _PlayerIndex;

    public int PlayerIndex
    {
        get
        {
            return _PlayerIndex;
        }
    }
	void Awake ()
    {
        _PlayerIndex = -1;
	}
	
    public void OnClick()
    {
        _PlayerIndex = TurnManager.Instance.CurrentPlayerIndex;
        _Button.interactable = false;
        _Image.sprite = _PlayersSprites[_PlayerIndex];
        if (BoardController.Instance.CheckWin())
        {
            Debug.Log("Win!");
        }
        else
        {
            TurnManager.Instance.NextPlayerTurn();
        }
    }
}
