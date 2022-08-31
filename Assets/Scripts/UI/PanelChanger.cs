using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class PanelChanger : MonoBehaviour
{
    public Button Btn_Object;
    public Button Btn_Color;

    public GameObject Panel_Menu;
    public GameObject Panel_Object;
    public GameObject Panel_Color;

    // Start is called before the first frame update
    void Start()
    {
        Panel_Menu.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Active_Object_Control_Panel()
    {
        // �ش� �г�,,�޴��г� Ȱ��ȭ ��Ű��, ������ �г��� Ȱ��ȭ ��Ű�� �ʴ´�.
        Panel_Object.gameObject.SetActive(true);
        Panel_Menu.gameObject.SetActive(true);
        Panel_Color.gameObject.SetActive(false);
    }
    public void Active_Color_Control_Panel()
    {
        // �ش� �г�,,�޴��г� Ȱ��ȭ ��Ű��, ������ �г��� Ȱ��ȭ ��Ű�� �ʴ´�.
        Panel_Object.gameObject.SetActive(false);
        Panel_Menu.gameObject.SetActive(true);
        Panel_Color.gameObject.SetActive(true);
    }
}