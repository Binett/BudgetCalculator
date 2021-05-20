using BudgetCalculator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BudgetCalculator.Controllers
{
    /// <summary>
    /// The "container" logic for economic objects.
    /// </summary>
    public class EconomicController
    {
        private List<EconomicObject> EconomicObjbectList;

        public EconomicController()
        {
            EconomicObjbectList = new List<EconomicObject>();
        }

        public List<EconomicObject> GetList => EconomicObjbectList;

        public bool AddEconomicObjectToList(string name, EconomicType type, double amount)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (amount > 0)
                {
                    EconomicObjbectList.Add(new EconomicObject
                    {
                        Name = name,
                        Type = type,
                        Amount = amount,
                    });
                    return true;
                }
                else
                {
                    Debug.WriteLine("Amount was less than zero");
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("String name was null or empty");
                return false;
            }
        }

        public bool RemoveEconomicObjectFromList(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEconomicObjectAmount(string name, double newAmount)
        {
            if(!string.IsNullOrEmpty(name))
            {
                if(newAmount > 0)
                {
                    for (int i = 0; i < EconomicObjbectList.Count; i++)
                    {
                        if(EconomicObjbectList[i].Name == name)
                        {
                            EconomicObjbectList[i].Amount = newAmount;
                            return true;
                        }
                        else 
                        {
                            Debug.Write("Name does not exist in economic object list");
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("new amount was less than zero");
                }
            }
            else
            {
                Debug.WriteLine("string name was null or empty");
            }
            return false;
        }

        public bool UpdateEconomicObjectName(string oldName, string newName)
        {
            if (!string.IsNullOrEmpty(newName))
            {
                for (int i = 0; i < EconomicObjbectList.Count; i++)
                {
                    if (EconomicObjbectList[i].Name == oldName)
                    {
                        EconomicObjbectList[i].Name = newName;
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Name does not exist in economic object list");
                    }
                }
            }
            else
            {
                Debug.WriteLine("string oldName was null or empty");
            }
            return false;
        }
    }
}