using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fy.Definitions;
using Fy.Helpers;

namespace Fy.UI {

	public struct MenuOrderTab {
		public GameObject go;

		public MenuOrderTab(GameObject go) {
			this.go = go;
		}
	}

	public struct MenuOrderTabLink {
		public GameObject go;
		public Image image;
		public MenuOrderTabLink(GameObject go, Image image) {
			this.go = go;
			this.image = image;
		}
	}

	public struct MenuOrderButton {
		public GameObject go;
		public Button button;
		public Text text;
		public Image image;

		public MenuOrderButton(GameObject go, Button button, Text text, Image image) {
			this.go = go;
			this.button = button;
			this.text = text;
			this.image = image;
		}
	}


	public class MenuController : MonoBehaviour
	{
		public InfoController info;
		public Transform parent;
		public Transform parentMenu;
		public MenuOrderButton[] buttons;
		public MenuOrderTab[] tabs;
		public Color activeColor;
		public Color defaultColor;
		public int current = -1;
		public MenuOrderDef currentOrder;
		public List<MenuOrderTabLink> links;

	    void Start()
	    {
	    	this.links = new List<MenuOrderTabLink>();
	    	int tabCount = 6;
	    	this.buttons = new MenuOrderButton[tabCount];
	    	this.tabs = new MenuOrderTab[tabCount];

	        this.AddTab("Orders", 0);
	        this.AddTab("Zones", 1);
	        this.AddTab("Structures", 2);
	        this.AddTab("Production",3);
	        this.AddTab("Magic", 4);
	        this.AddTab("Combat", 5);
	        this.currentOrder = null;
	        this.Reset();
	    }

	    public void ClearSelection() {
	    	foreach (MenuOrderButton btn in this.buttons) {
	    		btn.image.color = this.defaultColor;
	    	}
	    	foreach (MenuOrderTab tab in this.tabs) {
	    		tab.go.SetActive(false);
	    	}
	    	this.info.Reset();
	    }

	    public void ClearOrders() { 
	    	foreach (MenuOrderTabLink btn in this.links) {
	    		btn.image.color = this.defaultColor;
	    	}
	    }

	    public void Reset() {
	    	if (this.currentOrder == null) {
	    		this.current = -1;
	    		this.ClearSelection();
	    	} else {
	    		this.currentOrder = null;
	    		this.ClearOrders();
	    	}
 	    }

	    public void AddTab(string name, int id) {
	    	Text text;
	    	Image image;
	    	Button button;

	    	GameObject go = GameObject.Instantiate(Res.prefabs["order_panel"]);
	    	go.transform.SetParent(this.parent);
	    	// Change offset ?
	    	go.name = "OrderTab: "+ name;
	    	this.tabs[id] = new MenuOrderTab(go);
	    	List<MenuOrderDef> orders = new List<MenuOrderDef>();
	    	if (id == 0) {
	    		orders = new List<MenuOrderDef>(Defs.orders.Values);
	    	}
	    	foreach (MenuOrderDef order in orders) {
	    		GameObject _go = GameObject.Instantiate(Res.prefabs["button_order"]);
	    		_go.transform.SetParent(go.transform);
	    		_go.name = "OrderButton: "+ order.name;
	    		text = _go.GetComponentInChildren<Text>();
	    		text.text = "(_)"; // Keybind
	    		_go.GetComponentsInChildren<Image>()[1].sprite = order.sprite;

	    		button = _go.GetComponentInChildren<Button>();
	    		image =  _go.GetComponentsInChildren<Image>()[0];
	    		MenuOrderTabLink orderLink = new MenuOrderTabLink(_go, image);
	    		button.onClick.AddListener(
	    			delegate {
	    				this.ClearOrders();
	    				if (this.currentOrder != order) {
		    				this.currentOrder = order;
		    				orderLink.image.color = this.activeColor;
		    				this.info.title.text = order.name;
		    				this.info.desc.text = order.shortDesc;
	    				}
	    			}
	    		);
	    		this.links.Add(orderLink);
	    	}

	    	go = GameObject.Instantiate(Res.prefabs["button_player_panel"]);
	    	go.transform.SetParent(this.parentMenu);
	    	go.name = "Button: "+ name;
	    	text = go.GetComponentInChildren<Text>();
	    	text.text = name;
	    	image = go.GetComponentInChildren<Image>();
	    	button = go.GetComponentInChildren<Button>();
	    	button.onClick.AddListener(
	    		delegate {
	    			if (this.current != id) {
	    				this.ClearSelection();
		    			this.current = id;
		    			this.buttons[this.current].image.color = this.activeColor;
		    			this.tabs[this.current].go.SetActive(true);
	    			} else {
	    				this.Reset();
	    			}
	    		}
	    	);
	    	this.buttons[id] = new MenuOrderButton(go, button, text, image);
	    }

	    public void Update() {
	    	if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) {
	    		this.Reset();
	    	}
	    }
	}
}