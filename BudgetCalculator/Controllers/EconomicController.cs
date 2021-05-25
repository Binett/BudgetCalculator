using BudgetCalculator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BudgetCalculator.Helpers;

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
            string errormsg;
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
                        errormsg = $"{this} String name had a whitespace as first character";
                        Debug.WriteLine(errormsg);
                        ErrorLogger.Add(errormsg);
                        return false;
                    }
                }
                else
                {
                    errormsg = $"{this} String name was empty";
                    Debug.WriteLine(errormsg);
                    ErrorLogger.Add(errormsg);
                    return false;
                }
            }
            else
            {
                errormsg = $"{this} String name was null";
                Debug.WriteLine(errormsg);
                ErrorLogger.Add(errormsg);
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
                string errormsg = $"{this} Amount was less than zero";
                Debug.WriteLine(errormsg);
                ErrorLogger.Add(errormsg);
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

            string errormsg = $"{this} String name does not exist in economic object list";
            Debug.WriteLine(errormsg);
            ErrorLogger.Add(errormsg);
            return false;
        }
        #endregion
    }
}