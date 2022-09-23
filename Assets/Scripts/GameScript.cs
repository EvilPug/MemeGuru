using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class GameScript : MonoBehaviour
{
    //Текст для вывода из БД
    public Text DebugText;

    [Header("Панели")]
    public GameObject Epoch_Panel;
    public GameObject[] Shop_Panels;
    public GameObject[] Unit_Panels;

    [Header("Текст на главном экране")]
    public Text scoreText;
    public Text click_bonusText;
    public Text incomeText;
    public Text epochText;

    [Header("Кол-во очков, бонус за клик и заработок в секунду")]
    public double score = 0;
    public double click_bonus = 1;
    public double income = 0;

    [Header("Множитель стоимости юнита")]
    private double price_multiplier = 1.07;
    private double income_multiplier = 1.06;
    private double next_item = 1.4;

    [Header("Количество эпох и текущая эпоха")]
    public int epoch_amount = 9;
    public int current_epoch;


    [Header("Количество купленных юнитов")]
    public double[,] unit_owned;

    [Header("Количество всех юнитов")]
    public int all_units;

    [Header("Текущие стоимость и количество юнитов")]
    public Text[] unit_priceText;
    public Text[] unit_ownedText;

    [Header("Стоимость и прибыльность юнитов")]
    public double[,] unit_base_cost;
    public double[,] unit_base_income;
    public double[,] unit_price;
    public double[,] unit_income;

    [Header("Particles")]
    public ParticleSystem particles;


    void Start()
    {
        //Открываем БД
        var ds = new DataService("game_data.db");
        //ds.CreateDB ();
        var units = ds.GetUnits();
        ToConsole(units);


        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //unit_base_cost[i, j] = 50 * Math.Pow(2, i) * Math.Pow(5, j);
            }
        }

        StartCoroutine(Income_Per_Second());
    }

    //Заработок всех юнитов в секунду
    IEnumerator Income_Per_Second()
    {
        while (true)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; i < 8; j++)
                {
                    income += unit_income[i, j];
                }

            }

            score += income;

            yield return new WaitForSeconds(1);
        }
    }

    void Update()
    {
        // Обновляем очки, бонусы за клик и зароботок в секунду
        scoreText.text = "Points : " + score.ToString();
        click_bonusText.text = "Click Bonus: " + click_bonus.ToString() + "/click";
        incomeText.text = "Income: " + income.ToString() + "/sec";

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 8; j++)
            {

                //Рассчитываем стоимость юнитов
                //unit_price[i, j] = unit_base_cost[i, j]; //Math.Pow(price_multiplier, unit_owned[i, j]);

                //Рассчитываем прибыльность юнитов
               // unit_income[i, j] = unit_owned[i, j] * unit_base_income[i, j] * Math.Pow(income_multiplier, unit_owned[i, j]);

                //Пишем рассчитаные параметры на страницах юнитов
               // unit_priceText[i].text = "Price: " + unit_price[i, j].ToString();
                //unit_ownedText[i].text = "Owned: " + unit_owned[i, j].ToString();


            }
        }
    }

    // Элементы
    public void Shop_Opener()
    {
        Shop_Panels[current_epoch].SetActive(!Shop_Panels[current_epoch].activeSelf);

        for (int i = 0; i < all_units; i++)
        {
            if (Unit_Panels[current_epoch].activeSelf)
            {
                Unit_Panels[current_epoch].SetActive(false);
            }
        }
    }
    // */
    public void Choose_Epoch(int index)
    {
        current_epoch = index;
        Epoch_Panel.SetActive(!Epoch_Panel.activeSelf);

        epochText.text = "201" + (index + 1).ToString();
    }

    public void Buy_Unit(int unit_id)
    {
        if (score >= unit_price[current_epoch, unit_id])
        {
            score -= unit_price[current_epoch, unit_id];
            unit_owned[current_epoch, unit_id]++;
        }
        else
        {
            Debug.Log("Not Enough Money!");
        }
    }

    public void Epoch_Panel_Opener()
    {
        Epoch_Panel.SetActive(!Epoch_Panel.activeSelf);

        for (int i = 0; i < epoch_amount; i++)
        {
            if (Shop_Panels[i].activeSelf)
            {
                Shop_Panels[i].SetActive(!Shop_Panels[i].activeSelf);
            }
        }
    }

    public void Unit_Panel_Opener(int unit_id)
    {
        Unit_Panels[current_epoch].SetActive(!Unit_Panels[current_epoch].activeSelf);
    }

    public void OnClick()
    {
        particles.Play();
        ParticleSystem.EmissionModule em = particles.emission;
        em.enabled = false;

        if (UnityEngine.Random.Range(0, 11) == 1)
        {
            score += click_bonus*2;
            Debug.Log("Bonus!");
            
            em.enabled = true;
        }
        else
        {
            score += click_bonus;
        }
    }

    //Функция вывода текста из БД в консоль
    private void ToConsole(string msg)
    {
        DebugText.text += System.Environment.NewLine + msg;
        Debug.Log(msg);
    }

    private void ToConsole(IEnumerable<Unit> units)
    {
        foreach (var unit in units)
        {
            ToConsole(unit.ToString());
        }

    }

}