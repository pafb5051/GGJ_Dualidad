using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public enum PopUpMode{
	single,
	yesOrNo,
    autoHide,
}

public class GenericUIPopUp : MonoBehaviour {
	private static GenericUIPopUp _instance;
	public static GenericUIPopUp Instance{
		get{
			return _instance;
		}
	}

	private Action<bool> _currentAction;
	private string _message;
	private PopUpMode _mode;
    private float _timeToHide;

    public GameObject popUpGo;
	public GameObject singleButtonMode;
	public GameObject yesOrNoButtonMode;

    public Text textComponent;
    public Image background;
    //public GameObject border;
    public Sprite defaultBackground;
    public Color defaultColor;

	void Awake(){
		if(_instance == null){
			_instance = this;
		}else if(_instance != this){
			GameObject.Destroy(gameObject);
		}
	}

	void OnDestroy(){
		if(_instance == this){
			_instance = null;
		}
	}

	public void Show(){        
		if(_mode == PopUpMode.single){
			singleButtonMode.SetActive(true);
		}else if(_mode == PopUpMode.yesOrNo){
			yesOrNoButtonMode.SetActive(true);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(WaitToHide());
        }
        popUpGo.SetActive(true);
	}
	
	public void Hide(){
		if(_mode == PopUpMode.single){
			singleButtonMode.SetActive(false);
		}else{
			yesOrNoButtonMode.SetActive(false);
		}
        
        popUpGo.SetActive(false);
	}

    public void ConfigurePopUp(Sprite sprite, Action<bool> callback, PopUpMode type, float time = 0)
    {
        _message = string.Empty;
        _currentAction = callback;
        _mode = type;
        _timeToHide = time;
        textComponent.text = _message;
        background.sprite = sprite;
        background.color = Color.white;
        //border.SetActive(false);
    }

	public void ConfigurePopUp(String message, Action<bool> callback, PopUpMode type,float time = 0){
		_message = message;
		_currentAction = callback;
		_mode = type;
        _timeToHide = time;
        textComponent.text = _message;
        background.sprite = defaultBackground;
        background.color = defaultColor;
        //border.SetActive(true);
	}

    public void OnYesOrNoButtonClick(bool result)
    {
        if (_mode == PopUpMode.single || _mode == PopUpMode.autoHide)
        {
            result = true;
        }
        
        Hide();

        if (_currentAction != null)
        {
            _currentAction(result);
        }
    }

    IEnumerator WaitToHide()
    {
        yield return new WaitForSeconds(_timeToHide);
        OnYesOrNoButtonClick(true);
    }
}
