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
        private List<EconomicObject> EconomicObjectList;

        public EconomicController()
        {
            EconomicObjectList = new List<EconomicObject>();
        }

        public List<EconomicObject> GetList => EconomicObjectList;

        public bool AddEconomicObjectToList(string name, EconomicType type, double amount)
        {
            if (name != null)
            {
                if(name != "")
                {
                    if (amount > 0)
                    {
                        EconomicObjectList.Add(new EconomicObject
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
                        //Logic to save to logger
                        return false;
                    }
                }
                else
                {
                    Debug.WriteLine("String name was empty");
                    //Logic to save to logger
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("String name was null");
                //Logic to save to logger
                return false;
            }
        }

        public bool RemoveEconomicObjectFromList(string name)
        {
            if (name != null)
            {
                if (name != "")
                {
                    foreach (var ecoObj in EconomicObjectList)
                    {
                        if (ecoObj.Name.Contains(name))
                        {
                            EconomicObjectList.Remove(ecoObj);
                          
                            return true;
                        }
                    }
                    Debug.WriteLine("Name does not exist in economic object list");
                    //Logic to save to logger
                    return false;
                }
                else
                {
                    Debug.WriteLine("String name was empty");
                    //Logic to save to logger
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("String name was null");
                //Logic to save to logger
                return false;
            }
        }

        public bool UpdateEconomicObjectAmount(string name, double newAmount)
        {
            if (name != null)
            {
                if (name != "")
                {
                    if(newAmount > 0)
                    {
                        foreach(var ecoObj in EconomicObjectList)
                        {
                            if(ecoObj.Name.Contains(name))
                            {
                                ecoObj.Amount = newAmount;
                                return true;
                            }
                        }

                        Debug.WriteLine("Name does not exist in economic object list");
                        //Logic to save to logger
                        return false;
                    }
                    else
                    {
                        Debug.WriteLine("New amount was 0 or less");
                        //Logic to save to logger
                        return false;
                    }
                }
                else
                {
                    Debug.WriteLine("String name was empty");
                    //Logic to save to logger
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("String name was null");
                //Logic to save to logger
                return false;
            }
        }

        public bool UpdateEconomicObjectName(string oldName, string newName)
        {

            if (newName != null && oldName != null)
            {
                if (newName != "" && oldName != "")
                {
                    foreach(var ecoObj in EconomicObjectList)
                    {
                        if(ecoObj.Name.Contains(oldName))
                        {
                            ecoObj.Name = newName;
                            return true;
                        }
                    }

                    Debug.WriteLine("Name does not exist in economic object list");
                    //Logic to save to logger
                    return false;
                }
                else
                {
                    Debug.WriteLine("String name was empty");
                    //Logic to save to logger
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("String name was null");
                //Logic to save to logger
                return false;
            }
        }
    }
}