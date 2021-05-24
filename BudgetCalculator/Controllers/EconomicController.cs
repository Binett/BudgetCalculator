using BudgetCalculator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BudgetCalculator.Controllers
{
    /// <summary>
    /// The controller of EconomicObject objects.
    /// </summary>
    public class EconomicController
    {
        private List<EconomicObject> EconomicObjectList;
        private List<string> errorLog;

        #region Public Methods
        public EconomicController()
        {
            EconomicObjectList = new List<EconomicObject>();
            errorLog = new List<string>();
        }

        public List<EconomicObject> GetList => EconomicObjectList;

        public bool AddEconomicObjectToList(string name, EconomicType type, double amount)
        {
            if (IsValidString(name) && IsAmountMoreThanZero(amount) && !DoListContainName(name))
            {
                EconomicObjectList.Add(new EconomicObject
                {
                    Name = name,
                    Type = type,
                    Amount = amount,
                });
                return true;
            }

            return false;
        }

        public bool RemoveEconomicObjectFromList(string name)
        {
            if (IsValidString(name))
            {
                if (DoListContainName(name))
                {
                    EconomicObjectList.RemoveAll(x => x.Name == name);
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateEconomicObjectAmount(string name, double newAmount)
        {
            if (IsValidString(name) && IsAmountMoreThanZero(newAmount) && DoListContainName(name))
            {
                foreach (var ecoObj in EconomicObjectList)
                {
                    if (ecoObj.Name.Contains(name))
                    {
                        ecoObj.Amount = newAmount;
                        return true;
                    }
                }
            }

            return false;  
        }

        public bool UpdateEconomicObjectName(string oldName, string newName)
        {
            if(IsValidString(oldName) && IsValidString(newName))
            {
                foreach(var ecoObj in EconomicObjectList)
                {
                    if(ecoObj.Name.Contains(oldName))
                    {
                        ecoObj.Name = newName;
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Methods
        private bool IsValidString(string check)
        {
            if(!IsStringNull(check))
            {
                if(!IsStringEmpty(check))
                {
                    if(!IsStringPreWhitespace(check))
                    {
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("String name had a whitespace as first character");
                        //impelement logger logic
                        return false;
                    }
                }
                else
                {
                    Debug.WriteLine("String name was empty");
                    //impelement logger logic
                    return false;
                }
            }
            else
            {
                Debug.WriteLine("String name was null");
                //impelement logger logic
                return false;
            }
        }

        private bool IsStringNull(string check)
        {
            return check == null;
        }

        private bool IsStringEmpty(string check)
        {
            return check == "";
        }

        private bool IsStringPreWhitespace(string check)
        {
            return check[0] == ' ';
        }

        private bool IsAmountMoreThanZero(double amount)
        {
            if(amount > 0)
            {
                return true;
            }
            else
            {
                Debug.WriteLine("Amount was less than zero");
                //Logic to save to logger
                return false;
            }
        }

        private bool DoListContainName(string name)
        {
            foreach (var ecoObj in EconomicObjectList)
            {
                if (ecoObj.Name.Contains(name))
                {
                    return true;
                }
            }

            Debug.WriteLine("String name does not exist in economic object list");
            //Logic to save to logge
            return false;
        }
        #endregion
    }
}