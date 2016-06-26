﻿using UnityEngine;
using System.Collections;

public class BoxManager : MonoBehaviour {

	public Sprite[] sprites;
	public bool isActionable = false;
    public int level;//Niveau dans lequel se trouve la box
    public int num;//Numero de la box dans le niveau
	private RDF database;

	private int gravite;

	// Use this for initialization
	void Start () {
		string tailleBoite = "";
		database = new RDF ();
		tailleBoite = database.getValue (this.gameObject.name,"taille");
		this.transform.localScale = new Vector2(int.Parse(tailleBoite) ,int.Parse(tailleBoite));
		this.gravite = int.Parse(database.getValue (this.gameObject.name, "gravite"));
	}

	// Update is called once per frame
	void Update () {
		float distFromPlayer = Mathf.Sqrt (Mathf.Pow(this.transform.position.x - GameObject.Find("Bob").transform.position.x, 2) 
			+ Mathf.Pow(this.transform.position.y - GameObject.Find("Bob").transform.position.y, 2));
		if (distFromPlayer < 3) {
			GetComponent<SpriteRenderer> ().sprite = sprites [1];
			isActionable = true;
		} else {
			GetComponent<SpriteRenderer> ().sprite = sprites [0];
			isActionable = false;
		}

		if (Input.GetKeyDown ("e") && isActionable)
			refreshData ();
        if (Input.GetKeyDown("a") && isActionable)
            getData();
            

    }

    void getData()
    {
        GameObject.Find("Bob").GetComponent<Animator>().SetBool("bro", true);
        GameObject.Find("Bob").GetComponent<Animator>().SetInteger("num", num);
        GameObject.Find("Bob").GetComponent<Animator>().SetInteger("lvl", level);
    }

	void refreshData(){
		string tailleBoite = "";
		try{
			tailleBoite = database.getValue (this.gameObject.name,"taille");
			this.transform.localScale = new Vector2(int.Parse(tailleBoite) ,int.Parse(tailleBoite));
		}catch(System.FormatException ignored){
			Debug.Log (ignored.Message);
			Debug.Log (tailleBoite);
		}
	}
}
